namespace _01.Microsystem
{
    using System;
    using System.Collections.Generic;

    public class Microsystems : IMicrosystem
    {
        public void CreateComputer(Computer computer)
        {
            throw new NotImplementedException();
        }

        public bool Contains(int number)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public Computer GetComputer(int number)
        {
            throw new NotImplementedException();
        }

        public void Remove(int number)
        {
            throw new NotImplementedException();
        }

        public void RemoveWithBrand(Brand brand)
        {
            throw new NotImplementedException();
        }

        public void UpgradeRam(int ram, int number)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Computer> GetAllFromBrand(Brand brand)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Computer> GetAllWithColor(string color)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
        {
            throw new NotImplementedException();
        }
    }
}
