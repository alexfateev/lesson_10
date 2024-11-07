using lesson_10;

class Chicken : Animal

{

    private int _amountEgg = 0;
    public Chicken(AccountHandler ev) : base(ev)
    {
    }


    private int CalcEggChance()
    {
        Random rand = new Random();
        return rand.Next(1, 126);  // 25% шанc дать 2 яйца
    }

    public override int CollectHarvest()
    {
        if (_harvestToday) return 0; // Если сегодня собирали урожай
        _harvestToday = true;
        switch (_hunger)
        {
            case HungerLevel.Feedup:
                if (CalcEggChance() > 100) return 2;
                else return 1;
            case HungerLevel.LightHunger:
                if (CalcEggChance() <= 75) return 1;
                else return 0;
            case HungerLevel.MiddleHunger:
                if (CalcEggChance() <= 50) return 1;
                else return 0;
        }
        return 0;
    }
}