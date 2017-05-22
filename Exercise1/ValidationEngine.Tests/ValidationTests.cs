using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationEngine.Exceptions;

namespace ValidationEngine.Tests
{
    [TestFixture]
    public class ValidationTests
    {
        private static string[] errorEmails()
        {
            return new string[] {
            "",
            "Test.com",
            "name@test",
            "name.text@com",
            "Name2015@test.com",
            "name@test2015.com"

            };
        }

        [Test]
        public void TrueForValidAddress()
        {
            var sut = new Validator();

            var result = sut.ValidateEmailAdress("mike@edument.com");
            var result2 = sut.ValidateEmailAdress("joe@apple.se");


            Assert.IsTrue(result);
            Assert.IsTrue(result2);

        }
        [Test, Sequential]
        public void FalseForInvalidAddress([ValueSource("errorEmails")] string invalidEmail)
        {
            var sut = new Validator();

            Assert.Throws<InvalidEmailException>(() =>
            {
                var result = sut.ValidateEmailAdress(invalidEmail);

            });
        }
        [Test]
        public void FalseForInvalidMailAdressClassCheck()
        {
            var sut = new Validator();

            Assert.Throws<InvalidEmailException>(() =>
            {
                var result = sut.ValidateWithMailAdressClass("Det här är ingen email.");
                var result2 = sut.ValidateWithMailAdressClass("felemail.com");


            });
        }
    }
}
