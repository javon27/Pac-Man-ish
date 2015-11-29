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
    }

    public interface IGameActor: IGameObject
    {
        bool Alive
        {
            get; set;
        }
        Vector V
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
        float fX
        {
            get; set;
        }
        float fY
        {
            get; set;
        }
        void Start();
        void Stop();
    }

    public enum Vector
    {
        UP, DOWN, LEFT, RIGHT, STOP
    }

    public enum MenuItem
    {
        NEW, CONTINUE, SETTINGS, QUIT
    }

    static class Program
    {
        public static Properties.Settings options = Properties.Settings.Default;
        private static Game game;

        static void Main(string[] args)
        {
            //init();
            //t_KeyListener = new Thread(KeyListener);
            //t_KeyListener.Start();
            //t_DrawPlayers = new Thread(DrawPlayers);
            //t_DrawPlayers.Start();
            initWindow();
            
            bool playGame = true;
            do
            {
                switch (Menu())
                {
                    case MenuItem.NEW:
                        Console.Clear();
                        game = new Game();
                        game.Run();
                        break;
                    case MenuItem.CONTINUE:
                        if (game != null)
                        {
                            Console.Clear();
                            game.Run();
                        }
                        break;
                    case MenuItem.QUIT:
                        playGame = false;
                        break;
                    case MenuItem.SETTINGS:
                        Drawer.WriteStatus("Settings selected");
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
            int cursor = 0;
            string[] items =
            {
                "New Game",
                "Continue",
                "Settings",
                "Quit"
            };
            ConsoleKeyInfo cki;
            bool itemSelected = false;
            do
            {
                for (var i = 0; i < 4; i++)
                {
                    Console.SetCursorPosition(7, 10 + i);
                    if (i == cursor)
                    {
                        Console.Write("█ ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                    Console.Write(items[i]);
                }
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.UpArrow)
                {
                    cursor = (4 + cursor - 1) % 4;
                }
                else if (cki.Key == ConsoleKey.DownArrow){
                    cursor = (4 + cursor + 1) % 4;
                }
                else if (cki.Key == ConsoleKey.Enter)
                {
                    itemSelected = true;
                }
            } while (!itemSelected);

            return (MenuItem) cursor;
        }
    }
}
