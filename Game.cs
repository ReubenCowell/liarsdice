namespace LiarsDice;

public class Game
{
    private Cup[] _cupArray = null!; // Class-level field to hold the cups for each player
    private int _numPlayers; // Number of players in game
    private int _lastCallAmount; // The amount of dice called in last throw
    private int _lastCallValue; // The value of the dice called in last throw
    private readonly Random _random = new Random();
    
    
    public void NewGame()
    {
        // Welcomes user to game and establishes how many players are in game
        Console.Write("Welcome to Liars Dice!\nHow many players are playing >>> ");
        while (!int.TryParse(Console.ReadLine(), out _numPlayers) || _numPlayers <= 0)
        {
            Console.Write("Invalid input. Please enter a positive integer for players: ");
        }
        Console.Write("How many players would you like to be a computer? >>> ");
        int numComputers;
        while (!int.TryParse(Console.ReadLine(), out numComputers) || numComputers <= 0 || numComputers > _numPlayers)
        {
            Console.Write("Invalid input. Please enter a positive integer larger than the number of players: ");
        }

        int humanPlayers = _numPlayers - numComputers; // gets an int with the number of human players
        _cupArray = new Cup[_numPlayers]; // Creates array of cups with the length of numbers of players, accessible to the whole Game class
        Console.WriteLine();
        for (int i = 0; i < _numPlayers; i++) // for each player, it will create a cup object with 5 dice in it
        {
            if (i < humanPlayers) // Creates cups for each player with the correct type
            {
                _cupArray[i] = new Cup(PlayerType.Human);
            }
            else
            {
                _cupArray[i] = new Cup(PlayerType.Computer);
            }
            Console.WriteLine("     Player " + (i + 1) + " - " + _cupArray[i].ownedBy);
            
            for (int k = 0; k < 5; k++) // 5 times
            {
                _cupArray[i].AddDice(new Dice(6));
            }
        }
        Console.WriteLine();
    }

    public int GameStartPicker()
    {
        int playerChosen = _random.Next(_numPlayers);
        Console.WriteLine("Player " + (playerChosen + 1) + " will start.\n\n");
        return playerChosen;
    }

    private void RollAndDisplayDice()
    {
        Console.WriteLine("Dice rolled will now be shown one at a time for human players (Press enter when ready:) ");
        Console.ReadLine();

        for (int i = 0; i < _numPlayers; i++)
        {
            _cupArray[i].RollCup();
            if (_cupArray[i].ownedBy == PlayerType.Human)
            {
                Console.Clear();
                Console.WriteLine("Player " + (i + 1)+" dice ready, press enter:");
                Console.ReadLine();
                Console.WriteLine($"You have {_cupArray[i].GetCupSize()} dice in the cup");
                _cupArray[i].OutputDiceValues();
            }
        }
        Console.Clear();
    }

    private int GetDiceOnTableAmount()
    {
        int diceOnTableAmount = 0;
        for (int i = 0; i < _numPlayers; i++)
        {
            diceOnTableAmount += _cupArray[i].GetCupSize();
        }

        return diceOnTableAmount;
    }

    private int RoundLoop(int player)
    {
        Console.Clear();
        int nextPlayer; // sets a next player variable to be returned at the end of the round
        
        if (player == _numPlayers-1)
        {
            nextPlayer = 0;
        }
        else
        {
            nextPlayer = player + 1;
        }
        
        if (_cupArray[player].ownedBy == PlayerType.Human)
        {
            Console.Write($"Player {player+1}, would you like to call or challenge? >>> ");
            string playerChoice = Console.ReadLine();
            // TODO: Add error handling here
            
            if (playerChoice == "call")
            {
                bool validCall = false;
                while (!validCall) // Makes sure call is valid - either a bigger value or a bigger number of dice
                {
                    Console.Write("Enter the value of the dice you would like to call >>> ");
                    int diceValue = Convert.ToInt32(Console.ReadLine());
                    Console.Write($"Enter the number of {diceValue} dice you would like to call for >>> ");
                    int diceAmount = Convert.ToInt32(Console.ReadLine());

                    if ((diceValue < _lastCallValue && diceAmount < _lastCallAmount) || diceValue > 6)
                    {
                        Console.WriteLine("Please enter a call with either a higher dice amount or value that the last round, or a valid dice value");
                        Console.ReadLine();
                    }
                    else
                    {
                        validCall = true;
                        _lastCallAmount = diceAmount;
                        _lastCallValue = diceValue;
                        Console.WriteLine($"Player {player+1} has called that there are {diceAmount} dice with a value of {diceValue} on the table\n\npress enter for {nextPlayer+1}'s turn");
                        Console.ReadLine();
                    }
                    Console.Clear();
                }

                return nextPlayer;

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
                Console.WriteLine($"Player {player+1} has made a challenge on player {lastPlayer+1}'s turn!\n\n Player {lastPlayer+1} called that there were {_lastCallAmount} dice with a value of {_lastCallValue} on the table. \nPress enter to find out how many there are");
                Console.ReadLine();

                int numDice = GetDiceOnTableAmount();

                int[] diceValues = new int[numDice];

                int buffer = 0;
                for (int i = 0; i < _numPlayers; i++)
                {
                    
                    for (int j = 0; j < _cupArray[i].GetCupSize(); j++){
                        diceValues[buffer + j] = _cupArray[i].GetDice().ElementAt(j).GetValue();
                    }
                    buffer += _cupArray[i].GetCupSize();
                }
                
                //I now have an array with all the dice on the table in it

                int countOfChallengedValue = 0; // Gets a count of the number of dice with that value on the table
                foreach (int i in diceValues)
                {
                    if (diceValues[i] == _lastCallValue)
                    {
                        countOfChallengedValue++;
                    }
                }

                if (countOfChallengedValue >= _lastCallAmount)
                {
                    Console.WriteLine($"The challenge was incorrect.\n There were {countOfChallengedValue} dice with a value of {_lastCallValue} on the table.\n\nPlayer {player+1} loses a dice \nPress enter for player {nextPlayer}'s turn");
                    _cupArray[player].RemoveDice();
                    Console.ReadLine();
                    return lastPlayer;
                }
                else
                {
                    Console.WriteLine($"The challenge was correct. There were only {countOfChallengedValue} dice with a value of {_lastCallValue} on the table. \n\nPlayer {lastPlayer+1} loses a dice\nPress enter for player {nextPlayer}'s turn");
                    _cupArray[lastPlayer].RemoveDice();
                    Console.ReadLine();
                    return player;
                }
            }
        }
        else
        {
            Console.WriteLine("It is a computer's turn");
            //TODO: Create computer code
            
            int totalDice = GetDiceOnTableAmount();
            double riskTolerance = 0.6; // 60% chance to challenge if the bid is close to expected
            
            int expectedNumberOfDice = totalDice/6;
            bool playerIsBluffing = _lastCallAmount > expectedNumberOfDice;

            if ((playerIsBluffing && new Random().NextDouble() < riskTolerance) && _lastCallAmount != 0)
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
                Console.WriteLine($"Player {player+1} has made a challenge on player {lastPlayer+1}'s turn!\n\n Player {lastPlayer+1} called that there were {_lastCallAmount} dice with a value of {_lastCallValue} on the table. \nPress enter to find out how many there are");
                Console.ReadLine();

                int numDice = GetDiceOnTableAmount();

                int[] diceValues = new int[numDice];

                int buffer = 0;
                for (int i = 0; i < _numPlayers; i++)
                {
                    
                    for (int j = 0; j < _cupArray[i].GetCupSize(); j++){
                        diceValues[buffer + j] = _cupArray[i].GetDice().ElementAt(j).GetValue();
                    }
                    buffer += _cupArray[i].GetCupSize();
                }
                
                //I now have an array with all the dice on the table in it

                int countOfChallengedValue = 0; // Gets a count of the number of dice with that value on the table
                foreach (int i in diceValues)
                {
                    if (diceValues[i] == _lastCallValue)
                    {
                        countOfChallengedValue++;
                    }
                }

                if (countOfChallengedValue >= _lastCallAmount)
                {
                    Console.WriteLine($"The challenge was incorrect.\n There were {countOfChallengedValue} dice with a value of {_lastCallValue} on the table.\n\nPlayer {player+1} loses a dice \nPress enter for player {nextPlayer}'s turn");
                    _cupArray[player].RemoveDice();
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"The challenge was correct. There were only {countOfChallengedValue} dice with a value of {_lastCallValue} on the table. \n\nPlayer {lastPlayer+1} loses a dice\nPress enter for player {nextPlayer}'s turn");
                    _cupArray[lastPlayer].RemoveDice();
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine($"Player {player+1}, a computer, has decided not to challenge, it is calling a new bid");
                int amountIncrease;
                if (expectedNumberOfDice >= 5)
                {
                    amountIncrease = 3;
                }
                else if (expectedNumberOfDice >= 3)
                {
                    amountIncrease = 2;
                }
                else
                {
                    amountIncrease = 1;
                }

                bool valueIncrease = (new Random().NextDouble() > 0.7) && _lastCallValue <= 6; // 30% chance to change the value as well

                _lastCallAmount += amountIncrease;
                if (valueIncrease || _lastCallValue == 0)
                {
                    _lastCallValue += 1;
                }

                Console.WriteLine($"They have called {_lastCallAmount} dice with a value of {_lastCallValue}\n\nPress enter to move on");
                Console.ReadLine();

            }

        }

        return nextPlayer;
    }

    private bool CheckLoser(int player) // Returns true if the player has lost
    {
            if (_cupArray[player].GetCupSize() <= 0)
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
        bool gameOver = false;
        
        while (!gameOver)
        {
            for (int i = 0; i < _numPlayers; i++)
            {
                if (CheckLoser(i))
                {
                    //TODO: add what happens when someone runs out of dice
                    gameOver = true;
                    Console.WriteLine($"Player {i+1} has lost");
                }
            }
            
            // First round setup
            RollAndDisplayDice(); // rolls the dice in user's cups and displays them
            
            // TODO: Make the first round different from the rest
            
            int diceAmount = GetDiceOnTableAmount();
            bool gameRunning = true;

            while (gameRunning)
            {
                nextPlayer = RoundLoop(nextPlayer);

                if (GetDiceOnTableAmount() < diceAmount) // If the number of dice has decreased in the round
                {
                    Console.WriteLine("Round over, new round beginning");
                    RollAndDisplayDice(); // rolls the dice in user's cups and displays them
                }
                // TODO make round go until somebody loses

                for (int i = 0; i < _numPlayers; i++)
                {
                    if (CheckLoser(i))
                    {
                        Console.WriteLine($"Player {i+1} has lost, they are out of the game");
                    }
                }
            }
        }
    }
}


