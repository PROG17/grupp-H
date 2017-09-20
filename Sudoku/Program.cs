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

            const string medel = "037060000205000800006908000" +
                                   "000600024001503600650009000" +
                                   "000302700009000402000050360";

            const string zen = "000000000000000000000000000" +
                               "000000000000010000000000000" +
                               "000000000000000000000000000";

            const string empty = "000000000000000000000000000" +
                               "000000000000000000000000000" +
                               "000000000000000000000000000";


            const string supersv = "000900000000000000000000000" +
                                   "000000000000001000000000000" +
                                   "000000000000000000000000400";

            Sudoku game = new Sudoku(medel);

            Console.WriteLine(game.BoardAsText());

            //gameMedium.Solve(true);
            game.Solve();

            Console.WriteLine(game.BoardAsText());

            Console.ReadLine();
        }
    }
}
