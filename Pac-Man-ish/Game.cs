using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man_ish
{
    class Game
    {
        public bool RunGame = true;
        public const int TICK = 75;
        public static int gameCounter = 0;
        Player p1;
        List<Enemy> enemies;
        public PlayArea board;
        Thread t_KeyListener;
        int NumEnemies = 20;
        Properties.Settings options = Properties.Settings.Default;

        public Game()
        {
            init();
        }

        public void Run()
        {
            t_KeyListener = new Thread(KeyListener);
            t_KeyListener.Start();
            board.Draw();
            p1.Alive = true;
            p1.Start();
            foreach (var enemy in enemies)
            {
                enemy.Alive = true;
                enemy.Start();
            }
            RunGame = true;
            do
            {
                DrawPlayers();
                gameCounter++;
                gameCounter %= 100;
                Thread.Sleep(TICK);
            } while (RunGame);
            t_KeyListener.Abort();
            p1.Stop();
            foreach (var enemy in enemies)
            {
                enemy.Stop();
            }
        }

        private void DrawPlayers()
        {
                //board.ClearPlayArea();
                Drawer.DrawPlayer(p1);
                foreach (var enemy in enemies)
                {
                    Drawer.DrawPlayer(enemy);
                }
        }

        private void KeyListener()
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
                    case ConsoleKey.Spacebar:
                        p1.v = Vector.STOP;
                        break;
                }
            } while (RunGame);
        }

        private void init()
        {
            // Generate Board
            board = new PlayArea();
            p1 = new Player('█', ConsoleColor.Yellow, 4, 4, board);
            p1.v = Vector.RIGHT;
            ConsoleColor[] enemyColors =
            {
                ConsoleColor.Cyan,  // Blinky
                ConsoleColor.Magenta,  // Pinky
                ConsoleColor.Red,   // Inky
                ConsoleColor.Yellow  // Clyde

            };
            enemies = new List<Enemy>();
            Random rand = new Random(DateTime.Now.Millisecond);
            for (var i = 0; i < NumEnemies; i++)
            {
                var enemy = new Enemy('░', enemyColors[rand.Next() % 4], 30, 30, board);
                enemy.v = (Vector)(rand.Next() % 4);
                enemies.Add(enemy);
            }

        }
    }
}
