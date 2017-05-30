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
            Assert.That(tourSchedule.ToursCalled.Count == 1);
            Assert.That(tourSchedule.ToursCalled[0] == new DateTime(2017, 12, 24));




        }
        [Test]
        public void TourDoesntExistException_Thrown_When_Booking_A_Non_Existant_Tour()
        {
            tourSchedule.Tours = new List<Tour>();
            var passanger = new Passenger() { FirstName = "Nisse", LastName = "Nilsson" };

            Assert.Throws<TourDoesntExistException>(() =>
            {
                sut.CreateBooking("Safari", new DateTime(2017, 12, 24), passanger);
            });
            Assert.That(tourSchedule.ToursCalled.Count == 1);
            Assert.That(tourSchedule.ToursCalled[0] == new DateTime(2017, 12, 24));


        }
        [Test]
        public void SeatAllocationException_When_Booking_With_No_Available_Seats()
        {
            var testTour = new Tour("Safari", new DateTime(2017, 12, 24), 1);
            tourSchedule.Tours = new List<Tour>() { testTour };
            var passanger = new Passenger() { FirstName = "Nisse", LastName = "Nilsson" };

            sut.CreateBooking("Safari", new DateTime(2017, 12, 24), passanger);

            var passanger2 = new Passenger() { FirstName = "Olof", LastName = "Nilsson" };

            Assert.Throws<SeatAllocationException>(() => {
                sut.CreateBooking("Safari", new DateTime(2017, 12, 24), passanger);

            });
            Assert.That(tourSchedule.ToursCalled.Count == 2);
            Assert.That(tourSchedule.ToursCalled[0] == new DateTime(2017, 12, 24));

        }
        [Test]

        public void Can_Cancel_Tour_Booking()
        {
            var testTour = new Tour("Safari", new DateTime(2017, 12, 24), 5);
            tourSchedule.Tours = new List<Tour>() { testTour };
            var passanger = new Passenger() { FirstName = "Nisse", LastName = "Nilsson" };

            sut.CreateBooking("Safari", new DateTime(2017, 12, 24), passanger);

            sut.CancelBooking("Safari", new DateTime(2017, 12, 24), passanger);

            List<Booking> bookings = sut.GetBookingsFor(passanger);

            Assert.That(bookings.Count == 0);
        }

    
    }
}
