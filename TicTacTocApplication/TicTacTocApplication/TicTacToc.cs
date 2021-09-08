using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacTocApplication
{
    public delegate void Notify();  // delegate
    class TicTacToc
    {

        public char[] board;
        public char userMarker;
        protected char aiMarker;
        public char winner;
        public char currentMarker;
        //public event Notify ProcessCompleted; // event
        public event EventHandler<string> ProcessCompleted;
        public TicTacToc(char playerToken, char aiToken)
        {
            this.userMarker = playerToken;
            this.aiMarker = aiToken;
            this.winner = '-';
            this.board = setBoard();
            this.currentMarker = playerToken;
        }

        private static char[] setBoard()
        {
            char[] board = new char[9];
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = '-';
            }
            return board;
        }
        public Boolean playTurn(int spot)
        {
            Boolean isValid = withinRange(spot) && !isSpotTaken(spot);
            if (isValid)
            {
                board[spot - 1] = currentMarker;
                currentMarker = (currentMarker == userMarker) ? aiMarker : userMarker;
            }
            return (isValid);
        }

        private Boolean withinRange(int spot)
        {
            return spot > 0 && spot <= board.Length;
        }

        private Boolean isSpotTaken(int spot)
        {
            return board[spot - 1] != '-';
        }
        public void printBoard()
        {
            Console.WriteLine();
            for (int i = 0; i < board.Length; i++)
            {
                if (i % 3 == 0 && i != 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("---------------");
                }
                Console.Write(" | " + board[i]);
            }
            Console.WriteLine();
        }

        public void printIndexBoard()
        {
            Console.WriteLine();
            for (int i = 0; i < board.Length; i++)
            {
                if (i % 3 == 0 && i != 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("---------------");
                }
                Console.Write(" | " + (i + 1));
            }
            Console.WriteLine();
        }



        public Boolean isThereAWinner()
        {
            Boolean diagonalsAndMiddels = (rightDi() || leftDi() || middleRow() || secondCol()) && board[4] != '-';
            Boolean topAndFirst = (topRow() || firstCol()) && board[0] != '-';
            Boolean bottomAndTherd = (bottomRow() || thirdCol()) && board[8] != '-';
            if (diagonalsAndMiddels)
            {
                this.winner = board[4];
            }
            else if (topAndFirst)
            {
                this.winner = board[0];
            }
            else if (bottomAndTherd)
            {
                this.winner = board[8];
            }
            return diagonalsAndMiddels || topAndFirst || bottomAndTherd;
        }

        private Boolean rightDi()
        {
            return board[0] == board[4] && board[4] == board[8];
        }

        private Boolean leftDi()
        {
            return board[2] == board[4] && board[4] == board[6];
        }

        private Boolean middleRow()
        {
            return board[3] == board[4] && board[4] == board[5];
        }

        private Boolean topRow()
        {
            return board[0] == board[1] && board[1] == board[2];
        }

        private Boolean firstCol()
        {
            return board[0] == board[3] && board[3] == board[6];
        }
        private Boolean secondCol()
        {
            return board[1] == board[4] && board[4] == board[7];
        }
        private Boolean bottomRow()
        {
            return board[6] == board[7] && board[7] == board[8];
        }

        private Boolean thirdCol()
        {
            return board[2] == board[5] && board[5] == board[8];
        }

        public Boolean isTheBoardFilled()
        {
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == '-')
                    return false;
            }
            return true;
        }

        public String gameOver()
        {
            Boolean didSomeoneWin = isThereAWinner();
            if (didSomeoneWin)
            {
                return "we have a winner ! the winner is " + this.winner + " 's";
            }
            else if (isTheBoardFilled())
                return "Draw: Game Over!";
            else
                return "notOver";
        }

        public void StartProcess()
        {
            Boolean didSomeoneWin = isThereAWinner();
            if (didSomeoneWin)
            {
                OnProcessCompleted("we have a winner ! the winner is " + this.winner + " 's");
               
            }
            else if (isTheBoardFilled())
                OnProcessCompleted("Draw: Game Over!");
            else
                OnProcessCompleted("notOver");
            
        }
        public virtual void OnProcessCompleted(string state) //protected virtual method
        {
            //if ProcessCompleted is not null then call delegate
            ProcessCompleted?.Invoke(this, state);
        }
    }
}
