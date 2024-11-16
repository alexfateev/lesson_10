using System.Text;
using System.Threading.Channels;
using lesson_10;
class Farm
{
    private List<Animal> chikens = new List<Animal>();
    private List<Animal> cows = new List<Animal>();
    private int maxChickens = 50;
    private int maxCows = 20;
    private int money = 100;

    private int chickenCost = 15; // Стоимость покупки курочки
    private int cowCost = 65; // Стоимость покупки коровки

    private int eggSupply = 0; // Общий запас яйц
    private int milkSupply = 0; // общий запас молока

    private int feedSupply = 10; // Общий запас корма
    private int feedCost = 10; // Стоимость покупки
    private int feedBuyPart = 10; // Сколько покупаем за раз

    private int eggCost = 1; // Стоимость продажи яиц
    private int milkCost = 3; // Стоимость продажи молока

    private int day = 1;

    private IMessage _combineMessage = new CombineMessage(new ConsoleMessage(), new FileMessage());
    private IMessage _menuText = new ConsoleMessage();


    private void BuyFeed(out string message)
    {
        if (money >= feedCost)
        {
            money -= feedCost;
            feedSupply += feedBuyPart;
            message = $"Куплено {feedBuyPart} ед. корма";
        }
        else message = "Недостаточно денег для покупки корма";
    }

    private void BuyChicken(out string message)
    {
        if (money >= chickenCost)
        {
            if (chikens.Count < maxChickens)
            {
                money -= chickenCost;
                chikens.Add(new Chicken(_combineMessage.Message));
                message = "Куплена новая курочка";
            }
            else message = "На вашей ферме нет мест для новых курочек";
        }
        else message = "Недостаточно денег";
    }

    private void BuyCow(out string message)
    {
        if (money >= cowCost)
        {
            if (cows.Count < maxCows)
            {
                money -= cowCost;
                cows.Add(new Cow(_combineMessage.Message));
                message = "Куплена новая коровка";
            }
            else message = "На вашей ферме нет места для новых коровок";
        }
        else message = "Недостаточно денег";
    }

    private void FeedupAnimal(List<Animal> list, out string message)
    {
        message = "Некого кормить";
        foreach (Animal animal in list)
        {
            if (animal.IsAlive && feedSupply > 0)
            {
                animal.Feedup();
                feedSupply--;
            }
            else
            {
                message = "Закончился корм. Кто-то из животных будет голодать";
            }
        }
        message = "Все животные накормлены";
    }

    private void FeedupAll(out string message)
    {
        StringBuilder bs = new StringBuilder();

        FeedupAnimal(chikens, out string chickenMessage);
        FeedupAnimal(cows, out string cowMessage);

        bs.AppendLine(chickenMessage);
        bs.AppendLine(cowMessage);
        message = bs.ToString();
    }

    private void Harvest(out string message) // Собираем урожай
    {
        int eggCount = 0; int milkCount = 0;
        foreach (Chicken chicken in chikens)
        {
            if (chicken.IsAlive) eggCount += chicken.CollectHarvest();

        }
        foreach (Cow cow in cows)
        {
            if (cow.IsAlive) milkCount += cow.CollectHarvest();
        }
        message = $"Собрано яиц: {eggCount} \t Собрано молока: {milkCount}";
        eggSupply += eggCount;
        milkSupply += milkCount;
    }

    private void SellHarvest(out string message)
    {
        int sold = eggCost * eggSupply + milkCost * milkSupply;
        message = $"Продан урожай на {sold} монет";
        money += sold;
        eggSupply = 0;
        milkSupply = 0;
    }

    private void EndDay(out string message)
    {
        day++;
        List<Chicken> deadChickens = new List<Chicken>();
        List<Cow> deadCows = new List<Cow>();
        foreach (Chicken chicken in chikens)
        {
            chicken.EndDay(out bool isDead);
            if (isDead) deadChickens.Add(chicken);
            chicken.NewDay();
        }
        foreach (Cow cow in cows)
        {
            cow.EndDay(out bool isDead);
            if (isDead) deadCows.Add(cow);
            cow.NewDay();
        }
        if (deadCows.Count == 0 && deadChickens.Count == 0) message = "";
        else message = $"От голода умерло Курочек: {deadChickens.Count} \t  Коровок: {deadCows.Count}";

        foreach (Chicken deadChicken in deadChickens) chikens.Remove(deadChicken);
        foreach (Cow deadCow in deadCows) cows.Remove(deadCow);
    }

    private void ShowStatusFarm()
    {
        _menuText.Message(new string[] {
            $"День: {day}" ,
            $"Денег: {money}",
            $"Запас корма: {feedSupply}",
            $"Собрано яиц: {eggSupply}",
            $"Собрано молока: {milkSupply}",
            $"Курочек: {chikens.Count}\nКоровок: {cows.Count}",
            "------------------------------------------------------------------------"
        });
    }


    public void ShowMenu()
    {
        string message = "";
        while (true)
        {
            Console.Clear();
            ShowStatusFarm();

            _menuText.Message(new string[]
            {
                $"1. Купить корм. {feedCost} монет за {feedBuyPart} ед.",
                $"4. Покормить всех",
                $"5. Собрать урожай",
                $"6. Продать урожай",
                $"7. Купить курочку ({chickenCost}) монет",
                $"8. Купить коровку ({cowCost}) монет",
                $"9. Закончить день"
            });


            if (!message.Equals(""))
            {
                _combineMessage.Message(message);
                message = "";
            }

            _combineMessage.Message("Выберите действие: ");

            var res = int.TryParse(Console.ReadLine(), out int number);
            if (!res) continue;
            switch (number)
            {
                case 1: BuyFeed(out message); break;
                case 4: FeedupAll(out message); break;
                case 5: Harvest(out message); break;
                case 6: SellHarvest(out message); break;
                case 7: BuyChicken(out message); break;
                case 8: BuyCow(out message); break;
                case 9: EndDay(out message); break;
            }
        }
    }

}
