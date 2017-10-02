using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Sudoku
{
    public class Sudoku
    {
        // Fält för brädan
        private char[,] _board = new char[9, 9];

        // Property för _board
        private char[,] Board
        {
            get { return _board; }
            set { _board = value; }
        }

        // Konstruktor som tar in en sträng och omvandlar till 2D-chararray
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

        // Konstruktor som tar in en 2D-chararray och skapar en "djup"-kopia
        // Används endast inom klassen i rekursion, därmed "private"
        private Sudoku(char[,] boardChar)
        {
            Board = MakeDeepCopyOfBoard(boardChar);
        }

        public Sudoku()
        {
            throw new NotImplementedException();
        }

        // Returnerar en "djup" kopia av "board"
        private static char[,] MakeDeepCopyOfBoard(char[,] board)
        {
            var tryBoard = new char[9, 9];
            for (int yPos = 0; yPos < 9; yPos++)
            {
                for (int xPos = 0; xPos < 9; xPos++)
                {
                    tryBoard[yPos, xPos] = board[yPos, xPos];
                }
            }

            return tryBoard;
        }

        // Returnerar en sträng av brädan för att se ut som ett sudoku-bräde
        public string BoardAsText()
        {
            var graphicBoard = new char[9, 9];

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    graphicBoard[y, x] = Board[y, x];
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

        // Den metod som anropas för att lösa sudokut utanför klassen.
        // Skriver ut felmeddelande om TrySolve returnerar false.
        // Tar en bool som argument beroende på om man vill se pusslet lösas
        // i en animation eller ej. Är som default false.
        public void Solve(bool isDebug = false)
        {
            if (GetStringRepOfBoard(Board)== "000000000000000000000000000" +
                "000000000000000000000000000" +
                "000000000000000000000000000")
            {
                Board[0, 1] = '1';
            }

            bool isSolved = TrySolve(isDebug, ref _board);

            if (!isSolved)
            {
                Console.WriteLine("Sudokut är inte lösbart...");
                Console.ReadLine();
            }
        }

        // TrySolve returnerar sant eller falskt beroende på om sudokut är löst eller ej.
        // Anropas rekursivt i sig själv efter att den försökt lösa sudokut logiskt och
        // provat en "gissning". "headBoard" är den ytterta brädan som skickas in med ref 
        // för att se till att brädan som lösts allra sista ledet följer med tillbaka.
        // Om "isDebug" är true så skrivs datorns pysslande ut i en animation
        private bool TrySolve(bool isDebug, ref char[,] headBoard)
        {
            if (!LogicalSolve(Board, isDebug))
            {
                bool isSuccess = false;
                var possibleNumbers = GetNewListOfPossibleNumbers(Board);
                var coordinates = FindLowestValue(possibleNumbers);
                int y = coordinates[0];
                int x = coordinates[1];

                for (int i = 0; i < possibleNumbers[y][x].Count; i++)
                {
                    Board[y, x] = possibleNumbers[y][x][i].ToString()[0];

                    if (isDebug)
                    {
                        PrintBoardProgress();
                    }

                    Sudoku tryGame = new Sudoku(Board);

                    isSuccess = tryGame.TrySolve(isDebug, ref headBoard);

                    if (isSuccess)
                    {
                        break;
                    }
                }
                return isSuccess;
            }
            headBoard = _board;
            return true;
        }

        // Metod som skriver ut brädan under pysslandets gång
        private void PrintBoardProgress()
        {
            Console.WriteLine(BoardAsText());
            Thread.Sleep(8);
            Console.Clear();
        }

        // Löser alla celler som endast har ett alternativ, returnerar
        // bool beronde på om sudokut lösts eller ej. Anropas i TrySolve.
        private bool LogicalSolve(char[,] board, bool isDebug)
        {
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
                            var c = GetSingleRightNumber(board, x, y, ref isChanged);
                            if (isChanged)
                            {
                                changeCounter++;

                            }
                            board[y, x] = c;

                            if (isDebug)
                            {
                                PrintBoardProgress();
                            }
                        }
                    }
                }
                if (changeCounter == 0)
                {
                    return false;
                }

                changeCounter = 0;
                isChanged = false;
            }
            return true;
        }

        // Returnerar en sträng av brädan. Används tillsammans med
        // String-metoden Contains() för att kolla om det finns nollor
        // kvar i brädan. Anropas i LogicalSolve
        public string GetStringRepOfBoard(char[,] board)
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

        public string GetStringRepOfBoard()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    sb.Append(Board[y, x]);
                }
            }
            return sb.ToString();
        }

        // Returnerar en siffra (som char). Om koordinaten i brädan endast har ett alternativ så är
        // det den som returneras, annars returneras 0
        private char GetSingleRightNumber(char[,] board, int x, int y, ref bool isChanged)
        {
            var possibleNumbers = GetPossibleNumbers(board, x, y);

            if (possibleNumbers.Count == 1)
            {
                isChanged = true;

                return possibleNumbers[0].ToString()[0];
            }
            return '0';
        }

        // Returnerar en lista med alla möjliga rätta nummer för den koordinat i brädan som skickats in
        private List<int> GetPossibleNumbers(char[,] board, int x, int y)
        {
            List<int> possibleNumbersTemp = new List<int>();
            for (int i = 1; i <= 9; i++)
            {
                bool isNumberInBox = ContainsNumber(board, x, y, Zones.Box, i);
                bool isNumberInHorizontal = ContainsNumber(board, x, y, Zones.Horizontal, i);
                bool isNumberInVertical = ContainsNumber(board, x, y, Zones.Vertical, i);

                if (!isNumberInBox && !isNumberInHorizontal && !isNumberInVertical)
                {
                    possibleNumbersTemp.Add(i);
                }
            }
            return possibleNumbersTemp;
        }

        // Metod som kollar om aktuell siffran ("numberToLookFor") finns i aktuella zonen (Zone)
        private bool ContainsNumber(char[,] board, int x, int y, Zones zone, int numberToLookFor)
        {
            var numbers = GetListOfNumbers(board, x, y, zone);
            bool isNumberPresent = numbers.Contains(numberToLookFor.ToString());
            return isNumberPresent;
        }

        // Returnerar en sträng med de aktuella nummer i zonen "zone" som skickas in
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

        // Returnerar en int-array av koordinater för platsen i possibleList som har lägst
        // antal möjliga nummer. Om alla är 0 returneras 0,0
        private int[] FindLowestValue(List<List<List<int>>> possibleList)
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

        // Returnerar nästad lista där yttersta är "y"-koordinat, mellersta är "x"-koordinat
        // och inre listan är antal möjliga nummer per koordinat
        private List<List<List<int>>> GetNewListOfPossibleNumbers(char[,] board)
        {
            List<List<List<int>>> possibleNumbersOfBoard = new List<List<List<int>>>();
            for (int y = 0; y < 9; y++)
            {
                possibleNumbersOfBoard.Add(new List<List<int>>());
                for (int x = 0; x < 9; x++)
                {
                    if (board[y, x] == '0')
                    {
                        possibleNumbersOfBoard[y].Add(GetPossibleNumbers(board, x, y));
                    }
                    else
                    {
                        possibleNumbersOfBoard[y].Add(new List<int>());
                    }
                }
            }

            return possibleNumbersOfBoard;
        }

        // Enum som representerar de zoner som kollas i GetPossibleNumbers
        private enum Zones
        {
            Horizontal,
            Vertical,
            Box
        }

    }

}
