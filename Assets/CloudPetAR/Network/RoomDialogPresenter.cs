using UnityEngine;
using CloudPet.UI;
using UniRx.Async;

namespace CloudPet.Network
{
    [RequireComponent(typeof(RoomDialogView))]
    public class RoomDialogPresenter : CommonDialogPresenter<RoomDialogView, string>
    {
        public override async UniTask Initialize()
        {
            await base.Initialize();
            SetEvent();
        }

        private void SetEvent()
        {
            _view.DecideButton.onClickedCallback += () => 
            {
                _result = _view.Name;
                onClosedCallback?.Invoke();
            };

            _view.CloseButton.onClickedCallback += () =>
            {
                onClosedCallback?.Invoke();
            };
        }
    }
}
