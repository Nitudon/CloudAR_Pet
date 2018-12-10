using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UdonLib.Commons;
using UniRx;
using CloudPet.Common;

namespace CloudPet.Network
{
    public class RoomPresenter : InitializableMono
    {
        [SerializeField]
        private RoomConnector _roomConnector;

        [SerializeField]
        private RoomUIView _roomUIView;

        public override void Initialize()
        {
            SetEvent();
            Bind();
        }

        private void SetEvent()
        {
            _roomUIView.JoinRoomButton.onClickedCallback += async () => await _roomConnector.ConnectRoom();
            _roomUIView.CreateRoomButton.onClickedCallback += async () => await _roomConnector.CreateRoom();
        }

        private void Bind()
        {
            RoomManager.Instance.Model
                .onRoomCreated
                .Subscribe(_ => LoadRoomScene())
                .AddTo(gameObject);

            RoomManager.Instance.Model
                .onRoomJoined
                .Subscribe(_ => LoadRoomScene())
                .AddTo(gameObject);
        }

        private async Task OpenCreateRoomDialog()
        {
            //RoomDialogPresenter dialog = RoomDialogPresenter.OpenDialog
            await _roomConnector.ConnectRoom();
        }

        private async Task OpenJoinRoomDialog()
        {
            //await _roomConnector.CreateRoom();
        }

        private void LoadRoomScene()
        {
            SceneManager.Instance.SceneLoad(CommonUtility.GetSceneName(SceneEnum.Room));
        }
    }
}
