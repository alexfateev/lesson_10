using lesson_10;

namespace pack1;


class Program
{
    static void Main(string[] args)
    {
        Farm farm = new Farm(new CombineMessage(new ConsoleMessage(), new FileMessage()));
        farm.ShowMenu();
    }
}