using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame
{
    internal class Program
    {
        //    ╔═══════════════════╦═══════════════════╗
        //    ║ . . . . . . . . . ║ . . . . . . . . . ║
        //    ║ . ╔═╗ . ╔═════╗ . ║ . ╔═════╗ . ╔═╗ . ║
        //    ║ + ╚═╝ . ╚═════╝ . ╨ . ╚═════╝ . ╚═╝ + ║
        //    ║ . . . . . . . . . . . . . . . . . . . ║
        //    ║ . ═══ . ╥ . ══════╦══════ . ╥ . ═══ . ║
        //    ║ . . . . ║ . . . . ║ . . . . ║ . . . . ║
        //    ╚═════╗ . ╠══════   ╨   ══════╣ . ╔═════╝
        //          ║ . ║                   ║ . ║
        //    ══════╝ . ╨   ╔════---════╗   ╨ . ╚══════
        //            .     ║ █ █   █ █ ║     .        
        //    ══════╗ . ╥   ║           ║   ╥ . ╔══════
        //          ║ . ║   ╚═══════════╝   ║ . ║
        //          ║ . ║       READY       ║ . ║
        //    ╔═════╝ . ╨   ══════╦══════   ╨ . ╚═════╗
        //    ║ . . . . . . . . . ║ . . . . . . . . . ║
        //    ║ . ══╗ . ═══════ . ╨ . ═══════ . ╔══ . ║
        //    ║ + . ║ . . . . . . █ . . . . . . ║ . + ║
        //    ╠══ . ╨ . ╥ . ══════╦══════ . ╥ . ╨ . ══╣
        //    ║ . . . . ║ . . . . ║ . . . . ║ . . . . ║
        //    ║ . ══════╩══════ . ╨ . ══════╩══════ . ║
        //    ║ . . . . . . . . . . . . . . . . . . . ║
        //    ╚═══════════════════════════════════════╝

        public class PacMan
        {
            public void RenderPacMan((int X, int Y) position)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write('C');
                Console.ForegroundColor = ConsoleColor.White;
            }
            public void UpdatePacMan((int X, int Y) position, char[,] Dots, int moveDirection, out int scoreAdd, string GhostWalls)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write("C");
                /*
                char[,] Walls = new char[42, 42];
                int x = 0, y = 1;
                foreach (char c in GhostWalls)
                {
                    if (c == '\n')
                    {
                        y++;
                        x = 0;
                    }
                    else
                    {
                        Walls[x, y] = c;
                        x++;
                    }
                }



                if (moveDirection == (int)Direction.Up)
                {
                    if (Walls[position.X, position.Y - 1] == ' ')
                    {
                        //Console.Write(' ');
                        //Console.SetCursorPosition(position.X, position.Y - 1);
                        position.Y--;
                        RenderPacMan(position);
                    }
                }
                else if (moveDirection == (int)Direction.Down)
                {
                    if (Walls[position.X, position.Y + 1] == ' ')
                    {
                        //Console.Write(' ');
                        //Console.SetCursorPosition(position.X, position.Y + 1);
                        position.Y++;
                        RenderPacMan(position);
                    }
                }
                else if (moveDirection == (int)Direction.Left)
                {
                    if (Walls[position.X - 1, position.Y] == ' ')
                    {
                        //Console.Write(' ');
                        //Console.SetCursorPosition(position.X - 1, position.Y);
                        position.X--;
                        RenderPacMan(position);
                    }
                }
                else if (moveDirection == (int)Direction.Right)
                {
                    if (Walls[position.X + 1, position.Y] == ' ')
                    {
                        //Console.Write(' ');
                        //Console.SetCursorPosition(position.X + 1, position.Y);
                        position.X++;
                        RenderPacMan(position);
                    }
                }
                else
                {
                    RenderPacMan(position);
                }
                */

                if (Dots[position.X, position.Y] == '.')
                {
                    scoreAdd = 1;
                }
                else
                    scoreAdd = 0;
            }
        }

        public class Walls
        {
            public void RenderWalls(string map)
            {
                Console.SetCursorPosition(0, 0);
                int x = Console.CursorLeft;
                int y = Console.CursorTop;
                foreach (char c in map)
                {
                    if (c is '\n')
                    {
                        Console.WriteLine();

                        Console.SetCursorPosition(x, ++y);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(c);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
            }

            public void RenderDots(string map)
            {
                Console.SetCursorPosition(0, 0);
                int x = Console.CursorLeft;
                int y = Console.CursorTop;
                foreach (char c in map)
                {
                    if (c == '\n')
                    {
                        Console.SetCursorPosition(x, ++y);
                    }
                    else if (c == ' ')
                    {
                        Console.CursorLeft++;
                    }
                    else if (c == '.')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(c);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (c == '+')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(c);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }

            public char[,] countDots(string DotsString)
            {
                int x = 0;
                int y = 0;
                char[,] countDots = new char[42, 42];
                foreach (char c in DotsString)
                {
                    if (c == '\n')
                    {
                        y++;
                        x = 0;
                    }
                    else
                    {
                        countDots[x, y] = c;
                        x++;
                    }
                }
                return countDots;
            }
        }


        enum Direction
        {
            Up = 0,
            Down = 1,
            Left = 2,
            Right = 3,
        }

        static void Main(string[] args)
        {
            string GhostWallsString =
"╔═══════════════════╦═══════════════════╗\n" +
"║█                 █║█                 █║\n" +
"║█ █╔═╗█ █╔═════╗█ █║█ █╔═════╗█ █╔═╗█ █║\n" +
"║█ █╚═╝█ █╚═════╝█ █║█ █╚═════╝█ █╚═╝█ █║\n" +
"║█                                     █║\n" +
"║█ █═══█ █║█ █══════╦══════█ █║█ █═══█ █║\n" +
"║█       █║█       █║█       █║█       █║\n" +
"╚═════╗█ █╠══════█ █║█ █══════╣█ █╔═════╝\n" +
"██████║█ █║█                 █║█ █║██████\n" +
"══════╝█ █║█ █╔════█ █════╗█ █║█ █╚══════\n" +
"             █║█         █║█             \n" +
"══════╗█ █║█ █║███████████║█ █║█ █╔══════\n" +
"██████║█ █║█ █╚═══════════╝█ █║█ █║██████\n" +
"██████║█ █║█                 █║█ █║██████\n" +
"╔═════╝█ █║█ █══════╦══════█ █║█ █╚═════╗\n" +
"║█                 █║█                 █║\n" +
"║█ █══╗█ █═══════█ █║█ █═══════█ █╔══█ █║\n" +
"║█   █║█                         █║█   █║\n" +
"╠══█ █║█ █╥█ █══════╦══════█ █║█ █║█ █══╣\n" +
"║█       █║█       █║█       █║█       █║\n" +
"║█ █══════╩══════█ █║█ █══════╩══════█ █║\n" +
"║█                                     █║\n" +
"╚═══════════════════════════════════════╝";

            string WallsString =
"╔═══════════════════╦═══════════════════╗\n" +
"║                   ║                   ║\n" +
"║   ╔═╗   ╔═════╗   ║   ╔═════╗   ╔═╗   ║\n" +
"║   ╚═╝   ╚═════╝   ║   ╚═════╝   ╚═╝   ║\n" +
"║                                       ║\n" +
"║   ═══   ║   ══════╦══════   ║   ═══   ║\n" +
"║         ║         ║         ║         ║\n" +
"╚═════╗   ╠══════   ║   ══════╣   ╔═════╝\n" +
"      ║   ║                   ║   ║      \n" +
"══════╝   ║   ╔════   ════╗   ║   ╚══════\n" +
"              ║           ║              \n" +
"══════╗   ║   ║           ║   ║   ╔══════\n" +
"      ║   ║   ╚═══════════╝   ║   ║      \n" +
"      ║   ║                   ║   ║      \n" +
"╔═════╝   ║   ══════╦══════   ║   ╚═════╗\n" +
"║                   ║                   ║\n" +
"║   ══╗   ═══════   ║   ═══════   ╔══   ║\n" +
"║     ║                           ║     ║\n" +
"╠══   ║   ║   ══════╦══════   ║   ║   ══╣\n" +
"║         ║         ║         ║         ║\n" +
"║   ══════╩══════   ║   ══════╩══════   ║\n" +
"║                                       ║\n" +
"╚═══════════════════════════════════════╝";

            string DotsString =
"                                         \n" +
"  . . . . . . . . .   . . . . . . . . .  \n" +
"  .     .         .   .         .     .  \n" +
"  +     .         .   .         .     +  \n" +
"  . . . . . . . . . . . . . . . . . . .  \n" +
"  .     .   .               .   .     .  \n" +
"  . . . .   . . . .   . . . .   . . . .  \n" +
"        .                       .        \n" +
"        .                       .        \n" +
"        .                       .        \n" +
"        .                       .        \n" +
"        .                       .        \n" +
"        .                       .        \n" +
"        .                       .        \n" +
"        .                       .        \n" +
"  . . . . . . . . .   . . . . . . . . .  \n" +
"  .     .         .   .         .     .  \n" +
"  + .   . . . . . .   . . . . . .   . +  \n" +
"    .   .   .               .   .   .    \n" +
"  . . . .   . . . .   . . . .   . . . .  \n" +
"  .               .   .               .  \n" +
"  . . . . . . . . . . . . . . . . . . .  \n" +
"                                         ";

            Console.CursorVisible = false;
            Console.SetWindowSize(45, 45);
            Console.SetBufferSize(45, 45);
            Console.Title = "PacMan";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            PacMan pacMan = new PacMan();
            Walls walls = new Walls();

            char[,] Dots;
            int score = 0;
            int PacManDirection = 1;
            (int X, int Y) pacManPosition;

            pacManPosition = (20, 13);


            Dots = walls.countDots(DotsString);
            walls.RenderWalls(WallsString);
            walls.RenderDots(DotsString);

            while (true)
            {

                //pacManPosition = (pacManPosition.X + 1, pacManPosition.Y);

                //pacMan.RenderPacMan(pacManPosition);
                
                if (Console.ReadKey().Key == ConsoleKey.UpArrow)
                {
                    PacManDirection = (int)Direction.Up;
                    pacManPosition = (pacManPosition.X, pacManPosition.Y - 1);
                }
                else if (Console.ReadKey().Key == ConsoleKey.DownArrow)
                {
                    PacManDirection = (int)Direction.Down;
                    pacManPosition = (pacManPosition.X, pacManPosition.Y + 1);
                }
                else if (Console.ReadKey().Key == ConsoleKey.LeftArrow)
                {
                    PacManDirection = (int)Direction.Left;
                    pacManPosition = (pacManPosition.X - 1, pacManPosition.Y);
                }
                else if (Console.ReadKey().Key == ConsoleKey.RightArrow)
                {
                    PacManDirection = (int)Direction.Right;
                    pacManPosition = (pacManPosition.X + 1, pacManPosition.Y);
                }

                pacMan.UpdatePacMan(pacManPosition, Dots, PacManDirection, out int scoreAdd, GhostWallsString);
                score += scoreAdd;
                //Console.WriteLine(score);
            }

        }

    }

}
