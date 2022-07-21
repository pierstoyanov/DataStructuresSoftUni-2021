using System;
using System.Collections.Generic;

namespace Exam.MoovIt
{
    public class MoovIt : IMoovIt
    {
        public int Count => throw new NotImplementedException();

        public void AddRoute(Route route)
        {
            throw new NotImplementedException();
        }

        public void ChooseRoute(string routeId)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Route route)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Route> GetFavoriteRoutes(string destinationPoint)
        {
            throw new NotImplementedException();
        }

        public Route GetRoute(string routeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Route> GetTop5RoutesByPopularityThenByDistanceThenByCountOfLocationPoints()
        {
            throw new NotImplementedException();
        }

        public void RemoveRoute(string routeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Route> SearchRoutes(string startPoint, string endPoint)
        {
            throw new NotImplementedException();
        }
    }
}
