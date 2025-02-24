namespace LiarsDice;

public class Game
{
    public void NewGame()
    {
        // Welcomes user to game and establishes how many players are in game
        Console.Write("Welcome to Liars Dice!\nHow many players are playing >>> ");
        int num_players = Convert.ToInt32(Console.ReadLine());
        Console.Write("How many players would you like to be a computer? >>> ");
        int num_computers = Convert.ToInt32(Console.ReadLine());
        
        // TODO: Create error checking for above statement
        
        Cup playerCup = new Cup(); 
        for (int i = 0; i < 5; i++)
        {
            Dice dice = new Dice(6);
            playerCup.AddDice(dice);
        }
    }

}