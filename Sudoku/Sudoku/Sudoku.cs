using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku
{
    class Sudoku
    {
        Random rnd = new Random();

        private class Guess
        {
            public List<int> NumbersGuessed { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }

        private List<Guess> Guesses { get; set; } = new List<Guess>();

        private char[,] Board { get; set; } = new char[9, 9];

        public Sudoku(string boardString)
        {
            List<string> boardList = new List<string>();
            for (int i = 0; i < boardString.Length; i += 9)
            {
                boardList.Add(boardString.Substring(i, 9));
            }

            for (int y = 0; y < boardList.Count; y++)
            {
                for (int x = 0; x < boardList[y].Length; x++)
                {
                    Board[y, x] = boardList[y][x];
                }
            }
        }

        // Skriver ut brädan i konsollen
        public string BoardAsText()
        {
            var graphicBoard = Board;

            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (graphicBoard[y, x] == '0')
                    {
                        graphicBoard[y, x] = ' ';
                    }
                }
            }

            sb.AppendLine("\n\t-------------------------------");
            for (int y = 0; y < 9; y++)
            {
                sb.Append("\t");
                for (int x = 0; x < 9; x++)
                {
                    if (x == 2 || x == 5)
                    {
                        sb.Append(graphicBoard[y, x] + "  |  ");
                    }
                    else
                    {
                        sb.Append(graphicBoard[y, x] + "  ");
                    }

                }
                if (y == 2 || y == 5)
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
                for (int y = 0; y < 9; y++)
                {
                    for (int x = 0; x < 9; x++)
                    {
                        if (Board[y, x] == '0')
                        {
                            Board[y, x] = GetRightNumber(x, y, ref isChanged);
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
                for (int y = 0; y < 9; y++)
                {
                    for (int x = 0; x < 9; x++)
                    {
                        if (Board[y, x] == '0')
                        {
                            Board[y, x] = GetRightNumber(x, y, ref isChanged);

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

        private char GetRightNumber(int x, int y, ref bool isChanged)
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

                return Convert.ToChar(possibleNumbers[0]);
            }
            return '0';
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
                for (int xPos = 0; xPos < 9; xPos++)
                {
                    sb.Append(Board[y, xPos]);
                }
            }
            else
            {
                for (int yPos = 0; yPos < Board.Length; yPos++)
                {
                    sb.Append(Board[yPos, x]);
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
                    sb.Append(Board[yList[i],j + x]);
                }
            }

            return sb.ToString();
        }
        //Metod för att skapa en lista med kandidater för varje cell i sudokun.
        private List<int> GetPossibleNumbers(int x, int y)
        {
            List<int> possibleNumbersTemp = new List<int>();
            for (int i = 1; i < 9; i++)
            {
                bool isNumberInBox = ContainsNumber(x, y, Zones.Box, i);
                bool isNumberInHorizontal = ContainsNumber(x, y, Zones.Horizontal, i);
                bool isNumberInVertical = ContainsNumber(x, y, Zones.Vertical, i);

                if (!isNumberInBox && !isNumberInHorizontal && !isNumberInVertical)
                {
                    possibleNumbersTemp.Add(i);
                }
            }
            return possibleNumbersTemp;
        }

        //Om första solven misslyckas att lösa tar recursiveSolve över. 
        //Hittar första cellen med minst möjliga alternativ och gissar på det första. Hoppar till nästa
        //och upprepar tills sudokun löser sig eller det blir en konflikt. 
        public void RecursiveSolve()
        {
            List<List<List<int>>> possibleNumbers = new List<List<List<int>>>();
            for (int i = 0; i < 9; i++)
            {
                possibleNumbers.Add(new List<List<int>>());

            }

            List<int> subList = new List<int>();
            Console.WriteLine(BoardAsText());

            for (int y = 0; y < Board.Length; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (Board[y,x] == '0')
                    {
                        subList = GetPossibleNumbers(x, y);
                        possibleNumbers[y].Add(subList);
                    }
                    else
                    {
                        possibleNumbers[y].Add(new List<int>());
                    }

                }
            }
            RecursiveSolver(possibleNumbers);


        }

        public void RecursiveSolver(List<List<List<int>>> possibleNumbers)
        {
            string recursiveBoardString = Board.ToString();
            char[,] recursiveBoardArr = new char[9, 9];
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    recursiveBoardArr[y,x] = Board[]
                }
            }





            //char[] recursiveBoardArr = Board.ToArray();

            while (recursiveBoardString.Contains('0'))
            {

                for (int y = 0; y < recursiveBoardArr.Length; y++)
                {

                    for (int x = 0; x < recursiveBoardArr[y].Length; x++)
                    {

                        if (recursiveBoardArr[x,y] == '0')
                        {
                            recursiveBoardArr[y] = possibleNumbers[1].ToString();
                        }
                        else
                        {
                            Console.WriteLine("Test");
                        }
                    }
                }
            }

        }
        public int[] FindLowestValue(List<List<List<int>>> possibleList)
        {
            int[] indexCounter = new int[2];
            int counter = 8;

            for (int y = 0; y < possibleList.Count; y++)
            {
                for (int x = 0; x < possibleList[y].Count; x++)
                {
                    if (counter > possibleList[y][x].Count)
                    {
                        counter = possibleList[y][x].Count;
                        indexCounter[0] = y;
                        indexCounter[1] = x;

                    }
                }
            }
            return indexCounter;
        }

        //public bool Guesser(/*Kopia av brädan*/)
        //{

        //    List<List<List<int>>> possibleNumbersOfBoard = new List<List<List<int>>>();
        //    for (int i = 0; i < 9; i++)
        //    {
        //        possibleNumbersOfBoard.Add(new List<List<int>>());
        //        for (int j = 0; j < 9; j++)
        //        {
        //            possibleNumbersOfBoard[i].Add(new List<int>());
        //        }
        //    }
        //    for (int y = 0; y < 9; y++)
        //    {
        //        for (int x = 0; x < 9; x++)
        //        {
        //            possibleNumbersOfBoard[x].Add(GetPossibleNumbers(x, y));
        //        }
        //    }

        //    // Hittar cell med lägst alternativ, jämför med de sparade också

        //    // Gissa en av siffrorna

        //    // Spara index och gissad siffra

        //    // Prova lösa sudokut, returnera true eller false

        //    // Är sudokut löst så returnas true i annat fall anropas Guess rekursivt


        //}

        private enum Zones
        {
            Horizontal,
            Vertical,
            Box
        }

    }

}
