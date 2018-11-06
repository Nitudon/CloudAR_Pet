using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UdonLib.Commons;

namespace CloudPet.Network
{
    public class RoomUIView : UdonBehaviour
    {
        [SerializeField]
        private Button _joinRoomButton;
        public Button JoinRoomButton => _joinRoomButton;

        [SerializeField]
        private Button _createRoomButton;
        public Button CreateRoomButton => _createRoomButton;
    }
}
