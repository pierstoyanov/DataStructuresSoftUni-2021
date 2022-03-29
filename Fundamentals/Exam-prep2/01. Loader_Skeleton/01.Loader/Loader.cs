namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Loader : IBuffer
    {
        public int EntitiesCount => throw new NotImplementedException();

        public void Add(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEntity Extract(int id)
        {
            throw new NotImplementedException();
        }

        public IEntity Find(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<IEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void RemoveSold()
        {
            throw new NotImplementedException();
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            throw new NotImplementedException();
        }

        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            throw new NotImplementedException();
        }

        public void Swap(IEntity first, IEntity second)
        {
            throw new NotImplementedException();
        }

        public IEntity[] ToArray()
        {
            throw new NotImplementedException();
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
