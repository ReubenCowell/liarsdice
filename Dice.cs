namespace LiarsDice;

public class Dice
{
    // Creates public attributes of DIce
    public int value;
    public int sides;
    
    Random rnd = new Random();

    public Dice(int numSides) // The constructor method of the dice
    {
        sides = numSides;
    }

    public int GetValue() // Returns the last value of the dice
    {
        return value;
    }

    public int Roll() // Method for rolling dice
    {
        value = rnd.Next(sides) + 1; // Selects a value equal between 1 and the number of sides of the dice
        return value;
    }
}