using System;

namespace TravelAgency
{
    public class Tour: IEquatable<Tour>
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

        public bool Equals(Tour other)
        {
            if (this.Name == other.Name &&
                this.Date == other.Date &&
                this.NrOfSeats == other.NrOfSeats)
                return true;
            else
                return false;
        }
    }
}