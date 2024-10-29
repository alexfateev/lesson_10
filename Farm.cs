using System.Text;

class Farm
{
    private List<Chicken> chikens = new List<Chicken>();
    private List<Cow> cows = new List<Cow>();
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
                chikens.Add(new Chicken());
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
                cows.Add(new Cow());
                message = "Куплена новая коровка";
            }
            else message = "На вашей ферме нет места для новых коровок";
        }
        else message = "Недостаточно денег";
    }

    private void FeedupChickens(out string message)
    {
        message = "Нет курочек которых можно покормить";
        foreach (Chicken chicken in chikens)
        {
            if (chicken.IsAlive && feedSupply > 0)
            {
                chicken.Feedup();
                feedSupply--;
            }
            else
            {
                message = "Закончился корм. Кто-то из курочек будет голодать";
                return;
            }
            message = "Все курочки накормлены";
        }
    }

    private void FeedupCows(out string message)
    {
        message = "Нет коровок которых можно покормить";
        foreach (Cow cow in cows)
        {
            if (cow.IsAlive && feedSupply > 0)
            {
                cow.Feedup();
                feedSupply--;
            }
            else
            {
                message = "Закончился корм. Кто-то из коровок будет голодать";
                return;
            }
            message = "Все коровки накормлены";
        }
    }

    private void FeedupAll(out string message)
    {
        StringBuilder bs = new StringBuilder();

        FeedupChickens(out string chickenMessage);
        FeedupCows(out string cowMessage);

        bs.AppendLine(chickenMessage);
        bs.AppendLine(cowMessage);
        message = bs.ToString();
    }

    private void Harvest(out string message) // Собираем урожай
    {
        int eggCount = 0; int milkCount = 0;
        foreach (Chicken chicken in chikens)
        {
            if (chicken.IsAlive) eggCount += chicken.CollectEggs();

        }
        foreach (Cow cow in cows)
        {
            if (cow.IsAlive) milkCount += cow.CollectMilk();
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

    private void NewDay(out string message)
    {
        day++;
        List<Chicken> deadChickens = new List<Chicken>();
        List<Cow> deadCows = new List<Cow>();
        foreach (Chicken chicken in chikens)
        {
            chicken.NewDay(out bool isDead);
            if (isDead) deadChickens.Add(chicken);
        }
        foreach (Cow cow in cows)
        {
            cow.NewDay(out bool isDead);
            if (isDead) deadCows.Add(cow);
        }
        if (deadCows.Count == 0 && deadChickens.Count == 0) message = "";
        else message = $"От голода умерло Курочек: {deadChickens.Count} \t  Коровок: {deadCows.Count}";

        foreach (Chicken deadChicken in deadChickens) chikens.Remove(deadChicken);
        foreach (Cow deadCow in deadCows) cows.Remove(deadCow);
    }

    private void ShowStatusFarm()
    {
        Console.WriteLine($"День: {day}");
        Console.WriteLine($"Денег: {money}");
        Console.WriteLine($"Запас корма: {feedSupply}");
        Console.WriteLine($"Собрано яиц: {eggSupply}");
        Console.WriteLine($"Собрано молока: {milkSupply}");
        Console.WriteLine($"Курочек: {chikens.Count}\nКоровок: {cows.Count}");
        Console.WriteLine("------------------------------------------------------------------------");
    }




    public void ShowMenu()
    {
        string message = "";
        while (true)
        {
            Console.Clear();
            ShowStatusFarm();

            Console.WriteLine($"1. Купить корм. {feedCost} монет за {feedBuyPart} ед.");
            Console.WriteLine($"2. Покормить курочек");
            Console.WriteLine($"3. Покормить коровок");
            Console.WriteLine($"4. Покормить всех");
            Console.WriteLine($"5. Собрать урожай");
            Console.WriteLine($"6. Продать урожай");
            Console.WriteLine($"7. Купить курочку ({chickenCost}) монет");
            Console.WriteLine($"8. Купить коровку ({cowCost}) монет");
            Console.WriteLine($"9. Закончить день");

            if (!message.Equals("")) { Console.WriteLine(message); message = ""; }

            Console.Write("Выберите действие: ");

            var res = int.TryParse(Console.ReadLine(), out int number);
            if (!res) continue;
            switch (number)
            {
                case 1: BuyFeed(out message); break;
                case 2: FeedupChickens(out message); break;
                case 3: FeedupCows(out message); break;
                case 4: FeedupAll(out message); break;
                case 5: Harvest(out message); break;
                case 6: SellHarvest(out message); break;
                case 7: BuyChicken(out message); break;
                case 8: BuyCow(out message); break;
                case 9: NewDay(out message); break;
            }
        }
    }

}
