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

    public enum MenuItem
    {
        PLAY, SETTINGS, QUIT
    }

    class Program
    {
        static Properties.Settings options = Properties.Settings.Default;
        static void Main(string[] args)
        {
            //init();
            //t_KeyListener = new Thread(KeyListener);
            //t_KeyListener.Start();
            //t_DrawPlayers = new Thread(DrawPlayers);
            //t_DrawPlayers.Start();
            initWindow();

            Game game = new Game();
            bool playGame = true;
            do
            {
                switch (Menu())
                {
                    case MenuItem.PLAY:
                        Console.Clear();
                        game.Run();
                        break;
                    case MenuItem.QUIT:
                        playGame = false;
                        break;
                    case MenuItem.SETTINGS:
                        break;
                }
            } while (playGame);
        }

        static void initWindow()
        {
            Console.WindowHeight = options.windowHeight;
            Console.BufferHeight = options.windowHeight;
            Console.WindowWidth = options.windowWidth;
            Console.BufferWidth = options.windowWidth;
            Console.CursorVisible = false;
            Console.Title = "Pac-Man-Ish";
            Console.Clear();
        }

        static MenuItem Menu()
        {
            string msg = "Press enter to play game.";
            Console.SetCursorPosition(30 - (msg.Length / 2), 30);
            Console.Write(msg);
            Console.ReadLine();
            return MenuItem.PLAY;
        }

        static void test()
        {
            PlayArea board = new PlayArea();
            Player p1 = new Player('P', ConsoleColor.White, 1, 1, board);

        }
    }
}
