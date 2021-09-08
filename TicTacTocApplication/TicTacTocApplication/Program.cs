using System;

namespace TicTacTocApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
           // Scanner sc = new Scanner(System.in);
            Boolean doYouWantToPlay = true;
            while (doYouWantToPlay)
            {
                Console.WriteLine("welcome\n"
                                    + "must pick charecter you want to be and which charcter i will be\n");
                Console.WriteLine("Enter a single charcter thatwill represent you on the board");
                char playerToken = char.Parse( Console.ReadLine());
                Console.WriteLine("Enter a single charcter thatwill represent AI on the board");
                char aiToken = char.Parse(Console.ReadLine());
                TicTacToc game = new TicTacToc(playerToken, aiToken);
                AI ai = new AI();
                Console.WriteLine();
                Console.WriteLine("pick number from board");
                game.printIndexBoard();
                Console.WriteLine();
                game.ProcessCompleted += game_ProcessCompleted; // register with an event
                
                while (game.gameOver().Equals("notOver"))
                {
                    if (game.currentMarker == game.userMarker)
                    {
                        Console.WriteLine("Its your turn Enter a spot for your token");
                        int spot = Int32.Parse(Console.ReadLine());
                        while (!game.playTurn(spot))
                        {
                            Console.WriteLine("Try again. " + spot + " is invalid");
                            spot = Int32.Parse(Console.ReadLine());
                        }
                        Console.WriteLine("You picked. " + spot);
                    }
                    else
                    {
                        Console.WriteLine("Its my turn");
                        int aiSpot = ai.pickSpot(game);
                        game.playTurn(aiSpot);
                        Console.WriteLine("I picked. " + aiSpot + "!");
                    }
                    Console.WriteLine();
                    game.printBoard();
                    game.StartProcess();
                }
                //Console.WriteLine(game.gameOver());
                Console.WriteLine("Do you want to play again Enter Y / anything else");
                char response = char.Parse(Console.ReadLine());
                if (response == 'Y' || response == 'y')
                    doYouWantToPlay = true;
                else
                    doYouWantToPlay = false;

            }

        }
        // event handler
        public static void game_ProcessCompleted(object sender, string state)
        {
            Console.WriteLine("Process : " + state );

        }
    }
}
