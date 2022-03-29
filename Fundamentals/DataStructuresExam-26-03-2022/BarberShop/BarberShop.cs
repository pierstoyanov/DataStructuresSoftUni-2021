using System;
using System.Collections.Generic;
using System.Linq;

namespace BarberShop
{
    public class BarberShop : IBarberShop
    {
        //base objects
        private readonly Dictionary<string, Barber> barbers = new Dictionary<string, Barber>();
        private readonly Dictionary<string, Client> clients = new Dictionary<string, Client>();
        //references
        private readonly Dictionary<string, List<string>> barberClients = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, string> clientBarber = new Dictionary<string, string>();
        private readonly List<string> clientsWtihoutBarber = new List<string>();

        public void AddBarber(Barber b)
        {
            if (Exist(b))
            {
                throw new ArgumentException();
            }

            barbers.Add(b.Name, b);
            barberClients.Add(b.Name, new List<string>());
        }

        public void AddClient(Client c)
        {
            if (Exist(c))
            {
                throw new ArgumentException();
            }

            clients.Add(c.Name, c);

            if (c.Barber == null)
            {
                clientsWtihoutBarber.Add(c.Name);
            }
            else
            {
                barberClients[c.Barber.Name].Add(c.Name);
                clientBarber.Add(c.Name, c.Barber.Name);
            }
        }

        public bool Exist(Barber b)
        {
            return barbers.ContainsKey(b.Name);
        }

        public bool Exist(Client c)
        {
            return clients.ContainsKey(c.Name);
        }

        public IEnumerable<Barber> GetBarbers()
        {
            if (barbers.Count == 0)
            {
                return new Barber[0];
            }

            return barbers.Values.ToList();
        }

        public IEnumerable<Client> GetClients()
        {
            if (clients.Count == 0)
            {
                return new List<Client>();
            }

            return clients.Values.ToList();
        }

        public void AssignClient(Barber b, Client c)
        {

            if (!Exist(b) || !Exist(c))
            {
                throw new ArgumentException();
            }

            barberClients[b.Name].Add(c.Name);

            clientBarber.Add(c.Name, b.Name);

            clientsWtihoutBarber.Remove(c.Name);
        }

        public void DeleteAllClientsFrom(Barber b)
        {

            if (!Exist(b))
            {
                throw new ArgumentException();
            }

            //???deleting clients from barber removes them
            foreach (var c in barberClients[b.Name])
            {
                clientBarber.Remove(c);
                ///do clients stay?
                //clientsWtihoutBarber.Add(c);
            }
            barberClients[b.Name] = new List<string>();
        }

        public IEnumerable<Client> GetClientsWithNoBarber()
        {
            var result = new List<Client>();
 
            if (clientsWtihoutBarber.Count != 0)
            {
                foreach (var c in clientsWtihoutBarber)
                {
                    result.Add(clients[c]);
                }

            }

            return result;
        }

        public IEnumerable<Barber> GetAllBarbersSortedWithClientsCountDesc()
        {             
            var result = new List<Barber>();

            foreach (var kvp in barberClients.Where(b => b.Value.Count > 0).OrderByDescending(b => b.Value.Count))
            {
                result.Add(barbers[kvp.Key]);
            }

            return result;
        }

        public IEnumerable<Barber> GetAllBarbersSortedWithStarsDecsendingAndHaircutPriceAsc()
        {
            var barberNames = barbers.Keys.ToList()
                .OrderByDescending(name => barbers[name].Stars)
                .ThenBy(name => barbers[name].HaircutPrice);

            var result = new List<Barber>();

            foreach (var b in barberNames)
            {
                result.Add(barbers[b]);
            }

            return result;
        }

        public IEnumerable<Client> GetClientsSortedByAgeDescAndBarbersStarsDesc()
        {
            var filter = clientBarber.ToList()
                .OrderByDescending(kvp => clients[kvp.Key].Age)
                .ThenBy(kvp => barbers[kvp.Value].Stars);

            var result = new List<Client>();

            foreach (var c in filter)
            {
                result.Add(clients[c.Key]);
            }

            return result;
        }
    }
}
