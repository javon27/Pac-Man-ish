using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_ish
{
    class PlayArea
    {
        private static string TL = "╔";
        private static string TR = "╗";
        private static string BL = "╚";
        private static string BR = "╝";
        private static string H = "═";
        private static string V = "║";
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
        
        public PlayArea()
        {
            Width = Properties.Settings.Default.gameWidth-1;
            Height = Properties.Settings.Default.gameHeight-4;
            Board = new object[Width, Height];
            FillBoard();
        }

        private void FillBoard()
        {
            throw new NotImplementedException();
        }

        public object[,] this[int x, int y]
        {
            get { return Board; }
            set { Board[x, y] = value; }
        }

        public void Draw()
        {
            
        }
        
    }
}
