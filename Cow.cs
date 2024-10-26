using pack1;

class Cow
{
    private HungerLevel _hunger = HungerLevel.Feedup;
    private bool _isAlive = true;
    private int _amountMilk = 0;

    public bool IsAlive()
    {
        return _isAlive;
    }

    public void Feedup()
    {
        _hunger = HungerLevel.Feedup;
    }

    public void NewDay(out bool isDead)
    {
        _amountMilk = GetMilk();
        switch (_hunger)
        {
            case HungerLevel.Feedup: _hunger = HungerLevel.LightHunger; break;
            case HungerLevel.LightHunger: _hunger = HungerLevel.MiddleHunger; break;
            case HungerLevel.MiddleHunger: _hunger = HungerLevel.StrongHunger; break;
            case HungerLevel.StrongHunger: _isAlive = false; break;
        }
        isDead = !_isAlive;
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