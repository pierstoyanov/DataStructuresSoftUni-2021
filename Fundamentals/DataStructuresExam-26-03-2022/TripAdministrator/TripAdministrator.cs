using System;
using System.Collections.Generic;
using System.Linq;

namespace TripAdministrations
{
    public class TripAdministrator : ITripAdministrator
    {
        private Dictionary<string, Company> companies = new Dictionary<string, Company>();
        private Dictionary<string, Trip> trips = new Dictionary<string, Trip>();
        private Dictionary<string, List<string>> companyTrips = new Dictionary<string, List<string>>();


        public void AddCompany(Company c)
        {
            if (Exist(c))
            {
                throw new ArgumentException();
            }

            companies.Add(c.Name, c);
        }

        public void AddTrip(Company c, Trip t)
        {
            if (!Exist(c))
            {
                throw new ArgumentException();
            }

            var comp = companies[c.Name];
            

            if (comp.CurrentTrips < comp.TripOrganizationLimit)
            {
                comp.CurrentTrips++;

                trips.Add(t.Id, t);
                
                if (!companyTrips.ContainsKey(c.Name))
                {
                    companyTrips[c.Name] = new List<string>();
                }
                companyTrips[c.Name].Add(t.Id);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public bool Exist(Company c)
        {
            if (companies.ContainsKey(c.Name))
            {
                return true;
            }
            return false;
        }

        public bool Exist(Trip t)
        {
            if (trips.ContainsKey(t.Id))
            {
                return true;
            }
            return false;
        }

        public void RemoveCompany(Company c)
        {
            if (!Exist(c))
            {
                throw new ArgumentException();
            }

            if (companyTrips.ContainsKey(c.Name))
            {
                foreach (var t in companyTrips[c.Name])
                {
                    trips.Remove(t);
                }

                companyTrips.Remove(c.Name);
            }

            companies.Remove(c.Name);
        }

        public IEnumerable<Company> GetCompanies()
        {
            if (companies.Count == 0)
            {
                return new List<Company>();
            }

            return companies.Values.ToList();
        }

        public IEnumerable<Trip> GetTrips()
        {
            if (trips.Count == 0)
            {
                return new List<Trip>();
            }

            return trips.Values.ToList();
        }

        public void ExecuteTrip(Company c, Trip t)
        {
            if (!Exist(c) || !Exist(t))
            {
                throw new ArgumentException();
            }

            if (!companyTrips[c.Name].Contains(t.Id))
            { 
                throw new ArgumentException();
            }

            companies[c.Name].CurrentTrips--;
            companyTrips[c.Name].Remove(t.Id);
            trips.Remove(t.Id);
        }

        public IEnumerable<Company> GetCompaniesWithMoreThatNTrips(int n)
        {


            var result = companies
                .Where(kvp => kvp.Value.CurrentTrips > n)
                .Select(x => x.Value)
                .ToList();
            /*
            var result = new List<Company>();
            
            foreach (var c in companyTrips.Where(kvp => kvp.Value.Count > n))
            {
                result.Add(companies[c.Key]);
            }*/

            return result;
        }

        public IEnumerable<Trip> GetTripsWithTransportationType(Transportation t)
        {
            var result = trips
                .Where(kvp => kvp.Value.Transportation == t)
                .Select(x => x.Value)
                .ToList();

            return result;
        }

        public IEnumerable<Trip> GetAllTripsInPriceRange(int lo, int hi)
        {
            var result = trips
                .Where(kvp => kvp.Value.Price >= hi && kvp.Value.Price <= hi)
                .Select(x => x.Value)
                .ToList();

            return result;
        }
    }
}
