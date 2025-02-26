namespace LiarsDice;

class Program
{
    static void Main()
    {
        Game game = new Game(); // Creates new game object with name game
        game.NewGame(); // Runs the NewGame Method
        
        game.GameLoop(game.GameStartPicker()); // Runs the game loop, passing into to it a random player to start the round.
    }
}