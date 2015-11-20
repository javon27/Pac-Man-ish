using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_ish
{
    class Drawer
    {
        public void DrawObject(IGameObject obj)
        {
            Console.SetCursorPosition(obj.X, obj.Y);
            Console.ForegroundColor = obj.Color;
            Console.Write(obj.Icon);
            Console.SetCursorPosition(0, 0);
            Console.ResetColor();
        }
        
    }
}
