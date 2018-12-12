using UnityEngine;
using UnityEngine.UI;
using UdonLib.UI;
using CloudPet.UI;

namespace CloudPet.Network
{
    public class RoomDialogView : CommonDialogView
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
