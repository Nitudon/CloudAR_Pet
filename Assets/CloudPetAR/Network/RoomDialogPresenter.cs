using UnityEngine;
using CloudPet.UI;
using UniRx.Async;

namespace CloudPet.Network
{
    [RequireComponent(typeof(RoomDialogView))]
    public class RoomDialogPresenter : CommonDialogPresenter<RoomDialogView, string>
    {
        public override void Initialize()
        {
            SetEvent();
            base.Initialize();
        }

        private void SetEvent()
        {
            _view.DecideButton.onClickedCallback += async () =>
            {
                _result = _view.Name;
                await CloseDialog();
            };

            _view.CloseButton.onClickedCallback += async () => await CloseDialog();
        }
    }
}
