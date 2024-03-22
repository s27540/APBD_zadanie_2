namespace zadanie_2;

public class RefrigeratedContainer : Container
{
    private RefrigeratedLoad _typeOfLoadToStore;
    private double _temperatureInContainer;

    public RefrigeratedContainer(double heightCm, double ownWeightKg, double maxLoadKg, double containerDepthCm, RefrigeratedLoad typeOfLoadToStore, double temperatureInContainer) : base(heightCm, ownWeightKg, maxLoadKg, containerDepthCm)
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
