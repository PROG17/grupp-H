using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Sudoku
{
//    // Hjälpmetod till GetListOfNumbers som returnerar en sträng av alla nummer i den aktuella boxen
//    private string GetBox(int x, int y)
//    {
//    List<int> yList = new List<int>();
//        if (y < 3)
//        yList.AddRange(new[] { 0, 1, 2 });
//    else if (y < 6)
//        yList.AddRange(new[] { 3, 4, 5 });
//    else if (y < 9)
//        yList.AddRange(new[] { 6, 7, 8 });

//    if (x < 3)
//    {
//        return GetBoxValues(0, yList);
//    }
//    else if (x < 6)
//    {
//        return GetBoxValues(3, yList);
//    }
//    else
//    {
//        return GetBoxValues(6, yList);
//    }
//    }

//    // Hjälpmetod till GetBox som hämtar en sträng baserad på aktuellt x och y värde
//    private string GetBoxValues(int x, List<int> yList)
//    {
//    StringBuilder sb = new StringBuilder();

//        for (int i = 0; i < 3; i++)
//    {
//        for (int j = 0; j < 3; j++)
//        {
//            sb.Append(Board[yList[i]][j + x]);
//        }
//    }

//    return sb.ToString();
//}


    class Sudoku
    {
        // En array som håller sudokut
        private string[] Board { get; set; }

        public Sudoku(string boardString)
        {
            List<string> boardList = new List<string>();
            for (int i = 0; i < boardString.Length; i+=9)
            {
                boardList.Add(boardString.Substring(i, 9));
            }
            Board = boardList.ToArray();
        }

        public string boardToText()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Board.Length; i++)
            {
                for (int j = 0; j < Board[i].Length; j++)
                {
                    sb.Append(Board[i][j]);

                }
                sb.AppendLine();
            }
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
                        if (Board[y][x]=='0')
                        {
                            var charList = Board[y].ToCharArray();
                            charList[x] = Convert.ToChar(GetRightNumber(x, y, ref isChanged));
                            Board[y] = string.Join("", charList);
                        }
                }
                    if (!isChanged)
                    {
                        Console.WriteLine("Sudokun gick inte att lösa");
                        break; 
                    }
                    isChanged = false;
                }
        }

        private string GetRightNumber(int x, int y, ref bool isChanged)
        {
            
        }
        private bool ContainsNumber(int x, int y, Zones zone, int numberToLookFor )
        {
            
        }

        private enum Zones
        {
            Horizontal,
            Vertical,
            Box
        }

    }

}
