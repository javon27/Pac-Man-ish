using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pac_Man_ish
{
    class Player: IGameObject
    {        
        private static int counter = 0;
        public int Id
        {
            get; private set;
        }
        public bool Alive
        {
            get; set;
        }
        public ConsoleColor Color
        {
            get; set;
        }
        public char Icon
        {
            get; set;
        }
        public int X
        {
            get; set;
        }
        public int Y
        {
            get; set;
        }
        public int LastX
        {
            get; set;
        }
        public int LastY
        {
            get; set;
        }
        public Vector v
        {
            get; set;
        }
        private Thread p_thread;
        public PlayArea Board
        {
            get; set;
        }

        public Player(char _icon, ConsoleColor color, int x, int y, PlayArea board)
        {
            Id = Interlocked.Increment(ref counter);
            X = x;
            Y = y;
            LastX = x;
            LastY = y;
            Icon = _icon;
            Alive = true;
            Color = color;
            Board = board;
            v = Vector.STOP;
        }
        
        private void Move()
        {
            do
            {
                int x = X;
                int y = Y;
                switch (v)
                {
                    case Vector.UP:
                        y = Y - 1;
                        break;
                    case Vector.DOWN:
                        y = Y + 1;
                        break;
                    case Vector.LEFT:
                        x = X - 1;
                        break;
                    case Vector.RIGHT:
                        x = X + 1;
                        break;
                }
                if (Board[x, y] != null || x < 1 || x >= Board.Right || y < 1 || y >= Board.Bottom) {
                    v = Vector.STOP;
                }
                else
                {
                    X = x;
                    Y = y;
                }
                Thread.Sleep(Game.TICK);
            } while (Alive);
        }

        public void Start()
        {
            if (p_thread == null)
            {
                p_thread = new Thread(Move);
                p_thread.Start();
            }
        }

        ~Player()
        {
            Alive = false;
        }
    }
}
