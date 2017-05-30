using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency
{
    public class Passenger:IEquatable<Passenger>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool Equals(Passenger other)
        {
            if (FirstName == other.FirstName &&
                LastName == other.LastName)
                return true;
            else
                return false;
        }
    }
}
