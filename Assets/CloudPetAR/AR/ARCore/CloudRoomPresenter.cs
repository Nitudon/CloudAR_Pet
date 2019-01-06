using System.Threading.Tasks;
using UnityEngine;
using UdonLib.Commons;
using UniRx;
using CloudPet.Network;
using UniRx.Async;

namespace CloudPet.AR
{
    public class CloudRoomPresenter : AsyncInitializableMono
    {
        [SerializeField]
        private RoomConnector _roomConnector;

        [SerializeField]
        private CloudAnchorSystem _cloudAnchorController;

        public override async UniTask Initialize()
        {
            await _roomConnector.Initialize();
            _cloudAnchorController.Initialize();

            if(PhotonNetwork.isNonMasterClientInRoom)
            {
                _cloudAnchorController.SetResolverMode();
            }
            else
            {
                _cloudAnchorController.SetHostMode();
            }
        }
    }
}
