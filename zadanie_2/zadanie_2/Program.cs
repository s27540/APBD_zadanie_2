// See https://aka.ms/new-console-template for more information

using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        RefrigeratedLoad rl= new RefrigeratedLoad("Banana", -13);

        //Stworzenie kontenerów danego typu
        
        //Kontener na gaz
        GasContainer gasContainer = new GasContainer(321,231,3123131,2313);
        GasContainer gasContainer2 = new GasContainer(321,231,3123131,2313);
        
        //Kontener na płyny
        LiquidContainer liquidContainer = new LiquidContainer(231123,3213,3213,true);
        LiquidContainer liquidContainer2 = new LiquidContainer(231123,3213,6000,true);
        
        //Kontener chłodniczy
        RefrigeratedContainer refrigeratedContainer =
            new RefrigeratedContainer( 12313, 23133123, 312313232, rl, 21);
        RefrigeratedContainer refrigeratedContainer2 =
            new RefrigeratedContainer( 12313, 23133123, 312313232, rl, 21);
        
        //Załadowanie kontenerów
        gasContainer.LoadMass(1000);
        gasContainer2.LoadMass(2000);
        liquidContainer.LoadMass(1000);
        liquidContainer2.LoadMass(2000);
        refrigeratedContainer.LoadMass(1000);
        refrigeratedContainer2.LoadMass(2000);

        //Swtorzenie kontenerowca
        ContainerShip containerShip = new ContainerShip(23,2132313,2313123213213);
        
        //Stworzenie listy na kontenery i dodanie do niej paru kontenerów
        List<Container> containers = new List<Container>();
        containers.Add(gasContainer);
        containers.Add(liquidContainer);
        containers.Add(refrigeratedContainer);

        //Załadowanie listy koneterów na kontenerowiec
        containerShip.LoadListOfContainers(containers);
        
        //Załadowanie pojedynczyh koneterów na kontenerowiec
        containerShip.LoadContainer(gasContainer2);
        containerShip.LoadContainer(liquidContainer2);
        containerShip.LoadContainer(refrigeratedContainer2);
        
        //Wypisanie specyfikacji kontererowca oraz jego zawartosci
        Console.WriteLine(containerShip);
        
        //Usunięcie kontenera z kontenerowca
        containerShip.RemoveContainerFromShip(gasContainer);
        containerShip.RemoveContainerFromShip(liquidContainer);
        containerShip.RemoveContainerFromShip(refrigeratedContainer);
        
        //Wypisanie specyfikacji kontererowca oraz jego zawartosci po usunięciu kontenerów
        Console.WriteLine(containerShip);


        //Zawartość przed rozładowaniem:
        Console.WriteLine($"Zwartosc przed rozładowaniem -> {gasContainer2}");
        
        //Rozładowanie kontenera
        gasContainer2.EmptyTheLoad();
        
        //Zawartość po rozładowaniem:
        Console.WriteLine($"Zwartosc po rozładowaniem -> {gasContainer2}");
        
        //Zastąpienie kontenera na statku o danym numerze innym kontenerem
        Console.WriteLine();
        ContainerShip.SwitchContainers("KON-R-2",gasContainer);
        
        //Wypisanie specyfikacji kontererowca oraz jego zawartosci po zamianie kontenerów
        Console.WriteLine($"Zawartosc kontenerowca po zamianie -> {containerShip}");

    }
}

public class ContainerShip
{
    
    private static List<Container> _loadedContainers = new List<Container>();
    private double _maxSpeed;
    private int _maxAmountOfContainers;
    private double _maxMassOfContainers;

    public ContainerShip(double maxSpeed, int maxAmountOfContainers, double maxMassOfContainers)
    {
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

    public static void SwitchContainers(string serialNumberToRemove, Container containerToAdd)
    {
        
        for (int i = 0; i < _loadedContainers.Count; i++)
        {
            if (_loadedContainers[i]._serialNumber.Equals(serialNumberToRemove))
            {
                _loadedContainers.RemoveAt(i);
                break;
            }
        }

        _loadedContainers.Add(containerToAdd);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Loaded Containers: [");
        foreach (var container in _loadedContainers)
        {
            if (container != null)
            {
                sb.AppendLine(container.ToString());
            }
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
    private static Dictionary<string, Container> _containersBySerialNumber = new Dictionary<string, Container>();

    public Container(double height, double ownWeight, double maxLoad)
    {
        _height = height;
        _ownWeight = ownWeight;
        _serialNumber = SerialNumberGenerator.GenerateSerialNumberForDefaultContainer();
        _maxLoad = maxLoad;

        _containersBySerialNumber.Add(_serialNumber, this);
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
        if (_containersBySerialNumber.ContainsKey(serialNumber))
        {
            return _containersBySerialNumber[serialNumber];
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
    
    public LiquidContainer(double height, double ownWeight, double maxLoad, bool isDangerLoad) : base(height, ownWeight, maxLoad)
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
    
    public GasContainer(double height, double ownWeight, double maxLoad, double pressure) : base(height, ownWeight, maxLoad)
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

    public RefrigeratedContainer(double height, double ownWeight, double maxLoad, RefrigeratedLoad typeOfLoadToStore, double temperatureInContainer) : base(height, ownWeight, maxLoad)
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
        return $"{base.ToString()}, Stored load {_typeOfLoadToStore}, Temperature in container: {_temperatureInContainer}";
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