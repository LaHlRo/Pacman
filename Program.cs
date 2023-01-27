using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Practice4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            bool endGame = true;
            int scoreCounter = 0;
            int pacmanX = 1;
            int pacmanY = 1;

            char[,] map = ReadMap("map.txt");

            SetForeGroundColor(ConsoleColor.Yellow);
            Console.WriteLine("Press enter to start the game");

            ConsoleKeyInfo charKey = Console.ReadKey();

            Task.Run(() =>
            {
                while (endGame)
                {
                    charKey = Console.ReadKey();
                }
            });

            PointsGenereation(map, ref scoreCounter);

            while (endGame)
            {
                Console.Clear();
                ResetForeGroundColor();

                PacmanMovement(charKey, map, ref pacmanX, ref pacmanY);

                DrawMap(map);

                Console.SetCursorPosition(pacmanX, pacmanY);

                SetForeGroundColor(ConsoleColor.Yellow);
                Console.Write('С');

                PointsCollection(map, ref pacmanX, ref pacmanY, ref scoreCounter);

                EndGame(ref scoreCounter, ref endGame);
                ResetForeGroundColor();
                
                Thread.Sleep(62);
            }
        }

        private static void PacmanMovement(ConsoleKeyInfo key, char[,] array, ref int xPos, ref int yPos)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (array[xPos, yPos - 1] != '#')
                        yPos--;
                    break;
                case ConsoleKey.DownArrow:
                    if (array[xPos, yPos + 1] != '#')
                        yPos++;
                    break;
                case ConsoleKey.RightArrow:
                    if (array[xPos + 1, yPos] != '#')
                        xPos++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (array[xPos - 1, yPos] != '#')
                        xPos--;
                    break;
                default:
                    break;
            }
        }

        private static void DrawMap(char[,] array)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                    if (array[i, j] == '.')
                    {
                        SetForeGroundColor(ConsoleColor.DarkYellow);
                        Console.Write(array[i, j]);
                    }
                    else
                    {
                        SetForeGroundColor(ConsoleColor.Blue);
                        Console.Write(array[i, j]);
                    }
                Console.WriteLine();
            }
        }

        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines("map.txt");

            char[,] map = new char[GetMaxLengthOfLine(file), file.Length];

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j] = file[j][i];

            return map;
        }

        private static int GetMaxLengthOfLine(string[] lines)
        {
            int maxLength = lines[0].Length;

            foreach (var line in lines)
                if (line.Length > maxLength)
                    maxLength = line.Length;

            return maxLength;
        }

        private static void SetForeGroundColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        private static void ResetForeGroundColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void PointsGenereation(char[,] array, ref int counter)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] != '#')
                    {
                        array[i, j] = '.';
                        counter += 1;
                    }
                }
            }
        }

        private static void PointsCollection(char[,] array, ref int xPos, ref int yPos, ref int counter)
        {
            if (array[xPos, yPos] == '.')
            {
                array[xPos, yPos] = ' ';
                counter--;
            }
        }

        private static void EndGame(ref int counter, ref bool endGame)
        {
            if (counter == 0)
            {
                endGame = false;
                Console.Clear();
                Console.WriteLine("You've win!!!");
            }
                
        }
    }
}
