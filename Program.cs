using System.Runtime;

class Program
{
    class Ferma
    {
        private Chicken[] chikens;
        private Cow[] cows;
    }
    class Chicken
    {

        private HungerLevel _hunger { get; set; } = HungerLevel.Feedup;
        public bool _isAlive { get; set; } = true;
        private int _amountFeed { get; set; } = 0;
        private int _amountEggs { get; set; } = 0;

        public void Eat()
        {
            if (!_isAlive) return;
            if (_amountFeed > 0)
            {
                _amountFeed--;
                _hunger = HungerLevel.Feedup;
            }
            else
            {
                switch (_hunger)
                {
                    case HungerLevel.Feedup: _hunger = HungerLevel.LightHunger; break;
                    case HungerLevel.LightHunger: _hunger = HungerLevel.MiddleHunger; break;
                    case HungerLevel.MiddleHunger: _hunger = HungerLevel.StrongHunger; break;
                    case HungerLevel.StrongHunger: _isAlive = false; break;
                }
            }

            _amountEggs += GetEgg();
        }

        public void GiveFood(int count)
        {
            _amountFeed += count;
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

        int CalcEggChance()
        {
            Random rand = new Random();
            return rand.Next(1, 111);  //Для 10% шанса дать 2 яйца
        }

    }
    class Cow
    {
        private HungerLevel _hunger { get; set; } = HungerLevel.Feedup;
        public bool _isAlive { get; set; } = true;
        private int _amountFeed { get; set; } = 0;

        public void Eat()
        {
            if (!_isAlive) return;
            if (_amountFeed > 0)
            {
                _amountFeed--;
                _hunger = HungerLevel.Feedup;
            }
            else
            {
                switch (_hunger)
                {
                    case HungerLevel.Feedup: _hunger = HungerLevel.LightHunger; break;
                    case HungerLevel.LightHunger: _hunger = HungerLevel.MiddleHunger; break;
                    case HungerLevel.MiddleHunger: _hunger = HungerLevel.StrongHunger; break;
                    case HungerLevel.StrongHunger: _isAlive = false; break;
                }
            }
        }

        public void GiveFood(int count)
        {
            _amountFeed += count;
        }

        public int CollectMilk()
        {
            return 0;

        }

        private int GetMilk()
        {

            return 0;
            //int CalcEggChance()
            //{
            //    Random rand = new Random();
            //    return rand.Next(1, 111);  //Для 10% шанса дать 2 яйца
            //}

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


    }
}