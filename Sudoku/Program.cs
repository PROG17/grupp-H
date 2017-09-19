using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
           //Sudoku sudoku = new Sudoku("003020600" +
           //                                         "900305001" +
           //                                         "001806400" +
           //                                         "008102900" +
           //                                         "700000008" +
           //                                         "006708200" +
           //                                         "002609500" +
           //                                         "800203009" +
           //                                         "005010300");
            //sudoku.SolveAndShow();

            //SudokuCreator create = new SudokuCreator();

            //Sudoku game = new Sudoku(create.Sudoku);

            //Console.WriteLine(game.BoardAsText());

            //Console.WriteLine(sudoku.BoardAsText());


            Sudoku gameMedium = new Sudoku("830060050" +
                                           "905000040" +
                                           "401020300" +
                                           "100002070" +
                                           "050040020" +
                                           "090800005" +
                                           "009050701" +
                                           "020000604" +
                                           "010090082");



            Console.WriteLine(gameMedium.BoardAsText(gameMedium.Board));
            
            gameMedium.Solve();

            Console.WriteLine(gameMedium.BoardAsText(Sudoku.TryBoard));

            Console.ReadLine();
        }
    }
}
