using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency
{
    public class BookingSystem
    {
        private ITourSchedule tourSchedule;
        private List<Booking> bookings;

        public List<Booking> Bookings 
        {
            get { return bookings; }
            set { bookings = value; }
        }

        public BookingSystem()
        {

        }
        public BookingSystem(ITourSchedule tourSchedule)
        {
            this.tourSchedule = tourSchedule;
            bookings = new List<Booking>();
        }

        public void CreateBooking(string tourName, DateTime dateTime, Passenger passenger)
        {
            var tours = tourSchedule.GetToursFor(dateTime);
            var tour = tours.Where(t => t.Name == tourName).FirstOrDefault();
            if (bookings.Where(b => b.Tour.Equals(tour)).Count() < tour.NrOfSeats)
            {
                Bookings.Add(new Booking(passenger, tour));
            }
        }

        public List<Booking> GetBookingsFor(Passenger passenger)
        {
            return Bookings.Where(b => b.Passenger.Equals(passenger)).ToList();
        }
    }
}
