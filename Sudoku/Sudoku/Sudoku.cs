using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku
{
    class Sudoku
    {
        Random rnd = new Random();

        private string[] Board { get; set; }

        public Sudoku(string boardString)
        {
            List<string> boardList = new List<string>();
            for (int i = 0; i < boardString.Length; i += 9)
            {
                boardList.Add(boardString.Substring(i, 9));
            }
            Board = boardList.ToArray();
        }

        // Skriver ut brädan i konsollen
        public string BoardAsText()
        {
            var graphicBoard = Board.ToArray();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < graphicBoard.Length; i++)
            {
                graphicBoard[i] = graphicBoard[i].Replace("0", " ");
            }

            sb.AppendLine("\n\t-------------------------------");
            for (int i = 0; i < graphicBoard.Length; i++)
            {
                sb.Append("\t");
                for (int j = 0; j < graphicBoard[i].Length; j++)
                {
                    if (j == 2 || j == 5)
                    {
                        sb.Append(graphicBoard[i][j] + "  |  ");
                    }
                    else
                    {
                        sb.Append(graphicBoard[i][j] + "  ");
                    }

                }
                if (i == 2 || i == 5)
                {
                    sb.AppendLine("\n\t-------------------------------");
                }
                else
                {
                    sb.AppendLine();
                }
            }
            sb.AppendLine("\t-------------------------------");

            return sb.ToString();
        }

        public void Solve()
        {
            string boardString = string.Join("", Board);
            bool isChanged = false;


            while (boardString.Contains('0'))
            {
                for (int y = 0; y < Board.Length; y++)
                {
                    for (int x = 0; x < Board[y].Length; x++)
                    {
                        if (Board[y][x] == '0')
                        {
                            var charList = Board[y].ToCharArray();
                            charList[x] = Convert.ToChar(GetRightNumber(x, y, ref isChanged));
                            Board[y] = string.Join("", charList);
                        }
                    }

                }
                boardString = string.Join("", Board);

                if (!isChanged)
                {
                    Console.WriteLine("Sudokun gick inte att lösa");
                    break;
                }
                isChanged = false;
            }
        }

        public void SolveAndShow()
        {
            string boardString = string.Join("", Board);
            bool isChanged = false;

            Console.WriteLine(BoardAsText());

            while (boardString.Contains('0'))
            {
                for (int y = 0; y < Board.Length; y++)
                {
                    for (int x = 0; x < Board[y].Length; x++)
                    {
                        if (Board[y][x] == '0')
                        {
                            var charList = Board[y].ToCharArray();
                            charList[x] = Convert.ToChar(GetRightNumber(x, y, ref isChanged));
                            Board[y] = string.Join("", charList);

                            Console.Clear();
                            Console.WriteLine(BoardAsText());
                            Thread.Sleep(50);
                        }
                    }

                }
                boardString = string.Join("", Board);

                if (!isChanged)
                {
                    Console.WriteLine("Sudokun gick inte att lösa");
                    break;
                }
                isChanged = false;
            }
            Console.Clear();
            Console.WriteLine(BoardAsText());
        }

        private string GetRightNumber(int x, int y, ref bool isChanged)
        {
            List<int> possibleNumbers = new List<int>();
            for (int i = 1; i <= 9; i++)
            {
                bool isNumberInBox = ContainsNumber(x, y, Zones.Box, i);
                bool isNumberInHorizontal = ContainsNumber(x, y, Zones.Horizontal, i);
                bool isNumberInVertical = ContainsNumber(x, y, Zones.Vertical, i);

                if (!isNumberInBox && !isNumberInHorizontal && !isNumberInVertical)
                {
                    possibleNumbers.Add(i);
                }
            }
            if (possibleNumbers.Count == 1)
            {
                isChanged = true;
                
                return possibleNumbers[0].ToString();
            }
            return "0";
        }

        private bool ContainsNumber(int x, int y, Zones zone, int numberToLookFor)
        {
            var numbers = GetListOfNumbers(x, y, zone);
            bool isNumberPresent = numbers.Contains(numberToLookFor.ToString());
            return isNumberPresent;
        }

        private string GetListOfNumbers(int x, int y, Zones zone)
        {
            StringBuilder sb = new StringBuilder();

            if (zone == Zones.Box)
            {
                sb.Append(GetBox(x, y));
            }

            else if (zone == Zones.Horizontal)
            {
                sb.Append(Board[y]);
            }
            else
            {
                for (int i = 0; i < Board.Length; i++)
                {
                    sb.Append(Board[i][x]);
                }
            }
            return sb.ToString();
        }

        // Hjälpmetod till GetListOfNumbers som returnerar en sträng av alla nummer i den aktuella boxen
        private string GetBox(int x, int y)
        {
            // Hämtar Y-värdena
            List<int> yList = new List<int>();
            if (y < 3)
                yList.AddRange(new int[] { 0, 1, 2 });
            else if (y < 6)
                yList.AddRange(new int[] { 3, 4, 5 });
            else if (y < 9)
                yList.AddRange(new int[] { 6, 7, 8 });

            if (x < 3)
            {
                return GetBoxValues(0, yList);
            }
            else if (x < 6)
            {
                return GetBoxValues(3, yList);
            }
            else
            {
                return GetBoxValues(6, yList);
            }
        }

        // Hjälpmetod till GetBox som hämtar en sträng baserad på aktuellt x och y värde
        private string GetBoxValues(int x, List<int> yList)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sb.Append(Board[yList[i]][j + x]);
                }
            }

            return sb.ToString();
        }


        private enum Zones
        {
            Horizontal,
            Vertical,
            Box
        }

    }

}
