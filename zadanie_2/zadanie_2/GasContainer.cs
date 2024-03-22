namespace zadanie_2;

public class GasContainer : Container, IHazardNotifier
{
    private double _pressureKPA;
    
    public GasContainer(double heightCm, double ownWeightKg, double maxLoadKg, double containerDepthCm, double pressureKpa) : base(heightCm, ownWeightKg, maxLoadKg, containerDepthCm)
    {
        _serialNumber = SerialNumberGenerator.GenerateSerialNumberForGasContainer();
        _pressureKPA = pressureKpa;
    }

    public override void EmptyTheLoad()
    {
        double massToLeaveInContainer = _loadMassKG * 0.05;
        _loadMassKG = massToLeaveInContainer;
    }

    public void NotifyHazard()
    {
        Console.WriteLine($"Hazard notification for container {_serialNumber}: Danger detected!");
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Pressure: {_pressureKPA}";
    }
}