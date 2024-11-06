using lesson_10;
using pack1;

class Cow : Animal
{

    public override void NewDay(out bool isDead)
    {
        _amount = GetMilk();
        base.NewDay(out isDead);
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

}