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
        public int Width
        {
            get; set;
        }
        public int Height
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
            Width = Properties.Settings.Default.gameWidth-2;
            Height = Properties.Settings.Default.gameHeight-5;
            Board = new object[Width+1, Height+1];
            FillBoard();
            GetEmptyAreas();
        }

        private void GetEmptyAreas()
        {
            EmptyAreas = new List<Position>();
            for (var i = 1; i < Width; i++)
            {
                for (var j = 1; j < Height; j++)
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
            Board[Width, 0] = new StationaryObject(TR, ConsoleColor.White, Width, 0);
            Board[0, Height] = new StationaryObject(BL, ConsoleColor.White, 0, Height);
            Board[Width, Height] = new StationaryObject(BR, ConsoleColor.White, Width, Height);
            for (var i = 1; i < Width; i++)
            {
                Board[i, 0] = new StationaryObject(H, ConsoleColor.White, i, 0);
                Board[i, Height] = new StationaryObject(H, ConsoleColor.White, i, Height);
            }
            for (var i = 1; i < Height; i++)
            {
                Board[0, i] = new StationaryObject(V, ConsoleColor.White, 0, i);
                Board[Width, i] = new StationaryObject(V, ConsoleColor.White, Width, i);
            }
        }

        public object this[int x, int y]
        {
            get { return Board[x, y]; }
            set { Board[x, y] = value; }
        }

        public void Draw()
        {
            for (var i = 0; i <= Width; i++)
            {
                for (var j = 0; j <= Height; j++)
                {
                    var o = ((StationaryObject)Board[i, j]);
                    if (o != null)
                    {
                        Drawer.DrawPlayer(o);
                        o.v = Vector.STOP;
                    }
                    
                }
            }
        }

        public void ClearPlayArea()
        {
            foreach(var area in EmptyAreas)
            {
                Drawer.Erase(area.x, area.y);
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
