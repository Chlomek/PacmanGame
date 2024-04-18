using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        //    ║ + ╚═╝ . ╚═════╝ . ║ . ╚═════╝ . ╚═╝ + ║
        //    ║ . . . . . . . . . . . . . . . . . . . ║
        //    ║ . ═══ . ║ . ══════╦══════ . ║ . ═══ . ║
        //    ║ . . . . ║ . . . . ║ . . . . ║ . . . . ║
        //    ╚═════╗ . ╠══════   ║   ══════╣ . ╔═════╝
        //          ║ . ║                   ║ . ║
        //    ══════╝ . ║   ╔════---════╗   ║ . ╚══════
        //            .     ║ █ █   █ █ ║     .        
        //    ══════╗ . ║   ║           ║   ║ . ╔══════
        //          ║ . ║   ╚═══════════╝   ║ . ║
        //          ║ . ║       READY       ║ . ║
        //    ╔═════╝ . ║   ══════╦══════   ║ . ╚═════╗
        //    ║ . . . . . . . . . ║ . . . . . . . . . ║
        //    ║ . ══╗ . ═══════ . ║ . ═══════ . ╔══ . ║
        //    ║ + . ║ . . . . . . █ . . . . . . ║ . + ║
        //    ╠══ . ║ . ║ . ══════╦══════ . ║ . ║ . ══╣
        //    ║ . . . . ║ . . . . ║ . . . . ║ . . . . ║
        //    ║ . ══════╩══════ . ║ . ══════╩══════ . ║
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

            public bool AbleToMove((int X, int Y) position, char[,] Walls, int moveDirection)
            {
                
                if(moveDirection == (int)Direction.Up)
                {
                    if (Walls[position.X, --position.Y] == ' ')
                        return true;
                    else
                        return false;
                }
                else if (moveDirection == (int)Direction.Down)
                {
                    if (Walls[position.X, ++position.Y] == ' ')
                        return true;
                    else
                        return false;
                }
                else if (moveDirection == (int)Direction.Left)
                {
                    if (Walls[position.X - 1, position.Y] == ' ')
                        return true;
                    else
                        return false;
                }
                else if (moveDirection == (int)Direction.Right)
                {
                    if (Walls[position.X + 1, position.Y] == ' ')
                        return true;
                    else
                        return false;
                }
                else
                    return false;  
            }

            public void UpdatePacMan((int X, int Y) position, char[,] Dots, int moveDirection, out int scoreAdd, char[,] Walls)
            {
                if (moveDirection == (int)Direction.Up)
                {
                        position.Y--;
                        RenderPacMan(position);
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
                    else
                    {
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
                    else
                    {
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
                    else
                    {
                        position.X++;
                        RenderPacMan(position);
                    }
                }
                else
                {
                    RenderPacMan(position);
                }
                

                if (Dots[position.X, position.Y] == '.')
                {
                    scoreAdd = 1;
                }
                else
                    scoreAdd = 0;
            }
            public void Debug((int X, int Y) position)
            {
                Console.SetCursorPosition(0, 45);
                Console.Write(position);
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
"══════╗█  ║█ █║███████████║█ █║█ █╔══════\n" +
"██████║█  ║█ █╚═══════════╝█ █║█ █║██████\n" +
"██████║█  ║█                 █║█ █║██████\n" +
"╔═════╝█  ║█ █══════╦══════█ █║█ █╚═════╗\n" +
"║█                 █║█                 █║\n" +
"║█ █══╗█ █═══════█ █║█ █═══════█ █╔══█ █║\n" +
"║█   █║█                         █║█   █║\n" +
"╠══█ █║█ █║█ █══════╦══════█ █║█ █║█ █══╣\n" +
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
            Console.SetWindowSize(46, 46);
            Console.SetBufferSize(46, 46);
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

            char[,] GhostWalls = new char[42, 42];
            int x = 0, y = 0;
            foreach (char c in GhostWallsString)
            {
                if (c == '\n')
                {
                    GhostWalls[y, x] = c;
                    y++;
                    x = 0;
                }
                else
                {
                    GhostWalls[y, x] = c;
                    x++;
                }
            }
            //*
            for (int i = 0; i <41; i++)
            {
                for (int j = 0; j < 41; j++)
                {
                    Console.Write(GhostWalls[i, j]);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
            
            pacManPosition = (20, 13);
            Dots = walls.countDots(DotsString);
            walls.RenderWalls(WallsString);
            walls.RenderDots(DotsString);

            while (true)
            {

                //pacManPosition = (pacManPosition.X + 1, pacManPosition.Y);

                //pacMan.RenderPacMan(pacManPosition);

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                
                if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.W)
                {
                    if(pacMan.AbleToMove(pacManPosition, GhostWalls, (int)Direction.Up) == true)
                    {
                        PacManDirection = (int)Direction.Up;
                        pacManPosition = (pacManPosition.X, pacManPosition.Y-1);
                        pacMan.RenderPacMan(pacManPosition);
                    }                   
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.S)
                {
                    if (pacMan.AbleToMove(pacManPosition, GhostWalls, (int)Direction.Down) == true)
                    {
                        PacManDirection = (int)Direction.Down;
                        pacManPosition = (pacManPosition.X, pacManPosition.Y+1);
                        pacMan.RenderPacMan(pacManPosition);
                    }
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.A)
                {
                    if (pacMan.AbleToMove(pacManPosition, GhostWalls, (int)Direction.Left) == true)
                    {
                        PacManDirection = (int)Direction.Left;
                        pacManPosition = (pacManPosition.X-1, pacManPosition.Y);
                        pacMan.RenderPacMan(pacManPosition);
                    }
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow || keyInfo.Key == ConsoleKey.D)
                {
                    if(pacMan.AbleToMove(pacManPosition, GhostWalls, (int)Direction.Right) == true)
                    {
                        PacManDirection = (int)Direction.Right;
                        pacManPosition = (pacManPosition.X+1, pacManPosition.Y);
                        pacMan.RenderPacMan(pacManPosition);
                    }
                }
                
                //pacMan.UpdatePacMan(pacManPosition, Dots, PacManDirection, out int scoreAdd, GhostWalls);
                //score += scoreAdd;

                pacMan.Debug(pacManPosition);
                //Console.WriteLine(score);
            }

        }

        
    }

}
