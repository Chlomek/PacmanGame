using System;
using System.Collections.Generic;
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

        class PacMan
        {
            public void RenderPacMan((int X, int Y) position)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write('C');
                Console.ForegroundColor = ConsoleColor.White;
            }
            public void UpdatePacMan((int X, int Y) position)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write(' ');
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
        }

        string GhostWallsString =
"╔═══════════════════╦═══════════════════╗ \n" +
"║█                 █║█                 █║\n" +
"║█ █╔═╗█ █╔═════╗█ █║█ █╔═════╗█ █╔═╗█ █║\n" +
"║█ █╚═╝█ █╚═════╝█ █╨█ █╚═════╝█ █╚═╝█ █║\n" +
"║█                                     █║\n" +
"║█ █═══█ █╥█ █══════╦══════█ █╥█ █═══█ █║\n" +
"║█       █║█       █║█       █║█       █║\n" +
"╚═════╗█ █╠══════█ █╨█ █══════╣█ █╔═════╝\n" +
"██████║█ █║█                 █║█ █║██████\n" +
"══════╝█ █╨█ █╔════█ █════╗█ █╨█ █╚══════\n" +
"             █║█         █║█             \n" +
"══════╗█  ╥█ █║███████████║█ █╥█ █╔══════\n" +
"██████║█  ║█ █╚═══════════╝█ █║█ █║██████\n" +
"██████║█  ║█                 █║█ █║██████\n" +
"╔═════╝█  ╨█ █══════╦══════█ █╨█ █╚═════╗\n" +
"║█                 █║█                 █║\n" +
"║█ █══╗█ █═══════█ █╨█ █═══════█ █╔══█ █║\n" +
"║█   █║█                         █║█   █║\n" +
"╠══█ █╨█ █╥█ █══════╦══════█ █╥█ █╨█ █══╣\n" +
"║█       █║█       █║█       █║█       █║\n" +
"║█ █══════╩══════█ █╨█ █══════╩══════█ █║\n" +
"║█                                     █║\n" +
"╚═══════════════════════════════════════╝";


        static void Main(string[] args)
        {
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
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            PacMan pacMan = new PacMan();
            Walls walls = new Walls();
            int score = 0;
            (int X, int Y) pacManPosition;

            pacManPosition = (1, 1);

            
                walls.RenderWalls(WallsString);
                walls.RenderDots(DotsString);
            while (true)
            {
                //pacMan.UpdatePacMan(pacManPosition);

                Console.SetCursorPosition(pacManPosition.X, pacManPosition.Y);
                if (Console.Read() == '.')
                {
                    score++;
                }
                pacManPosition = (pacManPosition.X + 1, pacManPosition.Y);

                pacMan.RenderPacMan(pacManPosition);
                Console.ReadLine();
            }
        }
    }
}
