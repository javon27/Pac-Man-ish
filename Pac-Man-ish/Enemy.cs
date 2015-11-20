using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_ish
{
    class Enemy: Player
    {
        public Enemy (char _icon, ConsoleColor color, int x, int y, PlayArea board) 
            : base (_icon, color, x, y, board)
        { }


    }
}
