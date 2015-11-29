using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_ish
{
    class PlayArea
    {
        private static char TL = '╔';
        private static char TR = '╗';
        private static char BL = '╚';
        private static char BR = '╝';
        private static char H = '═';
        private static char V = '║';
        private static char W = '█';
        public int Right
        {
            get; set;
        }
        public int Bottom
        {
            get; set;
        }
        public object[,] Board
        {
            get; private set;
        }
        public List<Position> EmptyAreas;
        
        public PlayArea()
        {
            Right = Properties.Settings.Default.windowWidth-2;
            Bottom = Properties.Settings.Default.windowHeight-5;
            Board = new object[Right+1, Bottom+1];
            FillBoard();
            GetEmptyAreas();
        }

        private void GetEmptyAreas()
        {
            EmptyAreas = new List<Position>();
            for (var i = 1; i < Right; i++)
            {
                for (var j = 1; j < Bottom; j++)
                {
                    if (Board[i, j] == null)
                    {
                        Position temp = new Position(i, j);
                        EmptyAreas.Add(temp);
                    }
                }
            }
        }

        private void FillBoard()
        {
            Board[0, 0] = new StationaryObject(TL, ConsoleColor.White, 0, 0);
            Board[Right, 0] = new StationaryObject(TR, ConsoleColor.White, Right, 0);
            Board[0, Bottom] = new StationaryObject(BL, ConsoleColor.White, 0, Bottom);
            Board[Right, Bottom] = new StationaryObject(BR, ConsoleColor.White, Right, Bottom);
            for (var i = 1; i < Right; i++)
            {
                Board[i, 0] = new StationaryObject(H, ConsoleColor.White, i, 0);
                Board[i, Bottom] = new StationaryObject(H, ConsoleColor.White, i, Bottom);
            }
            for (var i = 1; i < Bottom; i++)
            {
                Board[0, i] = new StationaryObject(V, ConsoleColor.White, 0, i);
                Board[Right, i] = new StationaryObject(V, ConsoleColor.White, Right, i);
            }
            int[,] walls = new int[,]
            {
                {11, 1},
                {2, 2}, {3, 2}, {4, 2}, {6, 2}, {7, 2}, {8, 2}, {9, 2}, {11, 2}, 
                {13, 2}, {14, 2}, {15, 2}, {16, 2}, {18, 2}, {19, 2}, {20, 2},
                {2, 3}, {4, 3}, {6, 3}, {9, 3}, {11, 3}, {13, 3}, {16, 3}, {18, 3}, {20, 3},
                {2, 4}, {3, 4}, {4, 4}, {6, 4}, {7, 4}, {8, 4}, {9, 4}, {11, 4},
                {13, 4}, {14, 4}, {15, 4}, {16, 4}, {18, 4}, {19, 4}, {20, 4},
                {2, 6}, {3, 6}, {4, 6}, {6, 6}, {8, 6}, {9, 6}, {10, 6}, {11, 6},
                {12, 6}, {13, 6}, {14, 6}, {16, 6}, {18, 6}, {19, 6}, {20, 6},
                {6, 7}, {11, 7}, {16, 7},
                {1, 8}, {2, 8}, {3, 8}, {4, 8}, {6, 8}, {7, 8}, {8, 8}, {9, 8}, {11, 8},

            };

            for (var i = 0; i < walls.GetLength(0); i++)
            {
                int x = walls[i, 0];
                int y = walls[i, 1];
                Board[x, y] = new StationaryObject(W, ConsoleColor.Blue, x, y);
            }
        }

        public object this[int x, int y]
        {
            get { return Board[x, y]; }
            set { Board[x, y] = value; }
        }

        public void Draw(bool walls = false)
        {
            for (var i = 0; i <= Right; i++)
            {
                for (var j = 0; j <= Bottom; j++)
                {
                    var o = ((IGameObject)Board[i, j]);
                    if (o != null)
                    {
                        if (walls && o is StationaryObject)
                            Drawer.DrawObject(o.Icon, o.Color, i, j);
                        if (o.GetType() != typeof(StationaryObject))
                            Drawer.DrawObject(o.Icon, o.Color, i, j);
                        //o.v = Vector.STOP;
                    }
                    else
                    {
                        Drawer.Erase(i, j);
                    }
                }
            }
        }
        
    }

    class StationaryObject : IGameObject
    {
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

        public StationaryObject(char icon, ConsoleColor color, int x, int y)
        {
            Icon = icon;
            Color = color;
            LastX = X = x;
            LastY = Y = y;
            v = Vector.RIGHT;
        }
    }
}
