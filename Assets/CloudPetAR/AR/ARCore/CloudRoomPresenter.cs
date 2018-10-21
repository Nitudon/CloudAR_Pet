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
                .AnchorId
                .Where(_ => PhotonNetwork.isNonMasterClientInRoom)
                .Subscribe(_cloudAnchorController.ResolveAnchorFromId)
                .AddTo(gameObject);
        }
    }
}
