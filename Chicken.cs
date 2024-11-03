using lesson_10;

class Chicken : Animal
{
    
    private int _amountEggs = 0;

    public int CollectEggs()
    {
        var res = _amountEggs;
        _amountEggs = 0;
        return res;
    }

    public override void NewDay(out bool isDead)
    {
        _amountEggs = GetEgg();
        base.NewDay(out isDead);
    }

    private int GetEgg()
    {
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

    private int CalcEggChance()
    {
        Random rand = new Random();
        return rand.Next(1, 126);  // 25% шанc дать 2 яйца
    }

}