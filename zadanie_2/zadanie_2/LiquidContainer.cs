namespace zadanie_2;

public class LiquidContainer : Container, IHazardNotifier
{
    private bool _isDangerLoad;
    
    public LiquidContainer(double heightCm, double ownWeightKg, double maxLoadKg, double containerDepthCm, bool isDangerLoad) : base(heightCm, ownWeightKg, maxLoadKg, containerDepthCm)
    {
        _serialNumber = SerialNumberGenerator.GenerateSerialNumberForLiquidContainer();
        _isDangerLoad = isDangerLoad;
    }

    public override void LoadMass(double massToLoad)
    {
        double maxCapacity = _isDangerLoad ? _maxLoadKG * 0.5 : _maxLoadKG * 0.9;
        if (massToLoad >= 0 && massToLoad <= maxCapacity)
        {
            _loadMassKG = massToLoad;
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