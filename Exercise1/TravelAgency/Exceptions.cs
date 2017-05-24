using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency
{
    public class TourAllocationException : Exception
    {
        private DateTime _suggestedDate;
        public DateTime SuggestedDate {
            get { return new DateTime(_suggestedDate.Year, _suggestedDate.Month, _suggestedDate.Day); }
            set { _suggestedDate = value; }
        }
        public TourAllocationException()
        {

        }
        public TourAllocationException(string message) : base(message)
        {

        }
        public TourAllocationException(string message, DateTime suggestedDate) : base(message)
        {
            this.SuggestedDate = suggestedDate;
        }
        public TourAllocationException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
    public class TourAlreadyExistsException : Exception
    {
        public TourAlreadyExistsException()
        {

        }
        public TourAlreadyExistsException(string message) : base(message)
        {

        }
        public TourAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

}


