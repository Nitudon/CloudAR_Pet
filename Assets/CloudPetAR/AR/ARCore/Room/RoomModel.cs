namespace CloudPet.Network
{
    using UniRx;

    public class RoomModel
    {
        private ReactiveProperty<string> _roomName = new ReactiveProperty<string>();
        public IReadOnlyReactiveProperty<string> RoomName => _roomName;

        public void SetRoomName(string name)
        {
            _roomName.Value = name;
        }
    }
}
