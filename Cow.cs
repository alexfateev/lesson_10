using lesson_10;
using pack1;

class Cow : Animal
{
    public Cow(AccountHandler ev) : base(ev)
    {
    }

    public override int CollectHarvest()
    {
        if (_harvestToday) return 0; //Если сегодня собирали урожай
        _harvestToday = true;
        Random rand = new Random();
        switch (_hunger)
        {
            case HungerLevel.Feedup: return rand.Next(14, 21);
            case HungerLevel.LightHunger: return rand.Next(9, 13);
            case HungerLevel.MiddleHunger: return rand.Next(1, 5);
        }
        return 0;
    }

}