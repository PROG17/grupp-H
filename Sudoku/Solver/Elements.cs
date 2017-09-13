﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solver
{
    class Elements
    {
        public static void StringToInt()
        {
            //81 elements
            string sudokuNumbers = "619030040270061008000047621486302079000014580031009060005720806320106057160400030";
            int[,] elements = new int[9, 9];
            int startIndex = 0;


            //sparar string värde till 2D int array
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    //converterar string till int
                    int convert = int.Parse(sudokuNumbers.Substring(startIndex, 1));
                    elements[i, j] = convert;
                    startIndex++;
                }
            }

            Console.Write(" - - - - - - - - - - - - - -\n");
            for (int r = 1; r <= 9; r++)
            {
                for (int c = 1; c <= 9; c++)
                {
                    Console.Write($" {elements[r - 1, c - 1]} ");

                    if (c == 9)
                    {

                    }
                    else if (c % 3 == 0)
                    {
                        Console.Write("|");
                    }

                }

                if (r % 3 == 0)
                {
                    Console.WriteLine();
                    Console.Write(" - - - - - - - - - - - - - -");
                }
                Console.WriteLine();
            }
        }
    }
}
