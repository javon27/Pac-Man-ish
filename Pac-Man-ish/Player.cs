using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pac_Man_ish
{
    class Player: IGameObject, IGameActor
    {
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

        public Vector LastV
        {
            get; set;
        }

        protected Thread p_thread;
        public PlayArea Board
        {
            get; set;
        }

        public Player(char _icon, ConsoleColor color, int x, int y, PlayArea board)
        {
            fX = LastX = X = x;
            fY = LastY = Y = y;
            Icon = _icon;
            Alive = true;
            Color = color;
            Board = board;
            V = Vector.STOP;
            Board[X, Y] = this;
        }
        
        private void Move()
        {
            do
            {
                float fx = fX;
                float fy = fY;
                int x;
                int y;
                float SPEED = Program.options.playerSpeed;
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
                        x = (int)Math.Round(fx, 0);
                        if (Y == 12 && x == 0)
                        {
                            fx = 21f;
                        }
                        break;
                    case Vector.RIGHT:
                        fx = fX + SPEED;
                        x = (int)Math.Round(fx, 0);
                        if (Y == 12 && x == 22)
                        {
                            fx = 1f;
                        }
                        break;
                }
                x = (int)Math.Round(fx, 0);
                y = (int)Math.Round(fy, 0);
                if (Board[x, y] != this && (Board[x, y] != null || x < 1 || x >= Board.Right || y < 1 || y >= Board.Bottom))
                {
                    if (V != LastV)
                    {
                        fx = fX;
                        fy = fY;
                        switch (LastV)
                        {
                            case Vector.UP:
                                fy = fY - SPEED;
                                break;
                            case Vector.DOWN:
                                fy = fY + SPEED;
                                break;
                            case Vector.LEFT:
                                fx = fX - SPEED;
                                x = (int)Math.Round(fx, 0);
                                if (Y == 12 && x == 0)
                                {
                                    fx = 21f;
                                }
                                break;
                            case Vector.RIGHT:
                                fx = fX + SPEED;
                                x = (int)Math.Round(fx, 0);
                                if (Y == 12 && x == 22)
                                {
                                    fx = 1f;
                                }
                                break;
                        }
                        x = (int)Math.Round(fx, 0);
                        y = (int)Math.Round(fy, 0);
                        if (Board[x, y] != this && (Board[x, y] != null || x < 1 || x >= Board.Right || y < 1 || y >= Board.Bottom))
                        {
                            V = Vector.STOP;
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
                    }
                }
                else
                {
                    LastV = V;
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
            LastV = V;
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

        ~Player()
        {
            Alive = false;
        }
    }
}
