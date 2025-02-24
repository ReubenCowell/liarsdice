namespace LiarsDice;

public class Cup
{
    private List<Dice> _cup = new List<Dice>();

    public void AddDice(Dice dice)
    {
        _cup.Add(dice);
    }

    public int RollCup()
    {
        for (int i = 0; i < _cup.Count; i++)
        {
            _cup[i].Roll();
        }

        return 7;
    }

}