using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_ish
{
    public class Position
    {
        public int x
        {
            get; set;
        }
        public int y
        {
            get; set;
        }
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public interface IGameObject
    {
        ConsoleColor Color
        {
            get; set;
        }
        char Icon
        {
            get; set;
        }
        int X
        {
            get; set;
        }
        int Y
        {
            get; set;
        }
        int LastX
        {
            get; set;
        }
        int LastY
        {
            get; set;
        }
        Vector v
        {
            get; set;
        }
    }

    public enum Vector
    {
        UP, DOWN, LEFT, RIGHT, STOP
    }

    class Game
    {
        public static bool RunGame = true;
        public const int TICK = 50;
        static Player p1;
        static List<Player> enemies;
        public static PlayArea board;
        static Thread t_KeyListener;
        static Thread t_DrawPlayers;
        static int NumEnemies = 2;
        static Properties.Settings options = Properties.Settings.Default;

        static void Main(string[] args)
        {
            init();
            t_KeyListener = new Thread(KeyListener);
            t_KeyListener.Start();
            t_DrawPlayers = new Thread(DrawPlayers);
            t_DrawPlayers.Start();
        }

        private static void DrawPlayers()
        {
            do
            {
                //board.ClearPlayArea();
                Drawer.DrawObject(p1);
                foreach (var enemy in enemies)
                {
                     Drawer.DrawObject(enemy);
                }
                Thread.Sleep(TICK);
            } while (RunGame);
        }

        private static void KeyListener()
        {
            do
            {
                var cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    case ConsoleKey.Escape:
                        RunGame = false;
                        break;
                    case ConsoleKey.UpArrow:
                        p1.v = Vector.UP;
                        break;
                    case ConsoleKey.DownArrow:
                        p1.v = Vector.DOWN;
                        break;
                    case ConsoleKey.LeftArrow:
                        p1.v = Vector.LEFT;
                        break;
                    case ConsoleKey.RightArrow:
                        p1.v = Vector.RIGHT;
                        break;
                }
            } while (RunGame);
            p1.Alive = false;
        }

        private static void init()
        {
            Console.WindowHeight = options.gameHeight;
            Console.BufferHeight = options.gameHeight;
            Console.WindowWidth = options.gameWidth;
            Console.BufferWidth = options.gameWidth;
            Console.CursorVisible = false;
            Console.Title = "Pac-Man-Ish";
            // Generate Board
            board = new PlayArea();
            board.Draw();
            p1 = new Player('█', ConsoleColor.Yellow, 4, 4, board);
            p1.v = Vector.RIGHT;
            p1.Start();
            ConsoleColor[] enemyColors =
            {
                ConsoleColor.Cyan,
                ConsoleColor.Magenta,
                ConsoleColor.Red,
                ConsoleColor.Yellow

            };
            enemies = new List<Player>();
            var enemy = new Player((char)164, enemyColors[0], 10, 10, board);
            enemy.v = Vector.UP;
            enemies.Add(enemy);
            enemy = new Player((char)164, enemyColors[1], 25, 25, board);
            enemy.v = Vector.LEFT;
            enemies.Add(enemy);
            foreach (var e in enemies)
            {
                e.Start();
            }

        }
    }
}
