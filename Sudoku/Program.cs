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

            const string easy = "003020600900305001001806400" +
                                "008102900700000008006708200" +
                                "002609500800203009005010300";

            const string middle = "830060050" +
                                  "905000040" +
                                  "401020300" +
                                  "100002070" +
                                  "050040020" +
                                  "090800005" +
                                  "009050701" +
                                  "020000604" +
                                  "010090082";

            SudokuCreator create = new SudokuCreator();

            Sudoku game = new Sudoku(create.Sudoku);

            Console.WriteLine(game.BoardAsText());

            //Console.WriteLine(sudoku.BoardAsText());


            //Sudoku gameMedium = new Sudoku("830060050905000040401020300100002070050040020090800005009050701020000604010090082");

            Console.WriteLine(game.BoardAsText());


            //Console.WriteLine(gameMedium.BoardAsText());

            Console.ReadLine();
        }
    }
}
