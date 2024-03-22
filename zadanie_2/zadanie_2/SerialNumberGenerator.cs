namespace zadanie_2;

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