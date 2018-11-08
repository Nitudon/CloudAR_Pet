using UnityEngine;
using UnityEngine.UI;
using UdonLib.UI;

namespace CloudPet.Network
{
    public class RoomJoinDialogView : DialogViewBase
    {
        [SerializeField]
        private InputField _nameField;
        public string Name => _nameField.text;

        [SerializeField]
        private Button _closeButton;
        public Button CloseButton => _closeButton;

        [SerializeField]
        private Button _decideButton;
        public Button DecideButton => _decideButton;
    }
}
