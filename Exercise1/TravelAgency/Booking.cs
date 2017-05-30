using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency
{
    public class Booking
    {
        public Passenger Passenger { get; set; }

        public Tour Tour { get; set; }

        public Booking(Passenger passenger, Tour tour)
        {
            Passenger = passenger;
            Tour = tour;
        }
    }
}
