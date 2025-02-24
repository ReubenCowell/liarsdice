namespace LiarsDice;

public class Cup
{
    //private List<Dice> diceList = new List<Dice>();
    private List<Dice> diceList;
    public string Name;
    
    public Cup(string name)
    {
        Name = name;
        diceList = new List<Dice>();
    }
    
    public void AddDice(Dice dice)
    {
        diceList.Add(dice);
    }

    public List<Dice> RollCup() // Method To roll cup
    {
        for (int i = 0; i < diceList.Count; i++)
        {
            diceList[i].Roll();
        }
        
        return diceList;
    }

}