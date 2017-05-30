using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculatorApp;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        public StringCalculatorApp.StringCalculator sut { get; set; }
        [SetUp]
        public void Setup()
        {
            sut = new StringCalculatorApp.StringCalculator();
        }
        [Test]
        public void Input_Empty_String_Returns_0()
        {
            var result = sut.Add("");

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Input_A_Number_Will_Return_Itself()
        {
            var result = sut.Add("1");

            Assert.AreEqual(1, result);
        }
        [Test]
        public void The_Sum_Should_Be_The_Sum_Of_The_Comma_Separated_Numbers()
        {
            var result = sut.Add("1,2,3,4,5");

            Assert.AreEqual(15, result);
        }
        [Test]
        public void Should_Return_The_Sum_Of_NewLine_Separated_Numbers()
        {
            var result = sut.Add("5\n5");

            Assert.AreEqual(10, result);
        }
        
        [Test]
        [TestCase("k\n1k2k3")]
        [TestCase("]\n1]2]3")]
        [TestCase("!\n1!2!3")]

        public void First_Char_Should_Be_Delimiter_If_Followed_By_NewLine(string input)
        {
            var result = sut.Add(input);

            Assert.AreEqual(6, result);
        }
        [Test]
        public void Should_Not_Accept_Numbers_Above_1000()
        {
            var result = sut.Add(";\n2;1000");

            Assert.AreEqual(2, result);
        }
        [Test]
        public void Should_Accept_Several_Delimiters()
        {
            var result = sut.Add(";kf\n1;2k3f4");

            Assert.AreEqual(10, result);
        }
        [Test]
        public void Long_Delimiters()
        {
            var result = sut.GetStringDelimiters("[plus][minus]\n1plus2minus3").ToList(); ;

            Assert.AreEqual("plus", result.First());
        }
        [Test]
        public void Negative_Numbers_Not_Allowed()
        {

            Assert.Throws<StringCalculatorApp.NegativeNumberNotAllowedException>(() =>
            {
                var result = sut.Add("[plus]\n1plus-2");
            });
        }
        [Test]
        public void NegativeNumberNotAllowedException_Should_Return_Negative_Numbers()
        {

            var negativeNumbers = Assert.Throws<StringCalculatorApp.NegativeNumberNotAllowedException>(() =>
            {
                var result = sut.Add("[plus]\n1plus-2");
            });

            var numbers = negativeNumbers.NegativeNumbers;

            Assert.AreEqual(-2, numbers.FirstOrDefault());
        }

    }
}
