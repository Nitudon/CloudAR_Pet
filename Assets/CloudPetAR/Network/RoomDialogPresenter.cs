using UnityEngine;
using UnityEngine.UI;
using UdonLib.UI;
using CloudPet.UI;

namespace CloudPet.Network
{
    [RequireComponent(typeof(RoomDialogView))]
    public class RoomDialogPresenter : CommonDialogPresenter<RoomDialogView, string>
    {
        public override void Initialize()
        {
            base.Initialize();
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
