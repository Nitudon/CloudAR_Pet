using UnityEngine;
using UdonLib.Commons;
using UniRx;
using UniRx.Async;
using CloudPet.Commons;
using CloudPet.UI;

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
            _roomUIView.JoinRoomButton.onClickedCallback += async () => await OpenJoinRoomDialog();
            _roomUIView.CreateRoomButton.onClickedCallback += async () => await OpenCreateRoomDialog();
        }

        private void Bind()
        {
            RoomManager.Instance.Model
                .onRoomCreated
                .Subscribe(async _ => await LoadRoomScene())
                .AddTo(gameObject);

            RoomManager.Instance.Model
                .onRoomJoined
                .Subscribe( async _ => await LoadRoomScene())
                .AddTo(gameObject);
        }

        private async UniTask OpenCreateRoomDialog()
        {
            var dialog = await DialogUtility.CreateDialog<RoomDialogPresenter>(DialogType.RoomDialog);
            await _roomConnector.CreateRoom(dialog.Result);
        }

        private async UniTask OpenJoinRoomDialog()
        {
            var dialog = await DialogUtility.CreateDialog<RoomDialogPresenter>(DialogType.RoomDialog);
            await _roomConnector.ConnectRoom(dialog.Result);
        }

        private async UniTask LoadRoomScene()
        {
            await CommonSceneManager.Instance.SceneLoadAsync(CommonUtility.GetSceneName(SceneEnum.Room));
        }
    }
}
