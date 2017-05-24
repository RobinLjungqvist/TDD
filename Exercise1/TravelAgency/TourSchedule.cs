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
            var onlyDate = new DateTime(date.Year, date.Month, date.Day);

            if (nrOfSeats <= 0)
            {
                throw new TourAllocationException("Number of seats can not be negative!");
            }

            var tour = new Tour(name, onlyDate , nrOfSeats);

            if (Tours.Any(t => t.Key == onlyDate))
            {
                if (IsValidTourBooking(tour))
                    Tours[onlyDate].Add(tour);

            }
            else
            {
                Tours.Add(onlyDate, new List<Tour>() { tour });
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