using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinChangerApplication;

namespace CoinChangerApplication.Test
{
    [TestFixture]
    public class CoinChangerTests
    {
        [Test]
        public void Correct_Change_When_Using_One_CoinType()
        {
            var coinTypes = new List<decimal> { 1.0m };
            var sut = new CoinChanger(coinTypes);

            Dictionary<decimal, int> myChange = sut.MakeChange(33.0M);

            Assert.AreEqual(33, myChange[1m]);
            
        }

        [Test]
        public void Correct_Change_When_Using_Two_CoinTypes()
        {
            var coinTypes = new List<decimal> { 5.0m, 1.0M };
            var sut = new CoinChanger(coinTypes);

            Dictionary<decimal, int> myChange = sut.MakeChange(33.0M);

            Assert.AreEqual(6, myChange[5m]);
            Assert.AreEqual(3, myChange[1m]);


        }
        [Test]
        public void Correct_Change_When_Using_Fractal_CoinTypes_In_Wrong_Order()
        {
            var coinTypes = new List<decimal> { 0.5M, 5.0m, 1.0M, 0.25M };
            var sut = new CoinChanger(coinTypes);

            Dictionary<decimal, int> myChange = sut.MakeChange(33.75M);

            Assert.AreEqual(6, myChange[5m], "Number of 5's was incorrect.");
            Assert.AreEqual(3, myChange[1m], "Number of 1's was incorrect.");
            Assert.AreEqual(1, myChange[0.5m], "Number of 0.5's was incorrect.Correct.");
            Assert.AreEqual(1, myChange[0.25m], "Number of 0.25's was incorrect.");



        }
    }
}
