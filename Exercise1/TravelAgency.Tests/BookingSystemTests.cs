using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency;

namespace TravelAgency.Tests
{
    [TestFixture]
    public class BookingSystemTests
    {
        private TourScheduleStub tourSchedule;
        private BookingSystem sut;
        [SetUp]
        public void Setup()
        {
            tourSchedule = new TourScheduleStub();
            sut = new BookingSystem(tourSchedule);
        }
        [Test]
        public void CanCreateBooking()
        {
            var testTour = new Tour("Safari", new DateTime(2017, 12, 24), 5);
            tourSchedule.Tours = new List<Tour>() { testTour };
            var passanger = new Passenger() { FirstName = "Nisse", LastName = "Nilsson" };

            sut.CreateBooking("Safari", new DateTime(2017, 12, 24), passanger);

            List<Booking> bookings = sut.GetBookingsFor(passanger);

            Assert.That(bookings.Count == 1);
            Assert.That(bookings.First().Tour.Equals(testTour));
            Assert.That(bookings.First().Passenger.Equals(passanger));


        }
    }
}
