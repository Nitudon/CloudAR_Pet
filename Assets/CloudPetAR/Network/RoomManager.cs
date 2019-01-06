using UdonLib.Commons;

namespace CloudPet.Network
{
    /// <summary>
    /// ルーム関連のマネージャー
    /// </summary>
    public class RoomManager : UdonBehaviourSingleton<RoomManager>
    {
        private RoomModel _model;
        public RoomModel Model => _model;

        public string AnchorId => _model.AnchorId.Value;

        protected override bool IsDontDestroy => true;

        public override void Initialize()
        {
            base.Initialize();

            _model = new RoomModel();
        }

        public void SetRoomAnchorId(string anchorId)
        {
            if(PhotonNetwork.isNonMasterClientInRoom)
            {
                return;
            }

            _model.SetAnchorId(anchorId);

            var option = new ExitGames.Client.Photon.Hashtable() {
                    { RoomDefine.ANCHOR_KEY, anchorId }
                };
            PhotonNetwork.room.SetCustomProperties(option);
        }
    }
}
