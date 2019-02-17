using System;
using System.Collections.Generic;
using UniRx;

namespace CloudPet.Network
{
    public class RoomRepository : RepositoryBase<RoomDataStore>
    {
        private IReadOnlyReactiveProperty<IEnumerable<RoomData>> _roomList;
        public IReadOnlyReactiveProperty<IEnumerable<RoomData>> RoomList => _roomList;

        public override void Bind()
        {
            base.Bind();

            _roomList =
                _dataStore
                    .RoomList
                    .ToReactiveProperty();
        }
    }
}
