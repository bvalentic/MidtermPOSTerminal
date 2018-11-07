using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MidtermPOSTerminal;

namespace UnitTestProject1
{
    [TestClass]
    public class GoodsTests
    {
        [TestMethod]
        public void GoodsTest_CheckName_True()
        {
            Goods test = new Goods("test", 5.00, "test category", "a simple test to prove this actually works");

            var testName = test.Name;

            Assert.AreEqual("test", testName);
        }

        [TestMethod]
        public void GoodsListTest_SortList_True()
        {
            List<Goods> goodsListTest = new List<Goods> { };
            goodsListTest.Add(new Goods("Green", 12, "Color", "My favorite color"));
            goodsListTest.Add(new Goods("Circle", 15, "Shape", "My favorite shape"));

            goodsListTest.Sort();

            Assert.AreEqual("Color", goodsListTest[0].Category);
        }

        [TestMethod]
        public void GoodsListTest_SortByPrice_True()
        {
            List<Goods> goodsListTest = new List<Goods> { };
            goodsListTest.Add(new Goods("Green", 12, "Color", "My favorite color"));
            goodsListTest.Add(new Goods("Circle", 15, "Shape", "My favorite shape"));
            goodsListTest.Add(new Goods("Peregrine Falcon", 10, "Animal", "My favorite animal"));

            goodsListTest = goodsListTest.OrderBy(good => good.Price).ToList<Goods>();

            Assert.AreEqual(10, goodsListTest[0].Price);
        }
    }

    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        public void Validator_CheckYes_True()
        {
            bool test = false;
            string inputString = "yes";

            if (inputString == "yes" || inputString == "y")
            {
                test = true;
            }
            else if (inputString == "no" || inputString == "n")
            {
                test = false;
            }

            Assert.AreEqual(true, test);

        }

        [TestMethod]
        public void Validator_CheckNum_True()
        {
            string inputString = "12";
            int inputNum;
            if (!(int.TryParse(inputString, out inputNum)) || inputNum < 0 || inputNum > 10)
            {
                inputNum = 11;
            }

            Assert.AreEqual(11, inputNum);
        }

        [TestMethod]
        public void Validator_CheckNumString_StringLength()
        {
            string inputString = "123456789";
            int stringLength = 9;

            if (!Regex.IsMatch(inputString, @"^\d+$") || inputString.Length != stringLength)
            {
                Assert.Fail();
            }

            Assert.AreEqual("123456789", inputString);
        }
    }
}
