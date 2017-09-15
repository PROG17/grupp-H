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
        // Auto-Property för själva brädan
        private string[] Board { get; set; }

        // Konstruktor - delar upp in-strängen i 9 delar och sparar som array ("Board")
        public Sudoku(string boardString)
        {
            List<string> boardList = new List<string>();
            for (int i = 0; i < boardString.Length; i += 9)
            {
                boardList.Add(boardString.Substring(i, 9));
            }
            Board = boardList.ToArray();
        }

        // Skickar en sträng som används för att skriva ut brädan i aktuellt skick
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

        // Metod som anropas i Main för att lösa sudokut. Skriver ut "Går ej at lösa" om inte alla nollar kunde ändras
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

        // Metod som kan anropas i Main som löser sudokut (om möjligt) och skriver ut en varje steg i en "animation" 
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

        // Kollar varje tom plats in sudokut och provar vilka siffro 1-9 som
        // skulle fungera där. Om det bara finns et möjligt alternativ så
        // returneras den siffran. Annars returneras "0".
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

        // Metod för att kolla om den zon ("zone") som skickats in innehåller den 
        // aktuella siffran ("numberToLookFor") som skickats in. Returnerar ett bool-värde
        // Metoden används i GetRightNumber för att kolla möjliga korrekta siffror.
        private bool ContainsNumber(int x, int y, Zones zone, int numberToLookFor)
        {
            var numbers = GetListOfNumbers(x, y, zone);
            bool isNumberPresent = numbers.Contains(numberToLookFor.ToString());
            return isNumberPresent;
        }

        // Metod som returnerar en sträng med alla siffror i aktuell zon ("Zone").
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
            // Hämtar de samlade Y-värdena för en box-rad
            // beroende på vilken box-rad "y" är i
            List<int> yList = new List<int>();
            if (y < 3)
                yList.AddRange(new int[] { 0, 1, 2 });
            else if (y < 6)
                yList.AddRange(new int[] { 3, 4, 5 });
            else if (y < 9)
                yList.AddRange(new int[] { 6, 7, 8 });

            // Hämtar och returnerar en sträng med boxens alla
            // värden beroende på vilken box-kolumn och box-rad
            // "x" är i
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

        // Enums för att ange zoner som ska kollas av. 
        // Används i GetRightNumber och dess anropade metoder
        private enum Zones
        {
            Horizontal,
            Vertical,
            Box
        }

    }

}
