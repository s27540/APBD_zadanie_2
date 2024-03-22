namespace zadanie_2;

public class Container
{
    protected double _loadMassKG;
    protected double _heightCM;
    protected double _ownWeightKG;
    private double _containerDepthCM;
    public string _serialNumber { get; set; }
    protected double _maxLoadKG;
    private static List<Container> _containers = new List<Container>(); 

    public Container(double heightCm, double ownWeightKg, double maxLoadKg, double containerDepthCm)
    {
        _heightCM = heightCm;
        _ownWeightKG = ownWeightKg;
        _serialNumber = SerialNumberGenerator.GenerateSerialNumberForDefaultContainer();
        _maxLoadKG = maxLoadKg;
        _containerDepthCM = containerDepthCm;

        _containers.Add(this);
    }

    public virtual void EmptyTheLoad()
    {
        _loadMassKG = 0;
    }


    public Double getLoadMass()
    {
        return _loadMassKG;
    }
    
    public virtual void LoadMass(double massToLoad)
    {
        if (massToLoad >= 0 && massToLoad <= _maxLoadKG)
        {
            _loadMassKG = massToLoad;
        }
        else
        {
            throw new OverfillException("Load mass must be non-negative and less than or equal to max load.");
        }
    }
    
    public static Container GetContainerBySerialNumber(string serialNumber)
    {
        for (int i = 0; i < _containers.Count; i++)
        {
            if (_containers[i]._serialNumber.Equals(serialNumber))
            {
                return _containers[i];
            }
        }

        return null;
    }

    public override string ToString()
    {
        return $"Container Serial Number: {_serialNumber}, Load Mass: {_loadMassKG}, Height: {_heightCM}, Own Weight: {_ownWeightKG}, Max Load: {_maxLoadKG}, Container depth: {_containerDepthCM}";
    }

}