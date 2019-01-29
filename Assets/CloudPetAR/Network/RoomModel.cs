using System;
using UniRx;

namespace CloudPet.Network
{
    public class RoomModel
    {
        private Subject<Unit> _onRoomCreated = new Subject<Unit>();
        private Subject<Unit> _onRoomJoined = new Subject<Unit>();

        public IObservable<Unit> OnRoomCreated => _onRoomCreated;
        public IObservable<Unit> OnRoomJoined => _onRoomJoined;

        private ReactiveProperty<string> _anchorId = new ReactiveProperty<string>();
        public IReadOnlyReactiveProperty<string> AnchorId => _anchorId;

        private ReactiveProperty<string> _roomName = new ReactiveProperty<string>();
        public IReadOnlyReactiveProperty<string> RoomName => _roomName;

        public void SetAnchorId(string id)
        {
            _anchorId.Value = id;
        }

        public void CreateRoom(string name)
        {
            _roomName.Value = name;
            _onRoomCreated.OnNext(Unit.Default);
        }

        public void JoinRoom(string name)
        {
            _roomName.Value = name;
            _onRoomJoined.OnNext(Unit.Default);
        }
    }
}
