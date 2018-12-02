using UnityEngine;
using UnityEngine.UI;
using UdonLib.UI;

namespace CloudPet.Network
{
    [RequireComponent(typeof(RoomDialogView))]
    public class RoomDialogPresenter : DialogPresenterBase<RoomDialogView, string>
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
