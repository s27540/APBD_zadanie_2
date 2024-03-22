// See https://aka.ms/new-console-template for more information

using System.Text;
using zadanie_2;

public class Program
{
    public static void Main(string[] args)
    {
        RefrigeratedLoad refrigeratedLoad= new RefrigeratedLoad("Banana", -13);

        //Stworzenie kontenerów danego typu
        
        //Kontener na gaz
        GasContainer gasContainer = new GasContainer(321,231,3123131,1234,2313);
        GasContainer gasContainer2 = new GasContainer(321,231,3123131,3455,2313);
        
        //Kontener na płyny
        LiquidContainer liquidContainer = new LiquidContainer(231123,3213,3213,1234,true);
        LiquidContainer liquidContainer2 = new LiquidContainer(231123,3213,6000,4321,true);
        
        //Kontener chłodniczy
        RefrigeratedContainer refrigeratedContainer =
            new RefrigeratedContainer( 12313, 23133123, 312313232, 5433, refrigeratedLoad, 21);
        RefrigeratedContainer refrigeratedContainer2 =
            new RefrigeratedContainer( 12313, 23133123, 312313232, 8765, refrigeratedLoad, 21);
        
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
        Console.WriteLine($"Kontenerowiec po dodaniu listy oraz pojedynczych kontenerów -> {containerShip}");
        
        //Usunięcie kontenerów z kontenerowca
        containerShip.RemoveContainerFromShip(gasContainer);
        containerShip.RemoveContainerFromShip(liquidContainer);
        containerShip.RemoveContainerFromShip(refrigeratedContainer);
        
        //Wypisanie specyfikacji kontererowca oraz jego zawartosci po usunięciu kontenerów
        Console.WriteLine($"Kontenerowiec po usunięciu kontenerów z kontenerowca -> {containerShip}");


        //Zawartość przed rozładowaniem:
        Console.WriteLine($"Zwartosc kontenera z gazem przed rozładowaniem -> {gasContainer2}");
        
        //Rozładowanie kontenera
        gasContainer2.EmptyTheLoad();
        
        //Zawartość po rozładowaniem:
        Console.WriteLine($"Zwartosc kontenera z gazem po rozładowaniem -> {gasContainer2}");
        
        //Zastąpienie kontenera na statku o danym numerze seryjnym innym kontenerem
        Console.WriteLine();
        containerShip.SwitchContainers("KON-R-2",gasContainer);
        
        //Wypisanie specyfikacji kontererowca oraz jego zawartosci po zamianie kontenerów
        Console.WriteLine($"Zawartosc kontenerowca po zamianie -> {containerShip}");

        //Przeniesienie kontenera miedzy dwoma statkami
        ContainerShip containerShip2 = new ContainerShip(23,76777778,777777);
        containerShip.RemoveContainerFromShip(liquidContainer2);
        containerShip2.LoadContainer(liquidContainer2);
        
        //Wypisanie specyfikacji dwóch kontenerowców oraz jego zawartośći po przenisieniu kontenera z jedngo kontenerowca do drugiego
        Console.WriteLine($"Zawartosc kontenerowca nr 1 po przeniesieniu -> {containerShip}");
        Console.WriteLine($"Zawartosc kontenerowca nr 2 po przeniesieniu -> {containerShip2}");
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