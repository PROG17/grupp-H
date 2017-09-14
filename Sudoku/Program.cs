using Sudoku.Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
           Sudoku.Sudoku sudoku = new Sudoku.Sudoku("003020600" +
                                                    "900305001" +
                                                    "001806400" +
                                                    "008102900" +
                                                    "700000008" +
                                                    "006708200" +
                                                    "002609500" +
                                                    "800203009" +
                                                    "005010300");
            sudoku.Solve();

            Console.WriteLine(sudoku.BoardAsText());
            Console.ReadLine();
        }
    }
}
