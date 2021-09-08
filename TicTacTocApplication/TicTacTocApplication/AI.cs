using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacTocApplication
{
    class AI
    {
        public int pickSpot(TicTacToc game)
        {
            List<int> choises = new List<int>();
            // ArrayList<int> choises = new ArrayList();
            for (int i = 0; i < 9; i++)
            {
                if (game.board[i] == '-')
                    choises.Add(i + 1);
            }
            Random rand = new Random();
            int choice = choises.ElementAt(Math.Abs(rand.Next() % choises.Count()));
            return choice;
        }
    }
}
