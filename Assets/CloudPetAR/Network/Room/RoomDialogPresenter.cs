using UnityEngine;
using CloudPet.UI;
using UdonLib.Commons.Extensions;
using UniRx.Async;

namespace CloudPet.Network
{
    [RequireComponent(typeof(RoomDialogView))]
    public class RoomDialogPresenter : CommonDialogPresenter<RoomDialogView, string>
    {
        protected override void SetEvent()
        {
            base.SetEvent();
            _view.DecideButton.onClickedCallback += async () =>
            {
                if (_view.Name.IsNullOrWhiteSpace())
                {
                    return;
                }
                _result = _view.Name;

                await CloseDialog();
            };

            _view.CloseButton.onClickedCallback += async () => await CloseDialog();
        }
    }
}
