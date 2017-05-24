using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Tests
{
    [TestFixture]
    public class TourScheduleTests
    {
        public TourSchedule sut { get; set; }
        [SetUp]
        public void Setup()
        {
            sut = new TourSchedule();
        }
        [TearDown]
        public void TearDown()
        {
            sut = null;
        }
        [Test]
        public void CanCreateTour()
        {

            sut.CreateTour("Morning Tour", new DateTime(2017, 06, 18), 12);

            List<Tour> toursForDate = sut.GetToursFor(new DateTime(2017, 06, 18));

            Assert.AreEqual(1, toursForDate.Count);
            Assert.AreSame("Morning Tour", toursForDate[0].Name);

        }
        [Test]
        public void ToursAreScheduledByDateOnly()
        {
            sut.CreateTour(
                "New years day safari",
                new DateTime(2013, 1, 1, 10, 15, 0), 20);

            var toursForGivenDate = sut.GetToursFor(new DateTime(2013, 1, 1));

            Assert.IsTrue(toursForGivenDate[0].Name == "New years day safari");
            Assert.AreEqual(toursForGivenDate[0].Date, new DateTime(2013, 1, 1));


        }

        [Test]
        public void GetToursForGivenDayOnly()
        {
            sut.CreateTour(
                "New years day safari",
                new DateTime(2015, 1, 1, 10, 15, 0), 20);
            sut.CreateTour(
                "Old Safari",
                new DateTime(2013, 3, 15, 20, 15, 0), 20);
            sut.CreateTour(
                "A regular safari",
                new DateTime(2016, 5, 22, 10, 15, 0), 20);

            var resultForDay1 = sut.GetToursFor(new DateTime(2015, 1, 1));
            var resultForDay2 = sut.GetToursFor(new DateTime(2013, 3, 15));
            var resultForDay3 = sut.GetToursFor(new DateTime(2016, 5, 22));


            Assert.IsTrue(resultForDay1.Count == 1);
            Assert.AreEqual( new DateTime(2015, 1, 1 ), resultForDay1.First().Date);

            Assert.IsTrue(resultForDay2.Count == 1);
            Assert.AreEqual( new DateTime(2013, 3, 15), resultForDay2.First().Date);

            Assert.IsTrue(resultForDay3.Count == 1);
            Assert.AreEqual( new DateTime(2016, 5, 22), resultForDay3.First().Date);

        }
        [Test]
        public void More_Than_Three_Tours_On_Same_Date_Throws_TourAllocationException()
        {
            sut.CreateTour("First Safari", new DateTime(2015, 1, 1), 20);
            sut.CreateTour("Second Safari", new DateTime(2015, 1, 1), 20);
            sut.CreateTour("Third Safari", new DateTime(2015, 1, 1), 20);
            Assert.Throws<TourAllocationException>(() =>
            {           
                sut.CreateTour("Fourth Safari", new DateTime(2015, 1, 1), 20);
            });

        }

        [Test]
        public void Tour_With_Same_Name_On_Same_Date_Throws_TourAlreadyExistException()
        {
            sut.CreateTour("First Safari", new DateTime(2015, 1, 1), 20);

            Assert.Throws<TourAlreadyExistsException>(() =>
            {
                sut.CreateTour("First Safari", new DateTime(2015, 1, 1), 20);

            });

        }
        [Test]
        public void Creating_A_Tour_With_Negative_Seats_Throws_TourAllocationException()
        {
            Assert.Throws<TourAllocationException>(() =>
            {
                sut.CreateTour("First Safari", new DateTime(2015, 1, 1), -5);

            });
        }
        [Test]
        public void Creating_Tour_On_Fully_Booked_Date_Throws_TourAllocationException_And_Returns_A_DateSuggestion()
        {
            sut.CreateTour("First Safari", new DateTime(2015, 1, 1), 20);
            sut.CreateTour("Second Safari", new DateTime(2015, 1, 1), 20);
            sut.CreateTour("Third Safari", new DateTime(2015, 1, 1), 20);
            var tours = sut.GetToursFor(new DateTime(2015, 1, 1));

            var exception = Assert.Throws<TourAllocationException>(() =>
            {
                sut.CreateTour("Fourth Safari", new DateTime(2015, 1, 1), 20);
            });


            Assert.AreEqual(exception.SuggestedDate, tours[0].Date.AddDays(1));
        }

        [Test]
        public void Getting_A_Booking_On_Empty_Date_Should_Not_Throw_Exception()
        {
            Assert.DoesNotThrow(() =>
            {
                sut.GetToursFor(new DateTime(2015, 1, 1));
            });
        }
    }
}
