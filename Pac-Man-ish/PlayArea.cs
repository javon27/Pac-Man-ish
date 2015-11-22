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
        }

        public object this[int x, int y]
        {
            get { return Board[x, y]; }
            set { Board[x, y] = value; }
        }

        public void Draw()
        {
            for (var i = 0; i <= Right; i++)
            {
                for (var j = 0; j <= Bottom; j++)
                {
                    var o = ((StationaryObject)Board[i, j]);
                    if (o != null)
                    {
                        Drawer.DrawObject(o.Icon, o.Color, o.X, o.Y);
                        o.v = Vector.STOP;
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
