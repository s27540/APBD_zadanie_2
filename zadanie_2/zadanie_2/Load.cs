namespace zadanie_2;

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