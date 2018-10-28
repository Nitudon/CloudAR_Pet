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
        private Button _connectLobbyButton;

        [SerializeField]
        private Button _joinRoomButton;
    }
}
