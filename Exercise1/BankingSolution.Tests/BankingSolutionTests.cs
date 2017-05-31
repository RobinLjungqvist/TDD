using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extra_Exercise_3;
using NSubstitute;

namespace BankingSolution.Tests
{
    [TestFixture]
    public class BankingSolutionTests
    {
        public IAuditLogger logger { get; set; }
        public Bank sut { get; set; }
        [SetUp]
        public void Setup()
        {
            logger = Substitute.For<IAuditLogger>();
            sut = new Bank(logger);
        }
        [Test]
        public void CanCreateBankAccount()
        {
            sut.CreateAccount(new Account() { Name = "Nisse", Balance = 0, Number = "12345" });

            var account = sut.GetAccount("12345");

            Assert.That(account.Name == "Nisse");
            Assert.That(account.Balance == 0);
            Assert.That(account.Number == "12345");


        }
        [Test]
        public void CanNotCreateDuplicateAccounts()
        {
            sut.CreateAccount(new Account() { Name = "Nisse", Balance = 0, Number = "12345" });

            Assert.Throws<DuplicateAccount>(() =>
            {
                sut.CreateAccount(new Account() { Name = "Nisse", Balance = 0, Number = "12345" });

            });
        }

        [Test]
        public void WhenCreatingAnAccount_AMessageIsWrittenToTheAuditLog()
        {

            sut.CreateAccount(new Account() { Name = "Nisse", Balance = 0, Number = "12345" });

            logger.Received().AddMessage(Arg.Is<string>(x => x.Contains("Nisse")));
            logger.Received().AddMessage(Arg.Is<string>(x => x.Contains("12345")));

        }
        [Test]
        public void WhenCreatingAnValidAccount_OneMessageAreWrittenToTheAuditLog()
        {
            Assert.DoesNotThrow(() =>
            {
                sut.CreateAccount(new Account() { Name = "Nisse", Balance = 0, Number = "12345" });

            });

            logger.Received(1).AddMessage(Arg.Any<string>());

        }
        [Test]
        public void WhenCreatingAnInvalidAccount_TwoMessagesAreWrittenToTheAuditLog()
        {
            Assert.Throws<InvalidAccount>(() =>
            {
                sut.CreateAccount(new Account() { Name = "Nisse", Balance = 0, Number = "12kfg345" });
            });
            logger.Received(2).AddMessage(Arg.Any<string>());
        }
        [Test]
        public void WhenCreatingAnInvalidAccount_AWarn12AndErro45MessageIsWrittenToAuditLog()
        {
            Assert.Throws<InvalidAccount>(() =>
            {
                sut.CreateAccount(new Account() { Name = "Nisse", Balance = 0, Number = "12kfg345" });
            });

            logger.Received().AddMessage(Arg.Is<string>(x => x.Contains("Warn12")));
            logger.Received().AddMessage(Arg.Is<string>(x => x.Contains("Error45")));


        }
        [Test]
        public void VerifyThat_GetAuditLog_GetsTheLogFromTheAuditLogger()
        {
            var mockLogs = new List<string>()
            {
                "Nisse 123456",
                "Olle 808812",
                "Pelle 554344"
            };
            logger.GetLog().Returns(mockLogs);


            var logs = sut.GetAuditLog();


            Assert.That(logs.Count == 3);
            Assert.That(logs.Contains("Nisse 123456"));
            Assert.That(logs.Contains("Olle 808812"));
            Assert.That(logs.Contains("Pelle 554344"));



        }

    }
}
