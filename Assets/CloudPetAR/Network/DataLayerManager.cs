using System.Collections;
using System.Collections.Generic;
using UdonLib.Commons;
using UdonLib;

namespace CloudPet.Network
{
    public class DataLayerManager : UdonBehaviourSingleton<DataLayerManager>
    {
        protected override bool IsDontDestroy => true;
        private IDataStore[] _dataStores;

        public override void Initialize()
        {
            base.Initialize();
            RepositoryManager.InstanceObject.transform.SetParent(transform);
            BindDataLayer();
        }

        private void BindDataLayer()
        {
            // 全データストア生成
            #region Generate DataStore

            RoomDataStore roomDataStore = new RoomDataStore();

            _dataStores = new IDataStore[]
            {
                #region DataStore List
                roomDataStore,
                #endregion
            };

            #endregion

            // データストアとリポジトリ紐づけ
            #region Bind DataStore and Repository

            RepositoryManager.Instance.RoomRepository.SetDataStore(roomDataStore);

            RepositoryManager.Instance.BindAll();

            #endregion
        }

        public void ReceiveData()
        {
            _dataStores.ForEach(store => store.ReceiveData());
        }
    }
}
