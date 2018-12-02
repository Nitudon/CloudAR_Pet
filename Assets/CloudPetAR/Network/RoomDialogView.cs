using UnityEngine;
using UnityEngine.UI;
using UdonLib.UI;

namespace CloudPet.Network
{
    public class RoomDialogView : DialogViewBase
    {
        [SerializeField]
        private InputField _nameField;
        public string Name => _nameField.text;

        [SerializeField]
        private CommonButtonBase _closeButton;
        public CommonButtonBase CloseButton => _closeButton;

        [SerializeField]
        private CommonButtonBase _decideButton;
        public CommonButtonBase DecideButton => _decideButton;
    }
}
