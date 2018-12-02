using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UdonLib.Commons;
using UdonLib.UI;

namespace CloudPet.Network
{
    public class RoomUIView : UdonBehaviour
    {
        [SerializeField]
        private CommonButtonBase _joinRoomButton;
        public CommonButtonBase JoinRoomButton => _joinRoomButton;

        [SerializeField]
        private CommonButtonBase _createRoomButton;
        public CommonButtonBase CreateRoomButton => _createRoomButton;

        [SerializeField]
        private InputField _roomNameField;
        public string RoomName => _roomNameField.text;
    }
}
