namespace LiarsDice;

public class Cup
{
    private List<Dice> diceList;
    public string Name;
    private int[] lastValues;
    
    public Cup(string name)
    {
        Name = name;
        diceList = new List<Dice>();
        lastValues = new int[5];
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
    
    public List<Dice> GetDice()
    {
        return diceList;
    }

}