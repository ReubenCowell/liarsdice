namespace LiarsDice;

public class Dice
{
    // Creates public attributes of DIce
    private int _value;
    private readonly int _sides;
    
    Random _rnd = new Random();

    public Dice(int numSides) // The constructor method of the dice
    {
        _sides = numSides;
    }

    public int GetValue() // Returns the last value of the dice
    {
        return _value;
    }

    public int Roll() // Method for rolling dice
    {
        _value = _rnd.Next(_sides) + 1; // Selects a value equal between 1 and the number of sides of the dice
        return _value;
    }
}