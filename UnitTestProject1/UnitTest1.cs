using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SolvesNakedSingles()
        {
            //arrange
            string sudoku = "305420810487901506029056374850793041613208957074065280241309065508670192096512408";
            string solution = "365427819487931526129856374852793641613248957974165283241389765538674192796512438";


            //act
            Sudoku.Sudoku game = new Sudoku.Sudoku(sudoku);
            game.Solve();

            //assert
            Assert.AreEqual(game.GetStringRepOfBoard(), solution);
        }

        [TestMethod]
        public void SolvesHiddenSingles()
        {
            //arrange
            string sudoku = "002030008000008000031020000060050270010000050204060031000080605000000013005310400";
            string solution = "672435198549178362831629547368951274917243856254867931193784625486592713725316489";

            //act
            Sudoku.Sudoku game = new Sudoku.Sudoku(sudoku);
            game.Solve();

            //assert
            Assert.AreEqual(game.GetStringRepOfBoard(), solution);
        }

        [TestMethod]
        public void Fails()
        {
            //arrange
            string sudoku = "090300001000080046000000800405060030003275600060010904001000000580020000200007060";

            //act
            Sudoku.Sudoku game = new Sudoku.Sudoku(sudoku);
            game.Solve();

            //assert
            Assert.IsTrue(game.GetStringRepOfBoard().Contains("0"));
        }
    }
}
