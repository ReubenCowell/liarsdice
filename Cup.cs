namespace LiarsDice;


public enum PlayerType
{
    Human,
    Computer
}


public class Cup
{
    private List<Dice> _diceList; // Creates a list filled with dice objects
    private List<int> _lastValues; 
    public PlayerType OwnedBy;
    public string Name;
    
    public Cup(PlayerType type, string name) // Constructor class
    {
        Name = name;
        OwnedBy = type;
        _diceList = new List<Dice>();
        _lastValues = new List<int>();
    }
    
    public void AddDice(Dice dice) // Adds a dice to the dicelist
    {
        _diceList.Add(dice);
    }

    public List<Dice> RollCup() // Method To roll cup
    {
        _lastValues.Clear();
        for (int i = 0; i < _diceList.Count; i++) // for the number of dice in the cup
        {
            _lastValues.Add(_diceList[i].Roll()); // adds a new value from each dice
        }
        return _diceList;
    }
    
    public List<Dice> GetDice() // Returns the dice list
    {
        return _diceList;
    }

    public int GetCupSize() // Helper method returns the number of dice in the cup as an int
    {
        return _diceList.Count;
    }

    public void RemoveDice() // Method removes the last dice in the list
    {
        _diceList.RemoveAt(_diceList.Count-1);
    }

    public void OutputDiceValues()
    {
        Console.WriteLine("Cup contents = {0}", string.Join(", ", _lastValues));
        Console.ReadLine();
    }

    public string PlayerName() // Returns the player name as a string
    {
        return Name;
    }

}