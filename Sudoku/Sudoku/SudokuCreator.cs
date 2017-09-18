//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Sudoku
//{
//    class SudokuCreator
//    {
//        private Random rnd;

//        public string[] TestBoard { get; set; }

//        public string Sudoku { get; set; }

//        public SudokuCreator()
//        {
//            rnd = new Random();
//            List<int> list = new List<int>();
//            Stack<int> stackList = new Stack<int>();


//            // Lägger till alla siffror i ett sudoku
//            for (int i = 1; i <= 9; i++)
//            {
//                for (int j = 0; j < 9; j++)
//                {
//                    list.Add(i);
//                }
//            }

//            // Shufflar listan
//            for (int i = list.Count; i > 1; i--)
//            {
//                int pos = rnd.Next(i);
//                var x = list[i - 1];
//                list[i - 1] = list[pos];
//                list[pos] = x;
//            }

//            for (int i = 0; i < list.Count; i++)
//            {
//                stackList.Push(list[i]);
//            }

//            StringBuilder sb = new StringBuilder();

//            while (stackList.Count > 0)
//            {
//                sb.Append(stackList.Pop().ToString());
//            }

//            Sudoku = sb.ToString();
//            //Stack<int> ones = 
//            //    new Stack<int>(new []{1,1,1,1,1,1,1,1,1});
//            //Stack<int> twos = 
//            //    new Stack<int>(new []{2,2,2,2,2,2,2,2,2});
//            //Stack<int> threes = 
//            //    new Stack<int>(new []{3,3,3,3,3,3,3,3});
//            //Stack<int> fours = 
//            //    new Stack<int>(new []{4,4,4,4,4,4,4,4,4});
//            //Stack<int> fives = 
//            //    new Stack<int>(new []{5,5,5,5,5,5,5,5,5});
//            //Stack<int> sixes = 
//            //    new Stack<int>(new []{6,6,6,6,6,6,6,6,6});
//            //Stack<int> sevens = 
//            //    new Stack<int>(new []{7,7,7,7,7,7,7,7,7,7});
//            //Stack<int> eights = 
//            //    new Stack<int>();
//            //Stack<int> nines = 
//            //    new Stack<int>();



//            for (int i = 0; i < 81; i++)
//            {
                
//            }
//        }

//        private void MakeTestTestBoard()
//        {
//            List<string> TestBoardList = new List<string>();
//            for (int i = 0; i < Sudoku.Length; i += 9)
//            {
//                TestBoardList.Add(Sudoku.Substring(i, 9));
//            }
//            TestBoard = TestBoardList.ToArray();
//        }

//        private bool Validate()
//        {
            
//        }

//        private string GetWrongNumber(int x, int y, ref bool isChanged)
//        {
//            List<int> possibleNumbers = new List<int>();
//            for (int i = 1; i <= 9; i++)
//            {
//                bool isNumberInBox = ContainsNumber(x, y, Zones.Box, i);
//                bool isNumberInHorizontal = ContainsNumber(x, y, Zones.Horizontal, i);
//                bool isNumberInVertical = ContainsNumber(x, y, Zones.Vertical, i);

//                if (isNumberInBox || isNumberInHorizontal || isNumberInVertical)
//                {
//                    return "0";
//                }
//                else
//                {
//                    return 
//                }
//            }
//        }

//        private bool ContainsNumber(int x, int y, Zones zone, int numberToLookFor)
//        {
//            var numbers = GetListOfNumbers(x, y, zone);
//            bool isNumberPresent = numbers.Contains(numberToLookFor.ToString());
//            return isNumberPresent;
//        }

//        private string GetListOfNumbers(int x, int y, Zones zone)
//        {
//            StringBuilder sb = new StringBuilder();

//            if (zone == Zones.Box)
//            {
//                sb.Append(GetBox(x, y));
//            }

//            else if (zone == Zones.Horizontal)
//            {
//                sb.Append(TestBoard[y]);
//            }
//            else
//            {
//                for (int i = 0; i < TestBoard.Length; i++)
//                {
//                    sb.Append(TestBoard[i][x]);
//                }
//            }
//            return sb.ToString();
//        }

//        // Hjälpmetod till GetListOfNumbers som returnerar en sträng av alla nummer i den aktuella boxen
//        private string GetBox(int x, int y)
//        {
//            // Hämtar Y-värdena
//            List<int> yList = new List<int>();
//            if (y < 3)
//                yList.AddRange(new int[] { 0, 1, 2 });
//            else if (y < 6)
//                yList.AddRange(new int[] { 3, 4, 5 });
//            else if (y < 9)
//                yList.AddRange(new int[] { 6, 7, 8 });

//            if (x < 3)
//            {
//                return GetBoxValues(0, yList);
//            }
//            else if (x < 6)
//            {
//                return GetBoxValues(3, yList);
//            }
//            else
//            {
//                return GetBoxValues(6, yList);
//            }
//        }

//        // Hjälpmetod till GetBox som hämtar en sträng baserad på aktuellt x och y värde
//        private string GetBoxValues(int x, List<int> yList)
//        {
//            StringBuilder sb = new StringBuilder();

//            for (int i = 0; i < 3; i++)
//            {
//                for (int j = 0; j < 3; j++)
//                {
//                    sb.Append(TestBoard[yList[i]][j + x]);
//                }
//            }

//            return sb.ToString();
//        }


//        private enum Zones
//        {
//            Horizontal,
//            Vertical,
//            Box
//        }
//    }
//}
