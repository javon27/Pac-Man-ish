using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pac_Man_ish
{
    class Enemy: IGameObject, IGameActor
    {
        protected static int counter = 0;
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
        public float fX
        {
            get; set;
        }
        public float fY
        {
            get; set;
        }
        public Vector V
        {
            get; set;
        }
        protected Thread p_thread;
        public PlayArea Board;
        public Game Parent
        {
            get; set;
        }

        public Enemy (char _icon, ConsoleColor color, int x, int y, Game game) 
        {
            Id = Interlocked.Increment(ref counter);
            fX = LastX = X = x;
            fY = LastY = Y = y;
            Icon = _icon;
            Alive = true;
            Color = color;
            Parent = game;
            Board = Parent.board;
            Random rand = new Random(DateTime.Now.Millisecond);
            V = (Vector)(rand.Next() % 4);
            Board[X, Y] = this;
        }

        private Vector VectorToPlayer()
        {
            Player p1 = Parent.p1;
            int dx = p1.X - X;
            int dy = p1.Y - Y;
            if (dy > dx)
            {
                if (dy < 0)
                {
                    return Vector.UP;
                }
                else
                {
                    return Vector.DOWN;
                }
            }
            else
            {
                if (dx < 0)
                {
                    return Vector.LEFT;
                }
                else
                {
                    return Vector.RIGHT;
                }
            }
        }

        private void Move()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            do
            {
                float fx = fX;
                float fy = fY;
                float SPEED = Program.options.enemySpeed;
                switch (V)
                {
                    case Vector.UP:
                        fy = fY - SPEED;
                        break;
                    case Vector.DOWN:
                        fy = fY + SPEED;
                        break;
                    case Vector.LEFT:
                        fx = fX - SPEED;
                        break;
                    case Vector.RIGHT:
                        fx = fX + SPEED;
                        break;
                }
                int x = (int)Math.Round(fx, 0);
                int y = (int)Math.Round(fy, 0);
                if (V == Vector.UP && y == 10 && (x == 10 || x == 11 || x == 12))
                {
                    y = 9;
                }
                if (Board[x, y] != this && (Board[x, y] is StationaryObject || Board[x, y] is IGameActor))
                {
                    V = (Vector)(rand.Next() % 4);
                }
                else
                {
                    Board[X, Y] = null;
                    Board[x, y] = this;
                    fX = fx;
                    fY = fy;
                    X = x;
                    Y = y;
                }
                Thread.Sleep(Game.TICK);
            } while (Alive);
        }

        public void Start()
        {
            Alive = true;
            if (p_thread == null)
            {
                p_thread = new Thread(Move);
                p_thread.Start();
            }
        }
        
        public void Stop()
        {
            Alive = false;
            p_thread = null;
        }
    }
}
