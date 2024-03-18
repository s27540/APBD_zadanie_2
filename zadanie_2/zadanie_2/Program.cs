// See https://aka.ms/new-console-template for more information

public class Program
{
    public static void Main(string[] args)
    {


    }
}

public class Container
{
    protected double _loadMass;
    protected double _height;
    protected double _ownWeight;
    protected string _serialNumber;
    protected double _maxLoad;

    public Container(double loadMass, double height, double ownWeight, double maxLoad)
    {
        _loadMass = loadMass;
        _height = height;
        _ownWeight = ownWeight;
        _serialNumber = GenerateSerialNumber();
        _maxLoad = maxLoad;
    }

    public virtual void EmptyTheLoad()
    {
        _loadMass = 0;
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

    public string GenerateSerialNumber()
    {

        
        
        
        return "";
    }
}

public class LiquidContainer : Container, IHazardNotifier
{

    private bool _isDangerLoad;
    
    public LiquidContainer(double loadMass, double height, double ownWeight, double maxLoad, bool isDangerLoad) : base(loadMass, height, ownWeight, maxLoad)
    {
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
}

public class GasContainer : Container, IHazardNotifier
{

    private double _pressure;
    
    public GasContainer(double loadMass, double height, double ownWeight, double maxLoad, double pressure) : base(loadMass, height, ownWeight, maxLoad)
    {
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
}

public class RefrigeratedContainer : Container
{

    private RefrigeratedLoad _typeOfLoadToStore;
    private double _temperatureInContainer;

    public RefrigeratedContainer(double loadMass, double height, double ownWeight, double maxLoad, RefrigeratedLoad typeOfLoadToStore, double temperatureInContainer) : base(loadMass, height, ownWeight, maxLoad)
    {
        if(typeOfLoadToStore != _typeOfLoadToStore)
        {
            throw new ArgumentException("RefrigeratedContainer can only store products of the same type.");
        }

        if (!(_temperatureInContainer < typeOfLoadToStore._requiredTemperature))
        {
            throw new ArgumentException("RefrigeratedContainer temperature can't be lower than product required temperature.");
        }

        _typeOfLoadToStore = typeOfLoadToStore;
        _temperatureInContainer = temperatureInContainer;
    }
}

public class Load
{
    public string _name { get; set; }

    public Load(string name)
    {
        _name = name;
    }
}

public class RefrigeratedLoad : Load
{
    public double _requiredTemperature { get; set; }

    public RefrigeratedLoad(string name, double requiredTemperature) : base(name)
    {
        _requiredTemperature = requiredTemperature;
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