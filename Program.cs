using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

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
            Console.SetWindowSize(42, 50);
            Console.SetBufferSize(42, 50);
            Console.Title = "PacMan";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            Random rnd = new Random();
            int score = 0;
            Direction? pacManDirection = default;
            Direction? pacManMovingDirection = default;
            int pacManFrame = 0;
            const int framesToHorizontal = 4;
            const int framesToVertical = 4;
            int GhostWeakTime = 20;
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

            int pacManLives = 3;
            int SetGhostWeakTime = 1000;
            int pacManSpeedBonus = 0;

            void ResetGhosts()
            {
                a.Color = ConsoleColor.Red;
                a.StartingPosition = (20, 10);
                a.Position = a.StartingPosition;
                a.Start = 3;
                a.UpdateFrame = -30;
                a.FramesToUpdate = 5;

                b.Color = ConsoleColor.Yellow;
                b.StartingPosition = (20, 10);
                b.Position = b.StartingPosition;
                b.Start = 3;
                b.UpdateFrame = -90;
                b.FramesToUpdate = 5;

                c.Color = ConsoleColor.Cyan;
                c.StartingPosition = (20, 10);
                c.Position = c.StartingPosition;
                c.Start = 3;
                c.UpdateFrame = -150;
                c.FramesToUpdate = 8;

                d.Color = ConsoleColor.Magenta;
                d.StartingPosition = (20, 10);
                d.Position = d.StartingPosition;
                d.Start = 3;
                d.UpdateFrame = -210;
                d.FramesToUpdate = 8;
            }

            string username = "Player";
            Menu();
            pacManPosition = (20, 13);
            ResetGhosts();
            countDotsCall();

            //Console.SetCursorPosition(0, 0);
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
                    ResetGhosts();
                    pacManDirection = null;
                    FirstDirectionInput();
                }

                Thread.Sleep(40);
                //RenderDots();
                HandleInput();
                UpdatePacMan();
                RenderPacMan();

                UpdateGhost(a);
                UpdateGhost(b);
                UpdateGhost(c);
                UpdateGhost(d);

                RenderGhost(a);
                RenderGhost(b);
                RenderGhost(c);
                RenderGhost(d);



                Debug();
            }

            bool FirstDirectionInput()
            {
            GetInput:
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow: pacManMovingDirection = Direction.Left; break;
                    case ConsoleKey.RightArrow: pacManMovingDirection = Direction.Right; break;
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

            bool AbleToMove(int xc, int yc, Direction? direction, char[,] Walls)
            {
                switch (direction)
                {
                    case Direction.Up:
                        if (Walls[xc, yc - 1] == ' ')
                            return true;
                        else
                            return false;
                    case Direction.Down:
                        if (Walls[xc, yc + 1] == ' ')
                            return true;
                        else
                            return false;
                    case Direction.Left:
                        if (Walls[xc - 1, yc] == ' ')
                            return true;
                        else
                            return false;
                    case Direction.Right:
                        if (Walls[xc + 1, yc] == ' ')
                            return true;
                        else
                            return false;
                    default:
                        return false;
                }
            }

            bool AbleToMovePacMan()
            {
                switch (pacManMovingDirection)
                {
                    case Direction.Up:
                        if (GhostWalls[pacManPosition.X, pacManPosition.Y - 1] == ' ')
                        {
                            pacManMovingDirection = Direction.Up;
                            return true;
                        }
                        else
                            return false;

                    case Direction.Down:
                        if (GhostWalls[pacManPosition.X, pacManPosition.Y + 1] == ' ')
                        {
                            pacManMovingDirection = Direction.Down;
                            return true;
                        }
                        else
                            return false;

                    case Direction.Left:
                        if (GhostWalls[pacManPosition.X - 1, pacManPosition.Y] == ' ')
                        {
                            pacManMovingDirection = Direction.Left;
                            return true;
                        }
                        else
                            return false;

                    case Direction.Right:
                        if (GhostWalls[pacManPosition.X + 1, pacManPosition.Y] == ' ')
                        {
                            pacManMovingDirection = Direction.Right;
                            return true;
                        }
                        else
                            return false;

                    default:
                        return false;
                }
            }

            void UpdatePacMan()
            {
                if ((pacManMovingDirection == Direction.Left || pacManMovingDirection == Direction.Right) && pacManFrame >= framesToHorizontal - pacManSpeedBonus ||
                         (pacManMovingDirection == Direction.Up || pacManMovingDirection == Direction.Down) && pacManFrame >= framesToVertical - pacManSpeedBonus)
                {
                    if (GhostWeakTime == 0)
                    {
                        pacManSpeedBonus = 0;
                    }
                    if (AbleToMove(pacManPosition.X, pacManPosition.Y, pacManDirection, GhostWalls))
                    {
                        pacManFrame = 0;
                        Console.SetCursorPosition(pacManPosition.X, pacManPosition.Y);
                        Console.Write(' ');
                        switch (pacManDirection)
                        {
                            case Direction.Up:
                                pacManPosition.Y--;
                                pacManMovingDirection = Direction.Up;
                                break;
                            case Direction.Down:
                                pacManPosition.Y++;
                                pacManMovingDirection = Direction.Down;
                                break;
                            case Direction.Left:
                                pacManPosition.X--;
                                pacManMovingDirection = Direction.Left;
                                break;
                            case Direction.Right:
                                pacManPosition.X++;
                                pacManMovingDirection = Direction.Right;
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
                        }
                    }
                    else if (AbleToMove(pacManPosition.X, pacManPosition.Y, pacManMovingDirection, GhostWalls))
                    {
                        pacManFrame = 0;
                        Console.SetCursorPosition(pacManPosition.X, pacManPosition.Y);
                        Console.Write(' ');
                        switch (pacManMovingDirection)
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
                            pacManPosition.X = 39;
                        }
                        else if (pacManPosition.X >= 39)
                        {
                            pacManPosition.X = 0;
                        }
                        if (dotEaten() == true)
                        {
                            countDots[pacManPosition.X, pacManPosition.Y] = ' ';
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

            void UpdateGhost(Ghost ghost)
            {
                Kill(ghost);
                Console.SetCursorPosition(ghost.Position.X, ghost.Position.Y);
                if (countDots[ghost.Position.X, ghost.Position.Y] == '.')
                {
                    Console.SetCursorPosition(ghost.Position.X, ghost.Position.Y);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(countDots[ghost.Position.X, ghost.Position.Y]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (countDots[ghost.Position.X, ghost.Position.Y] == '+')
                {
                    Console.SetCursorPosition(ghost.Position.X, ghost.Position.Y);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(countDots[ghost.Position.X, ghost.Position.Y]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write(" ");
                }

                if (GhostWeakTime != 0)
                    GhostWeakTime--;

                if (ghost.Start > 1)
                {
                    if (ghost.UpdateFrame >= ghost.FramesToUpdate)
                    {
                        switch (ghost.Start)
                        {
                            case 3:
                                ghost.Position = (20, 9);
                                ghost.Start--;
                                break;
                            case 2:
                                ghost.Position = (20, 8);
                                ghost.Start--;
                                break;
                        }
                        ghost.UpdateFrame = 0;
                    }
                    else
                    {
                        ghost.UpdateFrame++;
                    }
                }
                else
                {
                    if (ghost.UpdateFrame >= ghost.FramesToUpdate)
                    {
                        ghost.UpdateFrame = 0;

                        bool up = AbleToMove(ghost.Position.X, ghost.Position.Y, Direction.Up, GhostWalls);
                        bool down = AbleToMove(ghost.Position.X, ghost.Position.Y, Direction.Down, GhostWalls);
                        bool left = AbleToMove(ghost.Position.X, ghost.Position.Y, Direction.Left, GhostWalls);
                        bool right = AbleToMove(ghost.Position.X, ghost.Position.Y, Direction.Right, GhostWalls);

                        ghost.Destination = (pacManPosition.X, pacManPosition.Y);

                        if (ghost.direction == Direction.Left && left == true && (up == false && down == false))
                        {
                            ghost.Position.X--;
                        }
                        else if (ghost.direction == Direction.Up && up == true && (right == false && left == false))
                        {
                            ghost.Position.Y--;
                        }
                        else if (ghost.direction == Direction.Right && right == true && (up == false && down == false))
                        {
                            ghost.Position.X++;
                        }
                        else if (ghost.direction == Direction.Down && down == true && (right == false && left == false))
                        {
                            ghost.Position.Y++;
                        }

                        else if (ghost.Position.X < ghost.Destination.Value.X && (ghost.Position.X - ghost.Destination.Value.X < ghost.Position.Y - ghost.Destination.Value.Y) && right == true && ghost.direction != Direction.Left)
                        {
                            ghost.Position.X++;
                            ghost.direction = Direction.Right;
                        }
                        else if (ghost.Position.X > ghost.Destination.Value.X && (ghost.Position.X - ghost.Destination.Value.X < ghost.Position.Y - ghost.Destination.Value.Y) && left == true && ghost.direction != Direction.Right)
                        {
                            ghost.Position.X--;
                            ghost.direction = Direction.Left;
                        }
                        else if (ghost.Position.Y < ghost.Destination.Value.Y && (ghost.Position.X - ghost.Destination.Value.X > ghost.Position.Y - ghost.Destination.Value.Y) && down == true && ghost.direction != Direction.Up)
                        {
                            ghost.Position.Y++;
                            ghost.direction = Direction.Down;
                        }
                        else if (ghost.Position.Y > ghost.Destination.Value.Y && (ghost.Position.X - ghost.Destination.Value.X > ghost.Position.Y - ghost.Destination.Value.Y) && up == true && ghost.direction != Direction.Down)
                        {
                            ghost.Position.Y--;
                            ghost.direction = Direction.Up;
                        }

                        else if (ghost.Position.X < ghost.Destination.Value.X && right == true && ghost.direction != Direction.Left)
                        {
                            ghost.Position.X++;
                            ghost.direction = Direction.Right;
                        }
                        else if (ghost.Position.X > ghost.Destination.Value.X && left == true && ghost.direction != Direction.Right)
                        {
                            ghost.Position.X--;
                            ghost.direction = Direction.Left;
                        }
                        else if (ghost.Position.Y < ghost.Destination.Value.Y && down == true && ghost.direction != Direction.Up)
                        {
                            ghost.Position.Y++;
                            ghost.direction = Direction.Down;
                        }
                        else if (ghost.Position.Y > ghost.Destination.Value.Y && up == true && ghost.direction != Direction.Down)
                        {
                            ghost.Position.Y--;
                            ghost.direction = Direction.Up;
                        }
                        else if (ghost.direction == Direction.Up && up == true)
                        {
                            ghost.Position.Y--;
                        }
                        else if (ghost.direction == Direction.Down && down == true)
                        {
                            ghost.Position.Y++;
                        }
                        else if (ghost.direction == Direction.Left && left == true)
                        {
                            ghost.Position.X--;
                        }
                        else if (ghost.direction == Direction.Right && right == true)
                        {
                            ghost.Position.X++;
                        }
                        else
                        {
                            ghost.direction = (Direction)rnd.Next(0, 4);
                        }
                        Kill(ghost);
                    }
                    else
                    {
                        ghost.UpdateFrame++;
                    }
                }
            }


            void Kill(Ghost ghost)
            {
                if (ghost.Position.X == pacManPosition.X && ghost.Position.Y == pacManPosition.Y && GhostWeakTime == 0)
                {
                    pacManLives--;
                    if (pacManLives == 0)
                    {
                        Console.Clear();

                        Console.SetWindowSize(30, 50);
                        Console.SetBufferSize(30, 50);

                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        SaveToLeaderboard();
                        Console.Write("Game Over");
                        Console.ReadKey();
                        Menu();
                    }
                    else
                    {
                        RenderWalls();
                        RenderDots();
                        pacManPosition = (20, 13);
                        ResetGhosts();
                        pacManDirection = null;
                        FirstDirectionInput();
                    }
                }
                else if (ghost.Position.X == pacManPosition.X && ghost.Position.Y == pacManPosition.Y && GhostWeakTime > 0)
                {
                    ghost.Position = ghost.StartingPosition;
                    ghost.Start = 3;
                    ghost.UpdateFrame = -100;
                    score += 200;
                }
            }

            void RenderGhost(Ghost ghost)
            {
                Console.SetCursorPosition(ghost.Position.X, ghost.Position.Y);
                if (GhostWeakTime > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.ForegroundColor = ghost.Color;
                }
                Console.Write('G');
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
                Console.SetCursorPosition(26, 24);
                Console.Write("Lives: " + pacManLives + "   ");
                Console.Write(GhostWeakTime);

                //Ghost A
                Console.SetCursorPosition(0, 25);
                Console.Write("                                       ");
                Console.SetCursorPosition(0, 25);
                Console.Write(a.Destination + "  " + a.direction);
                //Ghost B
                Console.SetCursorPosition(0, 26);
                Console.Write("                                       ");
                Console.SetCursorPosition(0, 26);
                Console.Write(b.Destination + "  " + b.direction);
                //Ghost C
                Console.SetCursorPosition(0, 27);
                Console.Write("                                       ");
                Console.SetCursorPosition(0, 27);
                Console.Write(c.Destination + "  " + c.direction);
                //Ghost D
                Console.SetCursorPosition(0, 28);
                Console.Write("                                       ");
                Console.SetCursorPosition(0, 28);
                Console.Write(a.Destination + "  " + d.direction);
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
                Console.SetCursorPosition(0, 0);
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
                    dotsCollected++;
                    score += 10;
                    return true;
                }
                else if (countDots[pacManPosition.X, pacManPosition.Y] == '+')
                {
                    GhostWeakTime = SetGhostWeakTime;
                    pacManSpeedBonus = 1;
                    return true;
                }
                else
                {
                    return false;
                }

            }

            void Menu()
            {
                Console.SetWindowSize(20, 10);
                Console.SetBufferSize(20, 10);

                int selected = 0;
                while (true)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Pacman                     ");
                    Console.SetCursorPosition(0, 1);
                    if (selected == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("> Start                 ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine("Start                  ");
                    }
                    Console.SetCursorPosition(0, 2);
                    if (selected == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("> Leaderboard                 ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine("Leaderboard                ");
                    }
                    Console.SetCursorPosition(0, 3);
                    if (selected == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("> Exit                              ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine("Exit                         ");
                    }
                    ConsoleKey key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            if (selected > 0)
                            {
                                selected--;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (selected < 2)
                            {
                                selected++;
                            }
                            break;
                        case ConsoleKey.Enter:
                            switch (selected)
                            {
                                case 0:
                                    Console.Clear();
                                    selected = 0;
                                    Console.SetWindowSize(42, 50);
                                    Console.SetBufferSize(42, 50);
                                    return;
                                case 1:
                                    Leaderboard();
                                    selected = 0;
                                    break;
                                case 2:
                                    Environment.Exit(0);
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            void Leaderboard()
            {
                Console.Clear();
                int row = 1;
                Console.WriteLine("Leaderboard");
                Console.SetCursorPosition(0, 0);
                foreach (string line in File.ReadAllLines("Leaderboard.txt"))
                {
                    if (line.Contains("-"))
                    {
                        Console.SetCursorPosition(0, row);
                        Console.Write(line);
                        row++;
                        if (row > 10)
                        {
                            return;
                        }
                    }
                }
                Console.ReadKey();
                Console.Clear();
            }

            void SaveToLeaderboard()
            {
                Console.SetCursorPosition(0, 0);
                Console.Write("Enter your username: ");
                username = Console.ReadLine();
                using (StreamWriter writer = new StreamWriter("Leaderboard.txt"))
                {
                    writer.WriteLine(score + " - " + username);
                }
            }
        }
    }
}


