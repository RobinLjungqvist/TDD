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

            if (tour == null)
                throw new TourDoesntExistException("Tour with that name doesn't exist.");


            if (bookings.Where(b => b.Tour.Equals(tour)).Count() < tour.NrOfSeats)
            {
                Bookings.Add(new Booking(passenger, tour));
            }
            else
            {
                throw new SeatAllocationException("No free seats available for that tour.");
            }
        }

        public List<Booking> GetBookingsFor(Passenger passenger)
        {
            return Bookings.Where(b => b.Passenger.Equals(passenger)).ToList();
        }

        public void CancelBooking(string name, DateTime dateTime, Passenger passanger)
        {
            Bookings.Where(b =>
            b.Tour.Name == name &&
            b.Tour.Date == dateTime &&
            b.Passenger.Equals(passanger));

            var booking = Bookings.SingleOrDefault();

            if(booking != null)
            {
                Bookings.Remove(booking);
            }
        }
    }
}
