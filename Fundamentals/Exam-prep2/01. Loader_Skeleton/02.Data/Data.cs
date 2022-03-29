namespace _02.Data
{
    using _02.Data.Interfaces;
    using System;
    using System.Collections.Generic;

    public class Data : IRepository
    {
        public int Size => throw new NotImplementedException();

        public void Add(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public IRepository Copy()
        {
            throw new NotImplementedException();
        }

        public IEntity DequeueMostRecent()
        {
            throw new NotImplementedException();
        }

        public List<IEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<IEntity> GetAllByType(string type)
        {
            throw new NotImplementedException();
        }

        public IEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<IEntity> GetByParentId(int parentId)
        {
            throw new NotImplementedException();
        }

        public IEntity PeekMostRecent()
        {
            throw new NotImplementedException();
        }
    }
}
