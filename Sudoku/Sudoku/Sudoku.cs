using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Sudoku
{
    class Sudoku
    {
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
