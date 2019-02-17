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

        public override async UniTask Initialize()
        {
            await _roomConnector.Initialize();

            if(PhotonNetwork.isNonMasterClientInRoom)
            {
                CloudAnchorManager.Instance.SetResolverMode();
            }
            else
            {
                CloudAnchorManager.Instance.SetHostMode();
            }
        }
    }
}
