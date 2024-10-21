using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CalcU;

namespace UnitTestProject1
{
    [TestClass]
    public class CalcTest
    {
        [TestMethod]
        public void Test_InputDigitOne()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("3");
            Assert.AreEqual("3", calc.Input_actual);
            Assert.AreEqual("3", calc.Input_actual);
        }
        [TestMethod]
        public void Test_InputDigitSecond()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("3");
            calc.DoWithSymbol("2");
            Assert.AreEqual("32", calc.Input_actual);
        }
        [TestMethod]
        public void Test_digitFirstAndOperation_plus()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("1");
            calc.DoWithSymbol("2");
            calc.DoWithSymbol("+");
            Assert.AreEqual("", calc.Input_actual);
            Assert.AreEqual("+", calc.Command);
            Assert.AreEqual((float)12, calc.Digit_a);
        }
        [TestMethod]
        public void Test_plusOnTwoDigit()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("1");
            calc.DoWithSymbol("2");
            calc.DoWithSymbol("+");
            calc.DoWithSymbol("3");
            calc.DoWithSymbol("4");
            Assert.AreEqual((float)12, calc.Digit_a);
        }
        [TestMethod]
        public void Test_plusOnTwoDigit_andCalculated()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("1");
            calc.DoWithSymbol("2");
            calc.DoWithSymbol("+");
            calc.DoWithSymbol("3");
            calc.DoWithSymbol("4");
            calc.DoWithSymbol("=");
            Assert.AreEqual((float)12 + (float)34, calc.Digit_a);
        }
        [TestMethod]
        public void Test_plusOnTwoDigit_andCalculatedSecond()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("1");
            calc.DoWithSymbol("2");
            calc.DoWithSymbol("+");
            calc.DoWithSymbol("3");
            calc.DoWithSymbol("4");
            calc.DoWithSymbol("=");
            calc.DoWithSymbol("=");
            Assert.AreEqual((float)12 + (float)34 + (float)34, calc.Digit_a);
        }
        [TestMethod]
        public void Test_plusOnTwoDigit_andCalculatedThird()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("1");
            calc.DoWithSymbol("2");
            calc.DoWithSymbol("+");
            calc.DoWithSymbol("3");
            calc.DoWithSymbol("4");
            calc.DoWithSymbol("=");
            calc.DoWithSymbol("=");
            calc.DoWithSymbol("=");
            Assert.AreEqual((float)12 + (float)34 + (float)34 + (float)34, calc.Digit_a);
        }
        [TestMethod]
        public void Test_plusOnTwoDigit_andPlusAgain()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("1");
            calc.DoWithSymbol("2");
            calc.DoWithSymbol("+");
            calc.DoWithSymbol("3");
            calc.DoWithSymbol("4");
            calc.DoWithSymbol("+");
            Assert.AreEqual((float)12 + (float)34, calc.Digit_a);
        }
        [TestMethod]
        public void Test_plusOnTwoDigit_andPlusNewDigit()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("1");
            calc.DoWithSymbol("2");
            calc.DoWithSymbol("+");
            calc.DoWithSymbol("3");
            calc.DoWithSymbol("4");
            calc.DoWithSymbol("+");
            calc.DoWithSymbol("5");
            calc.DoWithSymbol("6");
            Assert.AreEqual(1, calc.Status);
            Assert.AreEqual((float)12 + (float)34, calc.Digit_a);
            Assert.AreEqual("56", calc.Input_actual);
        }
        [TestMethod]
        public void Test_CalculatedTwoDigit_AndNewInput()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("1");
            calc.DoWithSymbol("2");
            calc.DoWithSymbol("+");
            calc.DoWithSymbol("3");
            calc.DoWithSymbol("4");
            calc.DoWithSymbol("=");
            calc.DoWithSymbol("7");
            calc.DoWithSymbol("8");
            Assert.AreEqual(0, calc.Status);
            Assert.AreEqual("78", calc.Input_actual);
        }
        [TestMethod]
        public void Test_CalculatedTwoDigit_AndNewInputWithMinus()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("1");
            calc.DoWithSymbol("2");
            calc.DoWithSymbol("+");
            calc.DoWithSymbol("3");
            calc.DoWithSymbol("4");
            calc.DoWithSymbol("=");
            calc.DoWithSymbol("7");
            calc.DoWithSymbol("8");
            calc.DoWithSymbol("-");
            Assert.AreEqual(1, calc.Status);
            Assert.AreEqual((float)78, calc.Digit_a);
            Assert.AreEqual("", calc.Input_actual);
        }
        [TestMethod]
        public void Test_CalculatedTwoDigit_AndNewInputWithMinusDigit()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("1");
            calc.DoWithSymbol("2");
            calc.DoWithSymbol("+");
            calc.DoWithSymbol("3");
            calc.DoWithSymbol("4");
            calc.DoWithSymbol("=");
            calc.DoWithSymbol("7");
            calc.DoWithSymbol("8");
            calc.DoWithSymbol("-");
            calc.DoWithSymbol("9");
            Assert.AreEqual(1, calc.Status);
            Assert.AreEqual((float)78, calc.Digit_a);
            Assert.AreEqual("9", calc.Input_actual);
        }
        [TestMethod]
        public void Test_CalculatedTwoReshenia()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("1");
            calc.DoWithSymbol("2");
            calc.DoWithSymbol("+");
            calc.DoWithSymbol("3");
            calc.DoWithSymbol("4");
            calc.DoWithSymbol("=");
            calc.DoWithSymbol("7");
            calc.DoWithSymbol("8");
            calc.DoWithSymbol("-");
            calc.DoWithSymbol("9");
            calc.DoWithSymbol("=");
            Assert.AreEqual(2, calc.Status);
            Assert.AreEqual((float)69, calc.Digit_a);
            Assert.AreEqual("69", calc.Input_actual);
        }
        [TestMethod]
        public void Test_DigitWithHimself()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("2");
            calc.DoWithSymbol("+");
            calc.DoWithSymbol("=");
            Assert.AreEqual(2, calc.Status);
            Assert.AreEqual((float)4, calc.Digit_a);
            Assert.AreEqual("4", calc.Input_actual);
        }
        [TestMethod]
        public void Test_DelitNaNol()
        {
            Calc calc = new Calc();

            calc.DoWithSymbol("2");
            calc.DoWithSymbol("/");
            calc.DoWithSymbol("0");
            calc.DoWithSymbol("=");
            Assert.AreEqual(0, calc.Status);
            Assert.AreEqual((float)0, calc.Digit_a);
            Assert.AreEqual("", calc.Input_actual);
        }
    }
}
