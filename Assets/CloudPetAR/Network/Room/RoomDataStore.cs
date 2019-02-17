using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace CloudPet.Network
{
    public class RoomDataStore : IDataStore
    {
        private Subject<IEnumerable<RoomData>> _roomList;
        public IObservable<IEnumerable<RoomData>> RoomList => _roomList;

        public RoomDataStore()
        {
            _roomList = new Subject<IEnumerable<RoomData>>();
        }

        public void ReceiveData()
        {
            var roomList = PhotonNetwork.GetRoomList();
            _roomList.OnNext(roomList.Select(room => new RoomData(room)));
        }
    }
}
