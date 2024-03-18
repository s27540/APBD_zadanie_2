// See https://aka.ms/new-console-template for more information

public class Program
{
    public static void Main(string[] args)
    {
        
    }
}

public class Container
{
    private double _loadMass;
    private double _height;
    private double _ownWeight;
    private string _serialNumber;
    private double _maxLoad;

    public Container(double loadMass, double height, double ownWeight, string serialNumber, double maxLoad)
    {
        _loadMass = loadMass;
        _height = height;
        _ownWeight = ownWeight;
        _serialNumber = serialNumber;
        _maxLoad = maxLoad;
    }

    public void EmptyTheLoad()
    {
        _loadMass = 0;
    }

    public void LoadMass(double massToLoad)
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
}


public class OverfillException : Exception
{ 
    public OverfillException(string message) : base (message)
    {
        
    }
}