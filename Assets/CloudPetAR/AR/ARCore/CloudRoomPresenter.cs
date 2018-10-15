using System.Threading.Tasks;
using UnityEngine;
using UdonLib.Commons;
using UniRx;
using CloudPet.Network;

namespace CloudPet.AR
{
    public class CloudRoomPresenter : AsyncInitializableMono
    {
        [SerializeField]
        private RoomConnector _roomConnector;

        [SerializeField]
        private CloudAnchorSystem _cloudAnchorController;

        public override async Task Initialize()
        {
            await _roomConnector.Initialize();
            _cloudAnchorController.Initialize();

            BindRoomConnection();
        }

        private void BindRoomConnection()
        {
            _roomConnector
                .Model
                .onRoomCreated
                .Subscribe(_ => BindHost())
                .AddTo(gameObject);

            _roomConnector
                .Model
                .onRoomJoined
                .Subscribe()
                .AddTo(gameObject);
        }

        private void BindHost()
        {

        }

        private void BindResolver()
        {
            
        }
    }
}
