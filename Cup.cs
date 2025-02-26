namespace LiarsDice;

public class Cup
{
    private List<Dice> diceList; // Creates a list filled with dice objects
    public string Name; 
    private List<int> lastValues; 
    
    public Cup(string name)
    {
        Name = name;
        diceList = new List<Dice>();
        lastValues = new List<int>();
    }
    
    public void AddDice(Dice dice)
    {
        diceList.Add(dice);
    }

    public List<Dice> RollCup() // Method To roll cup
    {
        lastValues.Clear();
        for (int i = 0; i < diceList.Count; i++) // for the number of dice in the cup
        {
            lastValues.Add(diceList[i].Roll()); // adds a new value from each dice
        }
        return diceList;
    }
    
    public List<Dice> GetDice()
    {
        return diceList;
    }

    public int GetCupSize() // Helper method returns the number of dice in the cup
    {
        return diceList.Count;
    }

    public void RemoveDice() // Method removes the last dice in the list
    {
        diceList.RemoveAt(diceList.Count-1);
    }

    public void OutputDiceValues()
    {
        Console.WriteLine("Cup contents = {0}", string.Join(", ", lastValues));
        Console.ReadLine();
    }

}