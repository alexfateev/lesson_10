namespace pack1;

enum HungerLevel
{
    Feedup,
    LightHunger,
    MiddleHunger,
    StrongHunger
}

class Program
{
    static void Main(string[] args)
    {
        Farm farm = new Farm();
        farm.ShowMenu();
    }
}