using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_ish
{
    class PlayArea
    {
        private static char[] WALLCHARS =
        {
            '╔', '╗', '╚', '╝', '═', '║', '╠', '╣', '╦', '╩', 'i'
        };
        public static char PELLET = '·';
        public static char POWERUP = '°';
        Assembly assembly = Assembly.GetExecutingAssembly();

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
        public object[,] PelletBoard
        {
            get; set;
        }
        public List<Position> EmptyAreas;
        
        public PlayArea()
        {
            Right = Properties.Settings.Default.windowWidth-2;
            Bottom = Properties.Settings.Default.windowHeight-4;
            Board = new object[Right + 1, Bottom + 1];
            PelletBoard = new object[Right + 1, Bottom + 1];
            FillBoard();
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
            List<string> lines = new List<string>();
            using (Stream stream = assembly.GetManifestResourceStream("Pac_Man_ish.1.brd"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    int row = 0;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        int col = 0;
                        foreach (char c in line)
                        {
                            if (WALLCHARS.Contains(c))
                            {
                                char i;
                                if (c == 'i')
                                    i = ' ';
                                else
                                    i = c;
                                Board[col, row] = new StationaryObject(i, ConsoleColor.Blue, col, row);
                            }
                            if (c == PELLET || c == POWERUP)
                            {
                                PelletBoard[col, row] = new Pellet(c, ConsoleColor.White, col, row);
                            }
                            if (c == 'n')
                            {
                                Board[col, row] = null;
                            }
                            col++;
                        }
                        row++;
                    }
                }
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
                    var p = ((IGameObject)PelletBoard[i, j]);
                    if (o != null || p != null)
                    {
                        if (walls && o is StationaryObject)
                            Drawer.DrawObject(o.Icon, o.Color, i, j);
                        if (p != null)
                            Drawer.DrawObject(p.Icon, p.Color, i, j);
                        if (o is IGameActor)
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

    class Pellet : IGameObject
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
        public bool PowerUp
        {
            get; set;
        }
        public Pellet(char icon, ConsoleColor color, int x, int y)
        {
            Icon = icon;
            Color = color;
            X = x;
            Y = y;
            if (icon == PlayArea.POWERUP)
                PowerUp = true;
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
