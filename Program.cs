using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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

            public bool dotEaten(char[,] Dots, (int X, int Y) position)
            {
                if (Dots[position.X, position.Y] == '.')
                {
                    return true;
                }
                else
                    return false;
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

            int score = 0;
            Direction? pacManDirection = default;
            int pacManFrame = 0;
            const int framesToHorizontal = 6;
            const int framesToVertical = 6;
            (int X, int Y) pacManPosition;

            char[,] GhostWalls = new char[42, 42];
            int dotsCollected = 0;
            int x = 0, y = 0;
            foreach (char c in GhostWallsString)
            {
                if (c == '\n')
                {
                    GhostWalls[x, y] = c;
                    y++;
                    x = 0;
                }
                else
                {
                    GhostWalls[x, y] = c;
                    x++;
                }
            }

            char[,] dotsMap = new char[42, 42];

            pacManPosition = (20, 13);
            dotsMap = walls.countDots(DotsString);
            walls.RenderWalls(WallsString);
            walls.RenderDots(DotsString);
            FirstDirectionInput();

            while (true)
            {
                if (dotsCollected >= 168)
                {
                    dotsCollected = 0;
                    dotsMap = walls.countDots(DotsString);
                    walls.RenderWalls(WallsString);
                    walls.RenderDots(DotsString);
                    pacManPosition = (20, 13);
                    FirstDirectionInput();
                }

                Thread.Sleep(40);
                    HandleInput();
                    UpdatePacMan();
                    RenderPacMan();

                Debug();
                /*
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.LeftArrow && pacManPosition.X == 0 || keyInfo.Key == ConsoleKey.A && pacManPosition.X == 0)
            {
                PacManDirection = (int)Direction.Left;
                Console.SetCursorPosition(pacManPosition.X, pacManPosition.Y);
                Console.Write(' ');
                pacManPosition.X = 40;
                pacMan.RenderPacMan(pacManPosition);
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow && pacManPosition.X == 40 || keyInfo.Key == ConsoleKey.D && pacManPosition.X == 40)
            {
                PacManDirection = (int)Direction.Right;
                Console.SetCursorPosition(pacManPosition.X, pacManPosition.Y);
                Console.Write(' ');
                pacManPosition.X = 0;
                pacMan.RenderPacMan(pacManPosition);
            }
            else if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.W)
            {
                if (pacMan.AbleToMove(pacManPosition, GhostWalls, (int)Direction.Up) == true)
                {
                    PacManDirection = (int)Direction.Up;
                    Console.SetCursorPosition(pacManPosition.X, pacManPosition.Y);
                    Console.Write(' ');
                    pacManPosition = (pacManPosition.X, pacManPosition.Y - 1);
                    pacMan.RenderPacMan(pacManPosition);
                    if (walls.dotEaten(dotsMap, pacManPosition) == true)
                    {
                        dotsMap[pacManPosition.X, pacManPosition.Y] = ' ';
                        score++;
                    }
                }
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.S)
            {
                if (pacMan.AbleToMove(pacManPosition, GhostWalls, (int)Direction.Down) == true)
                {
                    PacManDirection = (int)Direction.Down;
                    Console.SetCursorPosition(pacManPosition.X, pacManPosition.Y);
                    Console.Write(' ');
                    pacManPosition = (pacManPosition.X, pacManPosition.Y + 1);
                    pacMan.RenderPacMan(pacManPosition);
                    if (walls.dotEaten(dotsMap, pacManPosition) == true)
                    {
                        dotsMap[pacManPosition.X, pacManPosition.Y] = ' ';
                        score++;
                    }
                }
            }
            else if (keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.A)
            {
                if (pacMan.AbleToMove(pacManPosition, GhostWalls, (int)Direction.Left) == true)
                {
                    PacManDirection = (int)Direction.Left;
                    Console.SetCursorPosition(pacManPosition.X, pacManPosition.Y);
                    Console.Write(' ');
                    pacManPosition = (pacManPosition.X - 1, pacManPosition.Y);
                    pacMan.RenderPacMan(pacManPosition);
                    if (walls.dotEaten(dotsMap, pacManPosition) == true)
                    {
                        dotsMap[pacManPosition.X, pacManPosition.Y] = ' ';
                        score++;
                    }
                }
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow || keyInfo.Key == ConsoleKey.D)
            {
                if (pacMan.AbleToMove(pacManPosition, GhostWalls, (int)Direction.Right) == true)
                {
                    PacManDirection = (int)Direction.Right;
                    Console.SetCursorPosition(pacManPosition.X, pacManPosition.Y);
                    Console.Write(' ');
                    pacManPosition = (pacManPosition.X + 1, pacManPosition.Y);
                    pacMan.RenderPacMan(pacManPosition);
                    if (walls.dotEaten(dotsMap, pacManPosition) == true)
                    {
                        dotsMap[pacManPosition.X, pacManPosition.Y] = ' ';
                        score++;
                    }
                }
            }
        }
        else
        {
            pacManFrame--;
        }*/
                //pacMan.UpdatePacMan(pacManPosition, Dots, PacManDirection, out int scoreAdd, GhostWalls);
                //score += scoreAdd;


                //Console.WriteLine(score);
            }
            bool FirstDirectionInput()
            {
            GetInput:
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow: pacManDirection = Direction.Left; break;
                    case ConsoleKey.RightArrow: pacManDirection = Direction.Right; break;
                    default: goto GetInput;
                }
                return false;
            }

            bool HandleInput()
            {
                bool moved = false;
                void TrySetPacManDirection(Direction direction)
                {
                    if (moved == false && pacManDirection != direction)
                    {
                        pacManDirection = direction;
                        pacManFrame = 0;
                        moved = true;
                    }
                }
                while (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow: TrySetPacManDirection(Direction.Up); break;
                        case ConsoleKey.DownArrow: TrySetPacManDirection(Direction.Down); break;
                        case ConsoleKey.LeftArrow: TrySetPacManDirection(Direction.Left); break;
                        case ConsoleKey.RightArrow: TrySetPacManDirection(Direction.Right); break;
                    }
                    return false;
                }
                return false;
            }

            void RenderPacMan()
            {
                Console.SetCursorPosition(pacManPosition.X, pacManPosition.Y);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write('C');
                Console.ForegroundColor = ConsoleColor.White;
            }

            bool AbleToMove(int xc, int yc, Direction direction, char[,] Walls)
            {
                switch (direction)
                {
                    case Direction.Up:
                        if (Walls[xc, yc - 1] == ' ')
                            return true;
                        else
                            return false;
                        break;
                    case Direction.Down:
                        if (Walls[xc, yc + 1] == ' ')
                            return true;
                        else
                            return false;
                        break;
                    case Direction.Left:
                        if (Walls[xc - 1, yc] == ' ')
                            return true;
                        else
                            return false;
                        break;
                    case Direction.Right:
                        if (Walls[xc + 1, yc] == ' ')
                            return true;
                        else
                            return false;
                        break;
                    default:
                        return false;
                }
            }

            void UpdatePacMan()
            {
                if (pacManDirection.HasValue)
                {
                    if ((pacManDirection == Direction.Left || pacManDirection == Direction.Right) && pacManFrame >= framesToHorizontal ||
                             (pacManDirection == Direction.Up || pacManDirection == Direction.Down) && pacManFrame >= framesToVertical)
                    {
                        if (AbleToMove(pacManPosition.X, pacManPosition.Y, pacManDirection.Value, GhostWalls) == true)
                        {
                            pacManFrame = 0;
                            Console.SetCursorPosition(pacManPosition.X, pacManPosition.Y);
                            Console.Write(' ');
                            switch (pacManDirection)
                            {
                                case Direction.Up:
                                    pacManPosition.Y--;
                                    break;
                                case Direction.Down:
                                    pacManPosition.Y++;
                                    break;
                                case Direction.Left:
                                    pacManPosition.X--;
                                    break;
                                case Direction.Right:
                                    pacManPosition.X++;
                                    break;
                            }
                            if (pacManPosition.X <= 0)
                            {
                                pacManPosition.X = 40;
                            }
                            else if (pacManPosition.X >= 40)
                            {
                                pacManPosition.X = 0;
                            }
                            if (walls.dotEaten(dotsMap, pacManPosition) == true)
                            {
                                dotsMap[pacManPosition.X, pacManPosition.Y] = ' ';
                                dotsCollected += 1;
                                score += 1;
                            }
                        }
                        else
                        {
                            pacManDirection = null;
                        }
                    }
                    else
                    {
                        pacManFrame++;
                    }
                }
            }

            void Debug()
            {
                Console.SetCursorPosition(0, 24);
                Console.Write("                                       ");
                Console.SetCursorPosition(0, 24);
                Console.Write(pacManPosition);
                Console.SetCursorPosition(9, 24);
                Console.Write(" score:" + score);
                Console.SetCursorPosition(20, 24);
                Console.Write(pacManDirection);
            }

        }
    }
}
