using System.Linq;

namespace LiarsDice;

public class Game
{
    private Cup[] cupArray; // Class-level field to hold the cups for each player
    private int _numPlayers;
    private String[] _playerTypeArray;
    // todo: make player type an attribute of a cup class
    private int lastCallAmount = 0;
    private int lastCallValue = 0;
    
    
    public void NewGame()
    {
        // Welcomes user to game and establishes how many players are in game
        Console.Write("Welcome to Liars Dice!\nHow many players are playing >>> ");
        _numPlayers = Convert.ToInt32(Console.ReadLine());
        Console.Write("How many players would you like to be a computer? >>> ");
        int num_computers = Convert.ToInt32(Console.ReadLine());

        // TODO: Create error checking for above statement

        int human_players = _numPlayers - num_computers; // gets an int with the number of human players


        _playerTypeArray = new String[_numPlayers]; // Makes a public array of strings with either human or computer in
        // TODO: could make this a boolean list

        for (int i = 0; i < _numPlayers; i++) 
        {
            if (i < human_players)
            {
                _playerTypeArray[i] = "Human";
            }
            else
            {
                _playerTypeArray[i] = "Computer";
            }
        }

        cupArray = new Cup[_numPlayers]; // Creates array of cups with the length of numbers of players, accesible to the whole Game class

        for (int i = 0; i < _numPlayers; i++) // for each player, it will create a cup object with 5 dice in it
        {
            Console.WriteLine("Player " + (i + 1) + " - " + _playerTypeArray[i]);

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
        int playerChosen = random.Next(_numPlayers);
        Console.WriteLine("Player " + (playerChosen + 1) + " will start");
        return playerChosen;
    }

    public void RollAndDisplayDice()
    {
        Console.WriteLine("Dice rolled will now be shown one at a time for human players (Press enter when ready:) ");
        Console.ReadLine();

        for (int i = 0; i < _numPlayers; i++)
        {
            Console.Clear();
            Console.WriteLine("Player " + (i + 1)+" dice ready, press enter:");
            Console.ReadLine();

            cupArray[i].RollCup();
            Console.WriteLine($"You have {cupArray[i].GetCupSize()} dice in the cup");
            cupArray[i].OutputDiceValues();
        }

        /*for (int i = 0; i < num_players; i++)
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
        }*/
    }

    public int GetDiceOnTableAmount()
    {
        int diceOnTableAmount = 0;
        for (int i = 0; i < _numPlayers; i++)
        {
            diceOnTableAmount += cupArray[i].GetCupSize();
        }

        return diceOnTableAmount;
    }

    public int RoundLoop(int player)
    {
        Console.Clear();

        int nextPlayer;
        
        if (player == _numPlayers-1)
        {
            nextPlayer = _numPlayers + 1;
        }
        else
        {
            nextPlayer = player + 1;
        }
        
        if (_playerTypeArray[player] == "Human")
        {
            Console.Write($"Player {player+1}, would you like to call or challenge? >>> ");
            string playerChoice = Console.ReadLine();
            
            if (playerChoice == "call")
            {
                bool validCall = false;
                while (!validCall) // Makes sure call is valid - either a bigger value or a bigger number of dice
                {
                    Console.Write("Enter the value of the dice you would like to call >>> ");
                    int diceValue = Convert.ToInt32(Console.ReadLine());
                    Console.Write($"Enter the number of {diceValue} dice you would like to call for >>> ");
                    int diceAmount = Convert.ToInt32(Console.ReadLine());

                    if ((diceValue < lastCallValue && diceAmount < lastCallAmount) || diceValue > 6)
                    {
                        Console.WriteLine("Please enter a call with either a higher dice amount or value that the last round, or a valid dice value");
                        Console.ReadLine();
                    }
                    else
                    {
                        validCall = true;
                        lastCallAmount = diceAmount;
                        lastCallValue = diceValue;
                        Console.WriteLine($"Player {player+1} has called that there are {diceAmount} dice with a value of {diceValue} on the table\n\npress enter for {nextPlayer+1}'s turn");
                        Console.ReadLine();
                    }
                    Console.Clear();
                }
                
            }
            else if (playerChoice == "challenge")
            {
                int lastPlayer;
                if (player == 0)
                {
                    lastPlayer = _numPlayers;
                } else
                {
                    lastPlayer = player - 1;
                }
                Console.WriteLine($"Player {player+1} has made a challenge on player {lastPlayer+1}'s turn!\n\n Player {lastPlayer+1} called that there were {lastCallAmount} dice with a value of {lastCallValue} on the table. \nPress enter to find out how many there are");
                Console.ReadLine();

                int NumDice = GetDiceOnTableAmount();

                int[] diceValues = new int[NumDice];

                int buffer = 0;
                for (int i = 0; i < _numPlayers; i++)
                {
                    
                    for (int j = 0; j < cupArray[i].GetCupSize(); j++){
                        diceValues[buffer + j] = cupArray[i].GetDice().ElementAt(j).GetValue();
                    }
                    buffer += cupArray[i].GetCupSize();
                }
                
                //I now have an array with all the dice on the table in it

                int countOfChallengedValue = 0; // Gets a count of the number of dice with that value on the table
                foreach (int i in diceValues)
                {
                    if (diceValues[i] == lastCallValue)
                    {
                        countOfChallengedValue++;
                    }
                }

                if (countOfChallengedValue >= lastCallAmount)
                {
                    Console.WriteLine($"The challenge was incorrect.\n There were {countOfChallengedValue} dice with a value of {lastCallValue} on the table.\n\nPlayer {player+1} loses a dice \nPress enter for player {nextPlayer}'s turn");
                    cupArray[player].RemoveDice();
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"The challenge was correct. There were only {countOfChallengedValue} dice with a value of {lastCallValue} on the table. \n\nPlayer {lastPlayer+1} loses a dice\nPress enter for player {nextPlayer}'s turn");
                    cupArray[lastPlayer].RemoveDice();
                    Console.ReadLine();
                }
            }
        }
        else
        {
            Console.WriteLine("It is a computer's turn");
            //TODO: Create computer code
            
            int totalDice = GetDiceOnTableAmount();
            double riskTolerance = 0.6; // 60% chance to challenge if the bid is close to expected
            int averageDicePerPlayer = totalDice / _numPlayers;
            
            int expectedNumberOfDice = totalDice/6;
            bool playerIsBluffing = lastCallAmount > expectedNumberOfDice;

            if (playerIsBluffing && new Random().NextDouble() < riskTolerance)
            {
                //challenge the player
                int lastPlayer;
                if (player == 0)
                {
                    lastPlayer = _numPlayers;
                } else
                {
                    lastPlayer = player - 1;
                }
                Console.WriteLine($"Player {player+1} has made a challenge on player {lastPlayer+1}'s turn!\n\n Player {lastPlayer+1} called that there were {lastCallAmount} dice with a value of {lastCallValue} on the table. \nPress enter to find out how many there are");
                Console.ReadLine();

                int NumDice = GetDiceOnTableAmount();

                int[] diceValues = new int[NumDice];

                int buffer = 0;
                for (int i = 0; i < _numPlayers; i++)
                {
                    
                    for (int j = 0; j < cupArray[i].GetCupSize(); j++){
                        diceValues[buffer + j] = cupArray[i].GetDice().ElementAt(j).GetValue();
                    }
                    buffer += cupArray[i].GetCupSize();
                }
                
                //I now have an array with all the dice on the table in it

                int countOfChallengedValue = 0; // Gets a count of the number of dice with that value on the table
                foreach (int i in diceValues)
                {
                    if (diceValues[i] == lastCallValue)
                    {
                        countOfChallengedValue++;
                    }
                }

                if (countOfChallengedValue >= lastCallAmount)
                {
                    Console.WriteLine($"The challenge was incorrect.\n There were {countOfChallengedValue} dice with a value of {lastCallValue} on the table.\n\nPlayer {player+1} loses a dice \nPress enter for player {nextPlayer}'s turn");
                    cupArray[player].RemoveDice();
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"The challenge was correct. There were only {countOfChallengedValue} dice with a value of {lastCallValue} on the table. \n\nPlayer {lastPlayer+1} loses a dice\nPress enter for player {nextPlayer}'s turn");
                    cupArray[lastPlayer].RemoveDice();
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine($"Player {player+1}, a computer, has decided not to challenge, it is calling a new bid");
                
                
                
            }

            

        }

        return nextPlayer;
    }

    private bool CheckLoser(int player) // Returns true if the player has lost
    {
            if (cupArray[player].GetCupSize() <= 0)
            {
                // the person with this cup, who has 0 dice in it is the loser
                return true;
            }
            else
            {
                return false;
            }
    }

    public void GameLoop(int nextPlayer)
    {
        Console.WriteLine($"It will be player {nextPlayer+1}'s turn first"); // Start player picked by GameStartPicker in the program.cs file
        bool GameOver = false;

        while (!GameOver)
        {
            for (int i = 0; i < _numPlayers; i++)
            {
                if (CheckLoser(i))
                {
                    GameOver = true;
                    Console.WriteLine($"Player {i+1} has lost");
                }
            }
            
            
            Console.WriteLine("Dice are being rolled....");
            RollAndDisplayDice(); // rolls the dice in user's cups and displays them
            Console.Clear();
        
            // TODO: Make the first round different from the rest

            for (int i = 0; i < _numPlayers; i++)
            {
                nextPlayer = RoundLoop(nextPlayer);
            }


        }
        
        


    }
}

