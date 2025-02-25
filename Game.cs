namespace LiarsDice;

public class Game
{
    private Cup[] cupArray; // Class-level field to hold the cups for each player
    private int num_players;
    private String[] playerTypeArray;

    public void NewGame()
    {
        // Welcomes user to game and establishes how many players are in game
        Console.Write("Welcome to Liars Dice!\nHow many players are playing >>> ");
        num_players = Convert.ToInt32(Console.ReadLine());
        Console.Write("How many players would you like to be a computer? >>> ");
        int num_computers = Convert.ToInt32(Console.ReadLine());

        // TODO: Create error checking for above statement

        int human_players = num_players - num_computers; // gets an int with the number of human players


        playerTypeArray = new String[num_players]; // Makes a public array of strings with either human or computer in
        // TODO: could make this a boolean list

        for (int i = 0; i < num_players; i++) 
        {
            if (i < human_players)
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
                string diceName = $"Dice{k + 1}";
                
                Dice dice1 = new Dice(6);
                //Dice dice = new Dice(6); // creates a die with 6 sides
                cupArray[i].AddDice(dice1);
            }
        }

    }

    public int GameStartPicker()
    {
        Console.WriteLine("Starting player will be selected at random:");
        Random random = new Random();
        int playerChosen = random.Next(num_players);
        Console.WriteLine("Player " + (playerChosen + 1) + " will start");
        return playerChosen;
    }

    public void DiceShowing()
    {
        Console.WriteLine("Dice rolled will now be shown one at a time for human players (Press enter when ready:) ");
        Console.ReadLine();

        for (int i = 0; i < num_players; i++)
        {
            Console.Clear();
            Console.WriteLine("Player " + (i + 1)+" dice ready, press enter:");
            Console.ReadLine();
            foreach (var die in cupArray[i].GetDice())
            {
                Console.WriteLine("     Result: " + die.Roll());
            }

            Console.WriteLine("\n Press enter when ready to move on");
            Console.ReadLine();
        }
    }

    public int RoundLoop(int player)
    {
        Console.Clear();
        if (playerTypeArray[player] == "Human")
        {
            Console.Write($"Player {player}, would you like to call or challenge? >>> ");
            string playerChoice = Console.ReadLine();
            
            if (playerChoice == "call")
            {
                Console.Write("Enter the value of the dice you would like to call >>> ");
                int diceValue = Convert.ToInt32(Console.ReadLine());
                Console.Write($"Enter the number of {diceValue} dice you would like to call for >>> ");
                int diceAmount = Convert.ToInt32(Console.ReadLine());

                Console.Clear();
                Console.WriteLine($"Player {player} has called that there are {diceAmount} dice with a value of {diceValue} on the table");
            }
            else if (playerChoice == "challenge")
            {
                Console.WriteLine("challenge");
                // TODO: Create challenge code
            }
        }
        else
        {
            Console.WriteLine("It is a computer's turn");
        }

        return player + 1;
    }

    public void GameLoop(int startPlayer)
    {
        Console.WriteLine("Dice are being rolled....");
        DiceShowing();
        Console.Clear();
        Console.WriteLine($"Player {startPlayer}'s turn:");
        // TODO: Make the first round different from the rest
        int nextPlayer = RoundLoop(startPlayer);
        
        
        


    }
}

