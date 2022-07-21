using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.MobileX
{
    public class VehicleRepository : IVehicleRepository
    {
        private Dictionary<string, Vehicle> vehiclesDict = new Dictionary<string, Vehicle>();
        private SortedDictionary<string, List<Vehicle>> sellersDict = new SortedDictionary<string, List<Vehicle>>();
        private Dictionary<string, List<Vehicle>> brandsDict = new Dictionary<string, List<Vehicle>>();
        public int Count { get; private set; }

        public void AddVehicleForSale(Vehicle vehicle, string sellerName)
        {
            if (!sellersDict.ContainsKey(sellerName))
            {
                sellersDict.Add(sellerName, new List<Vehicle> { vehicle });
            }
            else
            {
                sellersDict[sellerName].Add(vehicle);
            }
            Count++;

            if (!brandsDict.ContainsKey(vehicle.Brand))
            {
                brandsDict.Add(vehicle.Brand, new List<Vehicle> { vehicle });
            }
            else
            {
                brandsDict[vehicle.Brand].Add(vehicle);
            }

            if (!vehiclesDict.ContainsKey(vehicle.Id))
            {
                vehiclesDict.Add(vehicle.Id, vehicle);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Vehicle BuyCheapestFromSeller(string sellerName)
        {
            if (!sellersDict.ContainsKey(sellerName))
            {
                throw new ArgumentException();
            }
            if (sellersDict[sellerName].Count == 0)
            {
                throw new ArgumentException();
            }

            var vehicleToSell = sellersDict[sellerName].OrderBy(car => car.Price).First();
            this.RemoveVehicle(vehicleToSell.Id);
            return vehicleToSell;
        }

        public bool Contains(Vehicle vehicle)
        {
            if (vehiclesDict.ContainsKey(vehicle.Id))
            {
                return true;
            }
            return false;
        }

        public Dictionary<string, List<Vehicle>> GetAllVehiclesGroupedByBrand()
        {
            this.CheckEmpty();
            var result = brandsDict;
            
            foreach (var item in result)
            {
                item.Value.OrderBy(x => x.Price);
            }

            return result;
        }

        public IEnumerable<Vehicle> GetAllVehiclesOrderedByHorsepowerDescendingThenByPriceThenBySellerName()
        {
            var result = new List<Vehicle>();

            var allVehicles = sellersDict;

            foreach (var item in allVehicles)
            {
                item.Value.OrderByDescending(x => x.Horsepower)
                          .ThenBy(x => x.Price);

                foreach (var car in item.Value)
                {
                    result.Add(car);
                }
            }

            return result;
        }

        public IEnumerable<Vehicle> GetVehicles(List<string> keywords)
        {
            var result = new List<Vehicle>();

            foreach (var item in vehiclesDict.Values)
            {
                if (keywords.Contains(item.Brand)
                    || keywords.Contains(item.Model)
                    || keywords.Contains(item.Location)
                    || keywords.Contains(item.Color))
                {
                    result.Add(item);
                }
            }

            result.OrderBy(x => x.IsVIP)
                .ThenBy(x => x.Price);

            return result;
        }

        public IEnumerable<Vehicle> GetVehiclesBySeller(string sellerName)
        {
            if (!sellersDict.ContainsKey(sellerName))
            {
                throw new ArgumentException();
            }

            return sellersDict[sellerName];
        }

        public IEnumerable<Vehicle> GetVehiclesInPriceRange(double lowerBound, double upperBound)
        {
            var tracks = vehiclesDict.Values.Where(x => lowerBound <= x.Price && x.Price >= upperBound);

            tracks = tracks.OrderByDescending(x => x.Horsepower);
            return tracks;
        }

        public void RemoveVehicle(string vehicleId)
        {
            if (!vehiclesDict.ContainsKey(vehicleId))
            {
                throw new ArgumentException();
            }

            var vehicleToRemove = vehiclesDict[vehicleId];

            brandsDict[vehicleToRemove.Brand].Remove(vehicleToRemove);
            foreach (var list in sellersDict.Values)
            {
                list.Remove(vehicleToRemove);
            }
            vehiclesDict.Remove(vehicleId);

        }

        public void CheckEmpty()
        {
            if (Count == 0)
            {
                throw new ArgumentException();
            }
        }
    }
}
