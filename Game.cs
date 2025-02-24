namespace LiarsDice;

public class Game
{
    private Cup[] cupArray; // Class-level field to hold the cups for each player
    
    public void NewGame()
    {
        // Welcomes user to game and establishes how many players are in game
        Console.Write("Welcome to Liars Dice!\nHow many players are playing >>> ");
        int num_players = Convert.ToInt32(Console.ReadLine());
        Console.Write("How many players would you like to be a computer? >>> ");
        int num_computers = Convert.ToInt32(Console.ReadLine());
        
        // TODO: Create error checking for above statement

        int human_players = num_players - num_computers; // gets an int with the number of human players
        
        
        String[] playerTypeArray = new String[num_players]; // Makes an array of strings of different 
        
        for (int i = 0; i < num_players; i++)
        {
            if (i <= human_players)
            {
                playerTypeArray[i] = "Human";
            }
            else
            {
                playerTypeArray[i] = "Computer";
            }
        }
        
        cupArray = new Cup[num_players]; // Creates array of cups with the length of numbers of players

        for (int i = 0; i < num_players; i++) // for each player, it will create a cup object with 5 dice in it
        {
            Console.WriteLine("Player " + (i + 1) + " - " + playerTypeArray[i]);
            
            string cupName = $"Cup{i + 1}";
            cupArray[i] = new Cup(cupName);
            
            for (int k = 0; k < 5; k++) // 5 times
            {
                Dice dice = new Dice(6); // creates a die with 6 sides
                cupArray[i].AddDice(dice);
            }

            Console.WriteLine(cupArray[i].Name);
        }

    }

}