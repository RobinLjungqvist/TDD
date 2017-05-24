using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelAgency
{
    public class TourSchedule
    {
        public Dictionary<DateTime, List<Tour>> Tours { get; set; }
        public TourSchedule()
        {
            Tours = new Dictionary<DateTime, List<Tour>>();
        }

        public void CreateTour(string name, DateTime date, int nrOfSeats)
        {
            if(nrOfSeats <= 0)
            {
                throw new TourAllocationException("Number of seats can not be negative!");
            }

            var dateWithoutTime = new DateTime(date.Year, date.Month, date.Day);
            var tour = new Tour(name, dateWithoutTime , nrOfSeats);

            if (Tours.Any(t => t.Key == dateWithoutTime))
            {
                if (IsValidTourBooking(tour))
                    Tours[dateWithoutTime].Add(tour);

            }
            else
            {
                Tours.Add(dateWithoutTime, new List<Tour>() { tour });
            }
        }

        private bool IsValidTourBooking(Tour tour)
        {
            if(Tours[tour.Date].Count >= 3)
            {
                var suggestedDate = GetDateSuggestion(tour);
                throw new TourAllocationException("Tour capacity met", suggestedDate);
                
            }
            if(Tours[tour.Date].Where(t => t.Name == tour.Name).Any())
            {
                throw new TourAlreadyExistsException();
            }
            return true;
        }

        private DateTime GetDateSuggestion(Tour tour)
        {
            DateTime dateSuggestion = new DateTime();
            bool suggestionFound = false;
            while(suggestionFound == false)
            {
                var newDate = tour.Date.AddDays(1);
                if (!Tours.Keys.Any(t => t == newDate))
                {
                    dateSuggestion = newDate;
                    suggestionFound = true;
                }
            }
            return dateSuggestion;
        }

        public List<Tour> GetToursFor(DateTime date)
        {
            var resultDate = new DateTime(date.Year, date.Month, date.Day);
            if (Tours.Keys.Any(t => t == resultDate))
                return Tours[date];
            else
            {
                return null;
            }
        }
    }
}