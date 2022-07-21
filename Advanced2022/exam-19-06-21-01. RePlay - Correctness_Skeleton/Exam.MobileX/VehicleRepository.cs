using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.MobileX
{
    public class VehicleRepository : IVehicleRepository
    {
        private Dictionary<string, List<Vehicle>> SellerVehicles = new Dictionary<string, List<Vehicle>>();
        private Dictionary<(string id, string seller), Vehicle> IdSellerVehicle = new Dictionary<(string id, string seller), Vehicle>();


        private Dictionary<string, List<Vehicle>> BrandVehicle = new Dictionary<string, List<Vehicle>>();
        private Dictionary<string, string> IdSeller = new Dictionary<string, string>();

        public int Count => IdSellerVehicle.Count;

        public void AddVehicleForSale(Vehicle vehicle, string sellerName)
        {
            if (!SellerVehicles.ContainsKey(sellerName))
            {
                SellerVehicles.Add(sellerName, new List<Vehicle>() { vehicle });
            }
            else
            {
                SellerVehicles[sellerName].Add(vehicle);
            }

            IdSellerVehicle.Add((vehicle.Id, sellerName), vehicle);

            if (!BrandVehicle.ContainsKey(vehicle.Brand))
            {
                BrandVehicle[vehicle.Brand] = new List<Vehicle> { vehicle };
            }
            else
            {
                BrandVehicle[vehicle.Brand].Add(vehicle);
            }

            IdSeller.Add(vehicle.Id, sellerName);
        }

        private void CheckVehicleIdExists(string id)
        {
            if (!IdSellerVehicle.Keys.Select(k => k.id).Contains(id))
            {
                throw new ArgumentException();
            }
        }

        public Vehicle BuyCheapestFromSeller(string sellerName)
        {
            //not fast
            CheckSellerExists(sellerName);

            var temp = SellerVehicles[sellerName]
                .OrderBy(x => x.Price)
                .First();

            if (temp == null)
            {
                throw new ArgumentException();
            }

            IdSellerVehicle.Remove((temp.Id, sellerName));
            SellerVehicles[sellerName].Remove(temp);
            return temp;
        }

        public bool Contains(Vehicle vehicle)
        {
            return IdSellerVehicle.Keys.Select(k => k.id).Contains(vehicle.Id);
        }

        public Dictionary<string, List<Vehicle>> GetAllVehiclesGroupedByBrand()
        {
            var result = new Dictionary<string, List<Vehicle>>();

            foreach (var kvp in BrandVehicle)
            {
                result.Add(kvp.Key, kvp.Value.OrderBy(x => x.Price).ToList());
            }

            return result;
        }

        public IEnumerable<Vehicle> GetAllVehiclesOrderedByHorsepowerDescendingThenByPriceThenBySellerName()
        {
            var vehiclesToRetrun = IdSellerVehicle.Values
                .OrderByDescending(x => x.Horsepower)
                .ThenBy(x => x.Price)
                .ThenBy(x => IdSeller[x.Id])
                .ToList();

            if (vehiclesToRetrun.Count == 0)
            {
                return new List<Vehicle>();
            }

            return vehiclesToRetrun;
        }

        public IEnumerable<Vehicle> GetVehicles(List<string> keywords)
        {
            var vehiclesToRetrun = IdSellerVehicle.Values
                .Where(x => new List<string> { x.Brand, x.Model, x.Color, x.Location }.Intersect(keywords).ToList().Count != 0)
                .OrderBy(x => x.IsVIP)
                .ThenBy(x => x.Price)
                .ToList();

            if (vehiclesToRetrun.Count == 0)
            {
                return new List<Vehicle>();
            }

            return vehiclesToRetrun;
        }

        public IEnumerable<Vehicle> GetVehiclesBySeller(string sellerName)
        {
            CheckSellerExists(sellerName);
            
            return SellerVehicles[sellerName];
        }

        public IEnumerable<Vehicle> GetVehiclesInPriceRange(double lowerBound, double upperBound)
        {
            var result = new List<Vehicle>();

            var vehiclesToReturn = IdSellerVehicle.Values
                .Where(x => x.Price >= lowerBound && x.Price <= upperBound)
                .OrderByDescending(x => x.Horsepower)
                .ToList();

            if (vehiclesToReturn.Count == 0)
            {
                return result;
            }

            return vehiclesToReturn;
        }

        public void RemoveVehicle(string vehicleId)
        {
            CheckVehicleIdExists(vehicleId);

            var temp = IdSellerVehicle[(vehicleId, IdSeller[vehicleId])];

            foreach (var kvp in SellerVehicles)
            {
                kvp.Value.Remove(temp);
            }

            BrandVehicle[temp.Brand].Remove(temp);

            IdSellerVehicle.Remove((vehicleId, IdSeller[vehicleId]));

            IdSeller.Remove(vehicleId);
        }

        private void CheckSellerExists(string sellerName)
        {
            if (!SellerVehicles.ContainsKey(sellerName))
            {
                throw new ArgumentException();
            }
        }
    }
}
