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
        void Draw();
    }

    public enum Vector
    {
        UP, DOWN, LEFT, RIGHT, STOP
    }

    class Game
    {
        public const int TICK = 30;
        static Player p1;
        static List<Player> enemies;
        public static PlayArea board;
        static Thread t_KeyListener;
        static Thread t_DrawPlayers;
        static int NumEnemies = 10;
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
                p1.Draw();
                Thread.Sleep(TICK);
            } while (p1.Alive);
        }

        private static void KeyListener()
        {
            bool runGame = true;
            do
            {
                var cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    case ConsoleKey.Escape:
                        runGame = false;
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
            } while (runGame);
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
            p1 = new Player('█', 4, 4);
            p1.Color = ConsoleColor.Yellow;
            p1.board = board;
            p1.Start();
            //enemies = new List<Player>();
            //for (var i = 0; i < NumEnemies; i++)
            //{
            //    enemies.Add(new Player((char)164));
            //}
        }
    }
}
