// See https://aka.ms/new-console-template for more information

using System.Text;

public class Program
{
    public static void Main(string[] args)
    {


    }
}

public class ContainerShip
{
    
    private List<Container> _loadedContainers;
    private double _maxSpeed;
    private int _maxAmountOfContainers;
    private double _maxMassOfContainers;

    public ContainerShip(List<Container> loadedContainers, double maxSpeed, int maxAmountOfContainers, double maxMassOfContainers)
    {
        _loadedContainers = loadedContainers;
        _maxSpeed = maxSpeed;
        _maxAmountOfContainers = maxAmountOfContainers;
        _maxMassOfContainers = maxMassOfContainers;
    }

    public void LoadListOfContainers(List<Container> containersToLoad)
    {
        
        if (_loadedContainers.Count + containersToLoad.Count > _maxAmountOfContainers)
        {
            throw new InvalidOperationException("Adding these containers would exceed the maximum amount of containers allowed on the ship.");
        }

        double totalMassOfNewContainers = 0;
        foreach (var container in containersToLoad)
        {
            totalMassOfNewContainers += container.getLoadMass();
        }
        
        double totalMassCurrentContainers = 0;
        foreach (var container in containersToLoad)
        {
            totalMassCurrentContainers += container.getLoadMass();
        }

        if (totalMassOfNewContainers + totalMassCurrentContainers > _maxMassOfContainers * 1000)
        {
            throw new InvalidOperationException("Adding these containers would exceed the maximum mass of containers allowed on the ship.");
        }

        _loadedContainers.AddRange(containersToLoad);
    }

    public void LoadContainer(Container containerToLoad)
    {
        List<Container> containersToAdd = new List<Container> { containerToLoad };
        LoadListOfContainers(containersToAdd);
    }

    public void RemoveContainerFromShip(Container containerToRemove)
    {
        _loadedContainers.Remove(containerToRemove);
    }

    public void SwitchContainers(string serialNumberToRemove, string serialNumberToAdd)
    {
        Container containerToRemove = Container.GetContainerBySerialNumber(serialNumberToRemove);
        Container containerToAdd = Container.GetContainerBySerialNumber(serialNumberToAdd);

        foreach (var container in _loadedContainers)
        {
            if (container._serialNumber.Equals(serialNumberToRemove))
            {
                _loadedContainers.Remove(containerToRemove); 
                _loadedContainers.Add(containerToAdd);
            }
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Loaded Containers: [");
        foreach (var container in _loadedContainers)
        {
            sb.AppendLine(container.ToString());
        }
        sb.AppendLine($"], Max speed: {_maxSpeed}, Max amount of containers: {_maxAmountOfContainers}, Max mass of containers: {_maxMassOfContainers}");

        return sb.ToString();
    }

}

public class Container
{
    protected double _loadMass;
    protected double _height;
    protected double _ownWeight;
    public string _serialNumber { get; set; }
    protected double _maxLoad;
    private static Dictionary<string, Container> containersBySerialNumber = new Dictionary<string, Container>();

    public Container(double loadMass, double height, double ownWeight, double maxLoad)
    {
        
        if (loadMass >= 0 && loadMass <= maxLoad)
        {
            _loadMass = loadMass;
        }
        else
        {
            throw new OverfillException("Load mass must be non-negative and less than or equal to max load.");
        }

        _height = height;
        _ownWeight = ownWeight;
        _serialNumber = SerialNumberGenerator.GenerateSerialNumberForDefaultContainer();
        _maxLoad = maxLoad;

        containersBySerialNumber.Add(_serialNumber, this);
    }

    public virtual void EmptyTheLoad()
    {
        _loadMass = 0;
    }


    public Double getLoadMass()
    {
        return _loadMass;
    }
    
    public virtual void LoadMass(double massToLoad)
    {
        if (massToLoad >= 0 && massToLoad <= _maxLoad)
        {
            _loadMass = massToLoad;
        }
        else
        {
            throw new OverfillException("Load mass must be non-negative and less than or equal to max load.");
        }
    }
    
    public static Container GetContainerBySerialNumber(string serialNumber)
    {
        if (containersBySerialNumber.ContainsKey(serialNumber))
        {
            return containersBySerialNumber[serialNumber];
        }
        else
        {
            return null; 
        }
    }

    public override string ToString()
    {
        return $"Container Serial Number: {_serialNumber}, Load Mass: {_loadMass}, Height: {_height}, Own Weight: {_ownWeight}, Max Load: {_maxLoad}";
    }

}

public class LiquidContainer : Container, IHazardNotifier
{

    private bool _isDangerLoad;
    
    public LiquidContainer(double loadMass, double height, double ownWeight, double maxLoad, bool isDangerLoad) : base(loadMass, height, ownWeight, maxLoad)
    {
        _serialNumber = SerialNumberGenerator.GenerateSerialNumberForLiquidContainer();
        _isDangerLoad = isDangerLoad;
    }

    public override void LoadMass(double massToLoad)
    {
        double maxCapacity = _isDangerLoad ? _maxLoad * 0.5 : _maxLoad * 0.9;
        if (massToLoad >= 0 && massToLoad <= maxCapacity)
        {
            _loadMass = massToLoad;
        }
        else
        {
            NotifyHazard();
        }
    }
    
    public void NotifyHazard()
    {
        Console.WriteLine($"Hazard notification for container {_serialNumber}: Danger detected!");
    }
    
    
    public override string ToString()
    {
        return $"{base.ToString()}, is Danger load ?: {_isDangerLoad}";
    }
}

public class GasContainer : Container, IHazardNotifier
{

    private double _pressure;
    
    public GasContainer(double loadMass, double height, double ownWeight, double maxLoad, double pressure) : base(loadMass, height, ownWeight, maxLoad)
    {
        _serialNumber = SerialNumberGenerator.GenerateSerialNumberForGasContainer();
        _pressure = pressure;
    }

    public override void EmptyTheLoad()
    {
        double massToLeaveInContainer = _loadMass * 0.05;
        _loadMass = massToLeaveInContainer;
    }

    public void NotifyHazard()
    {
        Console.WriteLine($"Hazard notification for container {_serialNumber}: Danger detected!");
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Pressure: {_pressure}";
    }
}

public class RefrigeratedContainer : Container
{

    private RefrigeratedLoad _typeOfLoadToStore;
    private double _temperatureInContainer;

    public RefrigeratedContainer(double loadMass, double height, double ownWeight, double maxLoad, RefrigeratedLoad typeOfLoadToStore, double temperatureInContainer) : base(loadMass, height, ownWeight, maxLoad)
    {
        if(typeOfLoadToStore == _typeOfLoadToStore)
        {
            throw new ArgumentException("RefrigeratedContainer can only store products of the same type.");
        }

        if (_temperatureInContainer < typeOfLoadToStore._requiredTemperature)
        {
            throw new ArgumentException("RefrigeratedContainer temperature can't be lower than product required temperature.");
        }

        _serialNumber = SerialNumberGenerator.GenerateSerialNumberForRefrigeratedContainer();
        _typeOfLoadToStore = typeOfLoadToStore;
        _temperatureInContainer = temperatureInContainer;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Stored load: {_typeOfLoadToStore}, Temperature in container: {_temperatureInContainer}";
    }
}

public class Load
{
    public string _name { get; set; }

    public Load(string name)
    {
        _name = name;
    }

    public override string ToString()
    {
        return $"Name : {_name}";
    }
}

public class RefrigeratedLoad : Load
{
    public double _requiredTemperature { get; set; }

    public RefrigeratedLoad(string name, double requiredTemperature) : base(name)
    {
        _requiredTemperature = requiredTemperature;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Required temperature: {_requiredTemperature}";
    }
}

public class SerialNumberGenerator
{
    private static int _defaultContainerCounter = 0;
    private static int _liquidContainerCounter = 0;
    private static int _gasContainerCounter = 0;
    private static int _refrigeratedContainerCounter = 0;
    
    public static string GenerateSerialNumberForDefaultContainer()
    {
        _defaultContainerCounter++;
        return $"KON-K-{_defaultContainerCounter}";
    }
    
    public static string GenerateSerialNumberForLiquidContainer()
    {
        _liquidContainerCounter++;
        return $"KON-L-{_liquidContainerCounter}";
    }

    public static string GenerateSerialNumberForGasContainer()
    {
        _gasContainerCounter++;
        return $"KON-G-{_gasContainerCounter}";
    }
    
    public static string GenerateSerialNumberForRefrigeratedContainer()
    {
        _refrigeratedContainerCounter++;
        return $"KON-R-{_refrigeratedContainerCounter}";
    }
}

public interface IHazardNotifier
{
    void NotifyHazard();
}


public class OverfillException : Exception
{ 
    public OverfillException(string message) : base (message)
    {
        
    }
}