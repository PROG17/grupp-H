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

            public Guess(int y, int x, int guess)
            {
                NumbersGuessed = new List<int>();
                X = x;
                Y = y;
                NumbersGuessed.Add(guess);
            }
        }

        private class Cell
        {
            public char value;
            public int X { get; set; }
            public int Y { get; set; }

            public Cell(int y, int x, char value)
            {
                this.value = value;

            }
        }

        private Stack<Guess> Guesses { get; set; } = new Stack<Guess>();

        public char[,] Board { get; set; } = new char[9, 9];

        public static char[,] TryBoard { get; set; }

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
        public string BoardAsText(char[,] board)
        {
            var graphicBoard = new char[9, 9];

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    graphicBoard[y, x] = board[y, x];
                }
            }


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
            if (!TrySolve(Board))
            {
                Guesser(Board);
            }
        }

        private bool TrySolve(char[,] board)
        {
            //string boardString = string.Join("", board);

            bool isChanged = false;
            int changeCounter = 0;
            while (GetStringRepOfBoard(board).Contains('0'))
            {
                for (int y = 0; y < 9; y++)
                {
                    for (int x = 0; x < 9; x++)
                    {
                        if (board[y, x] == '0')
                        {
                            var c = GetRightNumber(board, x, y, ref isChanged);
                            if (isChanged)
                            {
                                changeCounter++;

                            }
                            board[y, x] = c;
                        }
                    }

                }

                if (changeCounter == 0)
                {
                    Console.WriteLine(BoardAsText(board));
                    Console.ReadLine();
                    return false;
                }

                changeCounter = 0;
                isChanged = false;
            }
            return true;
        }

        private static string GetStringRepOfBoard(char[,] board)
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    sb.Append(board[y, x]);
                }
            }
            return sb.ToString();
        }

        //public void SolveAndShow()
        //{
        //    string boardString = string.Join("", Board);
        //    bool isChanged = false;

        //    Console.WriteLine(BoardAsText());

        //    while (boardString.Contains('0'))
        //    {
        //        for (int y = 0; y < 9; y++)
        //        {
        //            for (int x = 0; x < 9; x++)
        //            {
        //                if (Board[y, x] == '0')
        //                {
        //                    Board[y, x] = GetRightNumber(Board, x, y, ref isChanged);

        //                    Console.Clear();
        //                    Console.WriteLine(BoardAsText());
        //                    Thread.Sleep(50);
        //                }
        //            }

        //        }
        //        boardString = string.Join("", Board);

        //        if (!isChanged)
        //        {
        //            Console.WriteLine("Sudokun gick inte att lösa");

        //            break;
        //        }
        //        isChanged = false;
        //    }
        //    Console.Clear();
        //    Console.WriteLine(BoardAsText());
        //}

        private char GetRightNumber(char[,] board, int x, int y, ref bool isChanged)
        {
            List<int> possibleNumbers = new List<int>();
            for (int i = 1; i <= 9; i++)
            {
                bool isNumberInBox = ContainsNumber(board, x, y, Zones.Box, i);
                bool isNumberInHorizontal = ContainsNumber(board, x, y, Zones.Horizontal, i);
                bool isNumberInVertical = ContainsNumber(board, x, y, Zones.Vertical, i);

                if (!isNumberInBox && !isNumberInHorizontal && !isNumberInVertical)
                {
                    possibleNumbers.Add(i);
                }
            }
            if (possibleNumbers.Count == 1)
            {
                isChanged = true;

                return possibleNumbers[0].ToString()[0];
            }
            return '0';
        }

        private bool ContainsNumber(char[,] board, int x, int y, Zones zone, int numberToLookFor)
        {
            var numbers = GetListOfNumbers(board, x, y, zone);
            bool isNumberPresent = numbers.Contains(numberToLookFor.ToString());
            return isNumberPresent;
        }

        private string GetListOfNumbers(char[,] board, int x, int y, Zones zone)
        {
            StringBuilder sb = new StringBuilder();

            if (zone == Zones.Box)
            {
                sb.Append(GetBox(board, x, y));
            }

            else if (zone == Zones.Horizontal)
            {
                for (int xPos = 0; xPos < 9; xPos++)
                {
                    sb.Append(board[y, xPos]);
                }
            }
            else
            {
                for (int yPos = 0; yPos < 9; yPos++)
                {
                    sb.Append(board[yPos, x]);
                }
            }
            return sb.ToString();
        }

        // Hjälpmetod till GetListOfNumbers som returnerar en sträng av alla nummer i den aktuella boxen
        private string GetBox(char[,] board, int x, int y)
        {
            StringBuilder sb = new StringBuilder();
            int upperRow = (y / 3) * 3;
            int leftCol = (x / 3) * 3;

            // Gå igenom boxens tre rader och kolumner
            for (int boxRow = 0; boxRow < 3; boxRow++)
            {
                for (int boxCol = 0; boxCol < 3; boxCol++)
                {
                    sb.Append(board[upperRow + boxRow, leftCol + boxCol]);
                }

            }
            return sb.ToString();
        }

        //Metod för att skapa en lista med kandidater för varje cell i sudokun.
        private Stack<int> GetPossibleNumbers(char[,] board, int x, int y)
        {
            Stack<int> possibleNumbersTemp = new Stack<int>();
            for (int i = 1; i <= 9; i++)
            {
                bool isNumberInBox = ContainsNumber(board, x, y, Zones.Box, i);
                bool isNumberInHorizontal = ContainsNumber(board, x, y, Zones.Horizontal, i);
                bool isNumberInVertical = ContainsNumber(board, x, y, Zones.Vertical, i);

                if (!isNumberInBox && !isNumberInHorizontal && !isNumberInVertical)
                {
                    if (board[y, x] == '0')
                    {
                        possibleNumbersTemp.Push(i);
                    }
                }
            }
            return possibleNumbersTemp;
        }

        //Om första solven misslyckas att lösa tar recursiveSolve över. 
        //Hittar första cellen med minst möjliga alternativ och gissar på det första. Hoppar till nästa
        //och upprepar tills sudokun löser sig eller det blir en konflikt. 
        //public void RecursiveSolve()
        //{
        //    List<List<Stack<int>>> possibleNumbers = new List<List<Stack<int>>>();
        //    for (int i = 0; i < 9; i++)
        //    {
        //        possibleNumbers.Add(new List<Stack<int>>());

        //    }

        //    Stack<int> subList = new Stack<int>();
        //    Console.WriteLine(BoardAsText());

        //    for (int y = 0; y < Board.Length; y++)
        //    {
        //        for (int x = 0; x < 9; x++)
        //        {
        //            if (Board[y,x] == '0')
        //            {
        //                subList = GetPossibleNumbers(board, x, y);
        //                possibleNumbers[y].Add(subList);
        //            }
        //            else
        //            {
        //                possibleNumbers[y].Add(new Stack<int>());
        //            }

        //        }
        //    }
        //    RecursiveSolver(possibleNumbers);


        //}

        //public void RecursiveSolver(List<List<Stack<int>>> possibleNumbers)
        //{
        //    string recursiveBoardString = Board.ToString();
        //    char[,] recursiveBoardArr = new char[9, 9];
        //    for (int y = 0; y < 9; y++)
        //    {
        //        for (int x = 0; x < 9; x++)
        //        {
        //            recursiveBoardArr[y, x] = Board[y, x];
        //        }
        //    }

        //}

        // Returnerar en int-array av koordinater för platsen i possibleList som har lägst
        // antal möjliga nummer. Om alla är 0 returneras 0,0
        public int[] FindLowestValue(List<List<Stack<int>>> possibleList)
        {
            int[] indexCounter = new int[2];
            int counter = 8;

            for (int y = 0; y < possibleList.Count; y++)
            {
                for (int x = 0; x < possibleList[y].Count; x++)
                {
                    if (possibleList[y][x].Count != 0)
                    {
                        if (counter > possibleList[y][x].Count)
                        {
                            counter = possibleList[y][x].Count;
                            indexCounter[0] = y;
                            indexCounter[1] = x;

                        }
                    }

                }
            }
            return indexCounter;
        }

        public bool Guesser(char[,] board)
        {
            // Ny board för att inte pajja gamla board
            var tryBoard = new char[9, 9];
            for (int yPos = 0; yPos < 9; yPos++)
            {
                for (int xPos = 0; xPos < 9; xPos++)
                {
                    tryBoard[yPos, xPos] = board[yPos, xPos];
                }
            }

            // Hämtar lista av möjliga rätta svar för varje rad och kolumn
            // möjliga nummer returneras som Stack<int> (List<List<Stack<int>>>)
            var possibleNumbers = GetNewListOfPossibleNumbers(tryBoard);

            // Hämtar koordinaterna från en av de rutor med lägst alternativ
            var coordinatesOfLowest = FindLowestValue(possibleNumbers);

            // För att göra det mer lättläst så läggs koordinaterna i nya variabler
            int x = coordinatesOfLowest[1];
            int y = coordinatesOfLowest[0];

            // Om alla nummer används och fortfarande inte löst
            if (possibleNumbers[y][x].Count != 0)
            {
                // Här gissas det översta numret i Stack-listan
                tryBoard[y, x] = possibleNumbers[y][x].Peek().ToString()[0];
                // Spara index och gissad siffra
                Guesses.Push(new Guess(y, x, possibleNumbers[y][x].Pop()));
            }
            else
            {
                return false;
            }

            // Prova lösa sudokut, returnera true eller false
            bool isSolved = TrySolve(tryBoard);

            // Är sudokut löst så returnas true i annat fall anropas Guess rekursivt
            if (!isSolved)
            {
                bool isGuessRight = Guesser(tryBoard);

                if (!isGuessRight)
                {
                    tryBoard[y, x] = '0';
                    Guesser(tryBoard);
                }
                else
                {
                    return true;
                }
            }

            return false;

        }



        //public int[] GetWorkingNumbers(char[,] board, List<List<Stack<int>>> possibleNumbers)
        //{
        //    for (int y = 0; y < 9; y++)
        //    {
        //        for (int x = 0; x < 9; x++)
        //        {
        //            try
        //        }
        //    }
        //    }







            private List<List<Stack<int>>> GetNewListOfPossibleNumbers(char[,] board)
            {
                List<List<Stack<int>>> possibleNumbersOfBoard = new List<List<Stack<int>>>();
                for (int y = 0; y < 9; y++)
                {
                    possibleNumbersOfBoard.Add(new List<Stack<int>>());
                    for (int x = 0; x < 9; x++)
                    {
                        possibleNumbersOfBoard[y].Add(GetPossibleNumbers(board, x, y));
                    }
                }


                return possibleNumbersOfBoard;
            }

        private enum Zones
        {
            Horizontal,
            Vertical,
            Box
        }

    }

}
