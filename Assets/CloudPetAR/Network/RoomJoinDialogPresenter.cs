using UnityEngine;
using UnityEngine.UI;
using UdonLib.UI;

namespace CloudPet.Network
{
    [RequireComponent(typeof(RoomJoinDialogView))]
    public class RoomJoinDialogPresenter : DialogPresenterBase<RoomJoinDialogView, string>
    {
        private void SetEvent()
        {

        }
    }
}
