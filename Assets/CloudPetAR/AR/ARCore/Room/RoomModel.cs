namespace CloudPet.Network
{
    using UniRx;

    public class RoomModel
    {
        public string AnchorId{ get; private set; }

        public Subject<Unit> onRoomCreated = new Subject<Unit>();
        public Subject<Unit> onRoomJoined = new Subject<Unit>();

        private ReactiveProperty<string> _roomName = new ReactiveProperty<string>();
        public IReadOnlyReactiveProperty<string> RoomName => _roomName;

        public void SetAnchorId(string id)
        {
            AnchorId = id;
        }

        public void SetRoomName(string name)
        {
            _roomName.Value = name;
        }
    }
}
