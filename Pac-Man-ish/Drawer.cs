﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_ish
{
    class Drawer
    {
        static int statusRow = 26;
        public static void ClearStatus()
        {
            Console.SetCursorPosition(0, statusRow);
            for (var i = 0; i < 25; i++)
            {
                Console.Write(' ');
            }
        }

        public static void DrawPlayer(IGameActor player)
        {
            int x = player.X;
            int y = player.Y;
           // if (player.v != Vector.STOP)
            //{
                Erase(player.LastX, player.LastY);
                DrawObject(player.Icon, player.Color, x, y);
                player.LastX = x;
                player.LastY = y;
            //}
        }
        

        public static void DrawObject(char s, ConsoleColor c, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = c;
            Console.Write(s);
            Console.ResetColor();
        }

        public static void Erase(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }

        public static void WriteStatus(string s)
        {
            ClearStatus();
            Console.SetCursorPosition(0, statusRow);
            Console.Write(s);
        }
    }
}
