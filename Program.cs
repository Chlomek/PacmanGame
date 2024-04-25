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

        }

        class Ghost
        {
            public (int X, int Y) StartingPosition;
            public (int X, int Y) Position;
            public bool Weak;
            public int WeakTime;
            public ConsoleColor Color;
            public Direction direction;
            public int Start;
            public int UpdateFrame;
            public int FramesToUpdate;
            public (int X, int Y)? Destination;
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
"══════╝█ █║█ █╔════███════╗█ █║█ █╚══════\n" +
"█            █║█         █║█            █\n" +
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
"  + .   . . . . . . . . . . . . .   . +  \n" +
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

            Random rnd = new Random();
            int score = 0;
            Direction? pacManDirection = default;
            Direction? pacManMovingDirection = default;
            int pacManFrame = 0;
            const int framesToHorizontal = 6;
            const int framesToVertical = 6;
            (int X, int Y) pacManPosition;

            char[,] GhostWalls = new char[42, 42];
            int dotsCollected = 0;
            int x = 0, y = 0;
            foreach (char ch in GhostWallsString)
            {
                if (ch == '\n')
                {
                    GhostWalls[x, y] = ch;
                    y++;
                    x = 0;
                }
                else
                {
                    GhostWalls[x, y] = ch;
                    x++;
                }
            }

            char[,] dotsMap = new char[42, 42];
            char[,] countDots = new char[42, 42];

            Ghost a = new Ghost();
            Ghost b = new Ghost();
            Ghost c = new Ghost();
            Ghost d = new Ghost();

            a.Color = ConsoleColor.Red;
            a.StartingPosition = (20, 10);
            a.Position = a.StartingPosition;
            a.Start = 3;
            a.UpdateFrame = -30;
            a.FramesToUpdate = 8;

            pacManPosition = (20, 13);
            countDotsCall();
            RenderWalls();
            RenderDots();
            FirstDirectionInput();

            while (true)
            {
                if (dotsCollected > 168)
                {
                    dotsCollected = 0;
                    countDotsCall();
                    RenderWalls();
                    RenderDots();
                    pacManPosition = (20, 13);
                    FirstDirectionInput();
                }

                Thread.Sleep(40);
                //RenderDots();
                HandleInput();
                UpdatePacMan();
                RenderPacMan();
                UpdateGhost();
                RenderGhost();


                Debug();
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

            bool AbleToMovePacMan()
            {
                switch (pacManDirection)
                {
                    case Direction.Up:
                        if (GhostWalls[pacManPosition.X, pacManPosition.Y - 1] == ' ')
                        {
                            return true;

                        }
                        else if (GhostWalls[pacManPosition.X, pacManPosition.Y - 1] == '█')
                        {
                            pacManDirection = pacManMovingDirection;
                            return false;

                        }
                        else
                        {
                            return false;

                        }
                        break;
                    case Direction.Down:
                        if (GhostWalls[pacManPosition.X, pacManPosition.Y + 1] == ' ')
                        {
                            return true;

                        }
                        else if (GhostWalls[pacManPosition.X, pacManPosition.Y + 1] == '█')
                        {
                            pacManDirection = pacManMovingDirection;
                            return false;

                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case Direction.Left:
                        if (GhostWalls[pacManPosition.X - 1, pacManPosition.Y] == ' ')
                            return true;
                        else
                            return false;
                        break;
                    case Direction.Right:
                        if (GhostWalls[pacManPosition.X + 1, pacManPosition.Y] == ' ')
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
                        if (AbleToMovePacMan())
                        {
                            pacManMovingDirection = pacManDirection;
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
                            if (pacManPosition.X <= 1)
                            {
                                pacManPosition.X = 39;
                            }
                            else if (pacManPosition.X >= 39)
                            {
                                pacManPosition.X = 1;
                            }
                            if (dotEaten() == true)
                            {
                                countDots[pacManPosition.X, pacManPosition.Y] = ' ';
                                dotsCollected += 1;
                                score += 1;
                            }
                        }
                        else if (pacManMovingDirection.HasValue)
                        {
                            pacManDirection = pacManMovingDirection;
                            if (AbleToMovePacMan())
                            {
                                pacManFrame = 0;
                                Console.SetCursorPosition(pacManPosition.X, pacManPosition.Y);
                                Console.Write(' ');
                                if (pacManDirection == Direction.Left)
                                {
                                    pacManPosition.X--;
                                }
                                else if (pacManDirection == Direction.Right)
                                {
                                    pacManPosition.X++;
                                }
                                if (pacManPosition.X <= 0)
                                {
                                    pacManPosition.X = 40;
                                }
                                else if (pacManPosition.X >= 40)
                                {
                                    pacManPosition.X = 0;
                                }
                                if (dotEaten() == true)
                                {
                                    countDots[pacManPosition.X, pacManPosition.Y] = ' ';
                                    dotsCollected += 1;
                                    score += 1;
                                }
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

            void UpdateGhost()
            {
                Console.SetCursorPosition(a.Position.X, a.Position.Y);
                if (countDots[a.Position.X, a.Position.Y] == '.')
                {
                    Console.SetCursorPosition(a.Position.X, a.Position.Y);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(countDots[a.Position.X, a.Position.Y]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (countDots[a.Position.X, a.Position.Y] == '+')
                {
                    Console.SetCursorPosition(a.Position.X, a.Position.Y);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(countDots[a.Position.X, a.Position.Y]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write(" ");
                }

                if (a.Start > 1)
                {
                    if (a.UpdateFrame >= a.FramesToUpdate)
                    {
                        switch (a.Start)
                        {
                            case 3:
                                a.Position = (20, 9);
                                a.Start--;
                                break;
                            case 2:
                                a.Position = (20, 8);
                                a.Start--;
                                break;
                        }
                        a.UpdateFrame = 0;
                    }
                    else
                    {
                        a.UpdateFrame++;
                    }
                }
                else
                {
                    if (a.UpdateFrame >= a.FramesToUpdate)
                    {
                        a.UpdateFrame = 0;

                        bool up = AbleToMove(a.Position.X, a.Position.Y, Direction.Up, GhostWalls);
                        bool down = AbleToMove(a.Position.X, a.Position.Y, Direction.Down, GhostWalls);
                        bool left = AbleToMove(a.Position.X, a.Position.Y, Direction.Left, GhostWalls);
                        bool right = AbleToMove(a.Position.X, a.Position.Y, Direction.Right, GhostWalls);

                        if (a.Destination.HasValue)
                        {
                            a.Destination = (pacManPosition.X, pacManPosition.Y);
                            if (a.Position.X == a.Destination.Value.X && a.Position.Y == a.Destination.Value.Y)
                            {
                                a.Destination = (pacManPosition.X, pacManPosition.Y);
                            }
                            else if (a.direction == Direction.Left && left == true && (up == false || down == false))
                            {
                                a.Position.X--;
                            }
                            else if (a.direction == Direction.Up && up == true && (right == false || left == false))
                            {
                                a.Position.Y--;
                            }
                            else if (a.direction == Direction.Right && right == true && (up == false || down == false))
                            {
                                a.Position.X++;
                            }
                           
                            else if (a.direction == Direction.Down && down == true && (right == false || left == false))
                            {
                                a.Position.Y++;
                            }
                            else if (a.Position.X < a.Destination.Value.X && right == true && a.direction != Direction.Left)
                            {
                                a.Position.X++;
                                a.direction = Direction.Right;
                            }
                            else if (a.Position.X > a.Destination.Value.X && left == true && a.direction != Direction.Right)
                            {
                                a.Position.X--;
                                a.direction = Direction.Left;
                            }
                            else if (a.Position.Y < a.Destination.Value.Y && down == true && a.direction != Direction.Up)
                            {
                                a.Position.Y++;
                                a.direction = Direction.Down;
                            }
                            else if (a.Position.Y > a.Destination.Value.Y && up == true && a.direction != Direction.Down)
                            {
                                a.Position.Y--;
                                a.direction = Direction.Up;
                            }
                            else if (a.direction == Direction.Up && up == true)
                            {
                                a.Position.Y--;
                            }
                            else if (a.direction == Direction.Down && down == true)
                            {
                                a.Position.Y++;
                            }
                            else if (a.direction == Direction.Left && left == true)
                            {
                                a.Position.X--;
                            }
                            else if (a.direction == Direction.Right && right == true)
                            {
                                a.Position.X++;
                            }
                            else
                            {
                                a.direction = (Direction)rnd.Next(0, 4);
                            }

                        }
                        else
                        {
                            a.Destination = (pacManPosition.X, pacManPosition.Y);
                        }
                    }
                    else
                    {
                        a.UpdateFrame++;
                    }
                }
            }

            void RenderGhost()
            {
                Console.SetCursorPosition(a.Position.X, a.Position.Y);
                Console.ForegroundColor = a.Color;
                Console.Write('A');
                Console.ForegroundColor = ConsoleColor.White;
            }

            void Debug()
            {
                //Pacman
                Console.SetCursorPosition(0, 24);
                Console.Write("                                       ");
                Console.SetCursorPosition(0, 24);
                Console.Write(pacManPosition);
                Console.SetCursorPosition(9, 24);
                Console.Write(" score:" + score);
                Console.SetCursorPosition(20, 24);
                Console.Write(pacManDirection);

                //Ghost A
                Console.SetCursorPosition(0, 25);
                Console.Write("                                       ");
                Console.SetCursorPosition(0, 25);
                Console.Write(a.Destination);
            }

            void RenderWalls()
            {
                Console.SetCursorPosition(0, 0);
                x = Console.CursorLeft;
                y = Console.CursorTop;
                foreach (char ca in WallsString)
                {
                    if (ca is '\n')
                    {
                        Console.WriteLine();

                        Console.SetCursorPosition(x, ++y);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(ca);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
            }

            void RenderDots()
            {
                Console.SetCursorPosition(0, 0);
                x = 0;
                y = 0;
                for (y = 0; y < 42; y++)
                {
                    for (x = 0; x < 42; x++)
                    {
                        char ch = countDots[x, y];
                        if (ch == ' ')
                        {
                            Console.CursorLeft++;
                        }
                        else if (ch == '.')
                        {
                            Console.SetCursorPosition(x, y);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(ch);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (ch == '+')
                        {
                            Console.SetCursorPosition(x, y);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(ch);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                    }
                    Console.WriteLine();
                }
            }

            void countDotsCall()
            {
                x = 0;
                y = 0;

                foreach (char ch in DotsString)
                {
                    if (ch == '\n')
                    {
                        countDots[x, y] = ch;
                        y++;
                        x = 0;
                    }
                    else
                    {
                        countDots[x, y] = ch;
                        x++;
                    }
                }
            }

            bool dotEaten()
            {
                if (countDots[pacManPosition.X, pacManPosition.Y] == '.')
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }
    }
}
