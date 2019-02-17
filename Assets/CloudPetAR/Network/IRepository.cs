using System;
using UniRx;

namespace CloudPet.Network
{
    public interface IRepository
    {
        void Bind();
    }

    public abstract class RepositoryBase<TDataStore> : IRepository
        where TDataStore : IDataStore
    {
        protected TDataStore _dataStore;

        public void SetDataStore(TDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public virtual void Bind()
        {

        }
    }
}