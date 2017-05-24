using System;

namespace TravelAgency
{
    public class Tour
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int NrOfSeats { get; set; }

        public Tour(string name, DateTime date, int nrOfSeats)
        {
            Name = name;
            Date = date;
            NrOfSeats = nrOfSeats;
        }
    }
}