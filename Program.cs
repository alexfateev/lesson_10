using System.Runtime;
using System.Text;

class Program
{
    class Farm
    {
        private Chicken[] chikens = new Chicken[50];
        private Cow[] cows = new Cow[20];
        private int money = 100;

        private int chickenCount = 0;
        private int cowCount = 0;

        private int chickenCost = 15;
        private int cowCost = 65;

        private int eggSupply = 0;
        private int milkSupply = 0;

        private int feedSupply = 10; // Запас корма
        private int feedCost = 10; // Стоимость покупки
        private int feedBuyPart = 10; // Сколько покупаем за раз

        private int eggCost = 1;
        private int milkCost = 3;

        private int day = 1;

        public void BuyFeed(out string message)
        {
            if (money >= feedCost)
            {
                money -= feedCost;
                feedSupply += feedBuyPart;
                message = "Куплено 10 ед. корма";
            }
            else message = "Не достаточно денег для покупки корма";
        }

        public void BuyChicken(out string message)
        {
            if (money >= chickenCost)
            {
                if (chickenCount < chikens.Length)
                {
                    money -= chickenCost;
                    chikens[chickenCount] = new Chicken();
                    chickenCount++;
                    message = "Куплена новая курочка";
                }
                else message = "На вашей ферме нет мест для новых курочек";
            }
            else message = "Не достаточно денег";
        }

        public void BuyCow(out string message)
        {
            if (money >= cowCost)
            {
                if (cowCount < cows.Length)
                {
                    money -= cowCost;
                    cows[cowCount] = new Cow();
                    cowCount++;
                    message = "Куплена новая коровка";
                }
                else message = "На вашей ферме нет места для новых коровок";
            }
            else message = "Не достаточно денег";
        }

        public void FeedupChickens(out string message)
        {
            for (int i = 0; i < chickenCount; i++)
            {
                if (chikens[i].IsAlive() && feedSupply > 0)
                {
                    chikens[i].Feedup(1);
                    feedSupply--;
                }
                else
                {
                    message = "Закончился корм. Кто-то из курочек будет голодать";
                    return;
                }
            }
            message = "Все курочки накормлены";
        }

        public void FeedupCows(out string message)
        {
            for (int i = 0; i < cowCount; i++)
            {
                if (cows[i].IsAlive() && feedSupply > 0)
                {
                    cows[i].Feedup(1);
                    feedSupply--;
                }
                else
                {
                    message = "Закончился корм. Кто-то из коровок будет голодать";
                    return;
                }
            }
            message = "Все коровки накормлены";
        }

        public void FeedupAll(out string message)
        {
            StringBuilder bs = new StringBuilder();

            FeedupChickens(out string chickenMessage);
            FeedupCows(out string cowMessage);

            bs.AppendLine(chickenMessage);
            bs.AppendLine(cowMessage);
            message = bs.ToString();
        }

        public void Harvest(out string message) // Собираем урожай
        {
            int eggCount = 0; int milkCount = 0;
            for (int i = 0; i < chickenCount; i++)
            {
                if (chikens[i].IsAlive())
                {
                    eggCount += chikens[i].CollectEggs();
                }
            }
            for (int i = 0; i < cowCount; i++)
            {
                if (cows[i].IsAlive())
                {
                    milkCount += cows[i].CollectMilk();
                }
            }
            message = $"Собрано яиц: {eggCount} \t Собрано молока: {milkCount}";
            eggSupply += eggCount;
            milkSupply += milkCount;
        }

        public void SellHarvest(out string message)
        {
            message = $"Продан урожай на {eggCost * eggSupply + milkCost * milkSupply} монет";
            money += eggCost * eggSupply;
            eggSupply = 0;
            money += milkCost * milkSupply;
            milkSupply = 0;
        }

        public void NewDay()
        {
            for (int i = 0; i < chickenCount; i++) chikens[i].NewDay();
            for (int i = 0; i < cowCount; i++) cows[i].NewDay();
        }

        public void ShowStatusFarm()
        {
            Console.WriteLine($"Денег: {money}");
            Console.WriteLine($"Запас корма: {feedSupply}");
            Console.WriteLine($"Собрано яиц: {eggSupply}");
            Console.WriteLine($"Собрано молока: {milkSupply}");
            Console.WriteLine($"Курочек: {GetAliveChickens()}\nКоровок: {GetAliveCows()}");
            Console.WriteLine("------------------------------------------------------------------------");
        }



        private int GetAliveChickens()
        {
            var res = 0;
            for (int i = 0; i < chickenCount; i++)
            {
                if (chikens[i].IsAlive()) res++;
            }
            return res;
        }

        private int GetAliveCows()
        {
            var res = 0;
            for (int i = 0; i < cowCount; i++)
            {
                if (cows[i].IsAlive()) res++;
            }
            return res;
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
                    case 9: NewDay(); break;
                }
            }
        }

    }
    class Chicken
    {

        private HungerLevel _hunger = HungerLevel.Feedup;
        private bool _isAlive = true;
        private int _amountEggs = 0;
        private int maxEgg = 10;


        public bool IsAlive()
        {
            return _isAlive;
        }
        public void Feedup(int count)
        {
            if (!_isAlive) return;
            if (count > 0) _hunger = HungerLevel.Feedup;
        }

        public void NewDay()
        {
            _amountEggs += GetEgg();
            switch (_hunger)
            {
                case HungerLevel.Feedup: _hunger = HungerLevel.LightHunger; break;
                case HungerLevel.LightHunger: _hunger = HungerLevel.MiddleHunger; break;
                case HungerLevel.MiddleHunger: _hunger = HungerLevel.StrongHunger; break;
                case HungerLevel.StrongHunger: _isAlive = false; break;
            }
        }

        public int CollectEggs()
        {
            var res = _amountEggs;
            _amountEggs = 0;
            return res;

        }

        private int GetEgg()
        {
            switch (_hunger)
            {
                case HungerLevel.Feedup:
                    {
                        if (CalcEggChance() > 100) return 2;
                        else return 1;
                    }
                case HungerLevel.LightHunger:
                    {
                        if (CalcEggChance() <= 75) return 1;
                        else return 0;
                    }
                case HungerLevel.MiddleHunger:
                    {
                        if (CalcEggChance() <= 50) return 1;
                        else return 0;
                    }
            }
            return 0;
        }

        private int CalcEggChance()
        {
            Random rand = new Random();
            return rand.Next(1, 126);  // 25% шанc дать 2 яйца
        }

    }
    class Cow
    {
        private HungerLevel _hunger = HungerLevel.Feedup;
        private bool _isAlive = true;
        private int _amountMilk = 0;

        public bool IsAlive()
        {
            return _isAlive;
        }

        public void Feedup(int count)
        {
            if (!_isAlive) return;
            if (count > 0) _hunger = HungerLevel.Feedup;
        }

        public void NewDay()
        {
            _amountMilk = GetMilk();
            switch (_hunger)
            {
                case HungerLevel.Feedup: _hunger = HungerLevel.LightHunger; break;
                case HungerLevel.LightHunger: _hunger = HungerLevel.MiddleHunger; break;
                case HungerLevel.MiddleHunger: _hunger = HungerLevel.StrongHunger; break;
                case HungerLevel.StrongHunger: _isAlive = false; break;
            }
        }

        private int GetMilk()
        {
            Random rand = new Random();
            switch (_hunger)
            {
                case HungerLevel.Feedup: return rand.Next(14, 21);
                case HungerLevel.LightHunger: return rand.Next(9, 13);
                case HungerLevel.MiddleHunger: return rand.Next(1, 5);
            }
            return 0;
        }


        public int CollectMilk()
        {
            int res = _amountMilk;
            _amountMilk = 0;
            return res;
        }
    }

    enum HungerLevel
    {
        Feedup,
        LightHunger,
        MiddleHunger,
        StrongHunger
    }



    static void Main(string[] args)
    {
        Farm farm = new Farm();
        farm.ShowMenu();

    }
}