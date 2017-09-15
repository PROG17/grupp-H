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
            // Test-spel 1
            Sudoku game = new Sudoku("003020600" +
                                     "900305001" +
                                     "001806400" +
                                     "008102900" +
                                     "700000008" +
                                     "006708200" +
                                     "002609500" +
                                     "800203009" +
                                     "005010300");

            Console.WriteLine("Bräda ett innan lösning: ");
            // Skriver ut brädan INNAN den är löst
            Console.WriteLine(game.BoardAsText());

            // Anropar Solve() som försöker lösa sudokut
            game.Solve();

            Console.WriteLine("Bräda ett efter lösning: ");
            // Skriver ut brädan med resultatet efter Solve()
            Console.WriteLine(game.BoardAsText());

            // Test-spel 2
            Sudoku game2 = new Sudoku("00000670" +
                                      "07050100" +
                                      "80006570" +
                                      "00914706" +
                                      "30006390" +
                                      "00124000" +
                                      "94036720" +
                                      "00945000" +
                                      "70050201" +
                                      "008300000");



            Console.WriteLine("Bräda två innan lösning: ");
            Console.WriteLine(game2.BoardAsText());

            game2.Solve();

            Console.WriteLine("Bräda två efter lösning: ");
            Console.WriteLine(game2.BoardAsText());


            // Sudokut nedan var för svårt för denna sudokulösare
            Sudoku gameMedium = new Sudoku("830060050905000040401020300100002070050040020090800005009050701020000604010090082");

            Console.WriteLine("Bräda tre innan lösningsförsök: ");
            // Skriver ut brädan innan försöket att lösa det
            Console.WriteLine(gameMedium.BoardAsText());

            // Skriver ut ett meddelande om att sudokut inte gick att lösa
            gameMedium.Solve();

            Console.ReadLine();
        }
    }
}
