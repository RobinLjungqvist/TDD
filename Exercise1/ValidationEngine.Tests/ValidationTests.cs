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
        [Test]
        public void TrueForValidAddress()
        {
            var sut = new Validator();

            var result = sut.ValidateEmailAdress("nisse@email.com");
            var result2 = sut.ValidateEmailAdress("snickaren@jobb.se");


            Assert.IsTrue(result);
            Assert.IsTrue(result2);

        }
        [Test]
        public void FalseForInvalidAddress()
        {
            var sut = new Validator();

            Assert.Throws<InvalidEmailException>(() =>
            {
                var result = sut.ValidateEmailAdress("Det här är ingen email.");
                var result2 = sut.ValidateEmailAdress("felemail.com");
                var result3 = sut.ValidateEmailAdress("fel@email.coms");


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
