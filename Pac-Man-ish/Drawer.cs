using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_ish
{
    class Drawer
    {
        public static void DrawObject(IGameObject obj)
        {
            int x = obj.X;
            int y = obj.Y;
            if (obj.v != Vector.STOP)
            {
                Erase(obj.LastX, obj.LastY);
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = obj.Color;
                Console.Write(obj.Icon);
                obj.LastX = x;
                obj.LastY = y;
                //Console.SetCursorPosition(0, 0);
                Console.ResetColor();
            }
        }

        public static void Erase(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
            //Console.SetCursorPosition(0, 0);
        }
        
    }
}
