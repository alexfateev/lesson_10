using pack1;

class Chicken
{
    private HungerLevel _hunger = HungerLevel.Feedup;
    private int _amountEggs = 0;
    private const int maxEgg = 10;
    public bool IsAlive { get; private set; } = true;

    public void Feedup()
    {
        _hunger = HungerLevel.Feedup;
    }

    public void NewDay(out bool isDead)
    {
        _amountEggs += GetEgg();
        switch (_hunger)
        {
            case HungerLevel.Feedup: _hunger = HungerLevel.LightHunger; break;
            case HungerLevel.LightHunger: _hunger = HungerLevel.MiddleHunger; break;
            case HungerLevel.MiddleHunger: _hunger = HungerLevel.StrongHunger; break;
            case HungerLevel.StrongHunger: IsAlive = false; break;
        }
        isDead = !IsAlive;
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