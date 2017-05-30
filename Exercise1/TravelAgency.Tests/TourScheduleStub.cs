using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Tests
{
    public class TourScheduleStub: ITourSchedule
    {
        public List<Tour> Tours { get; set; }
         

        public void CreateTour(string name, DateTime date, int nrOfSeats)
        {
            Tours.Add(new Tour(name, date, nrOfSeats));
        }

        public List<Tour> GetToursFor(DateTime date)
        {
            return Tours.Where(t => t.Date == date).ToList();
        }
    }
}
