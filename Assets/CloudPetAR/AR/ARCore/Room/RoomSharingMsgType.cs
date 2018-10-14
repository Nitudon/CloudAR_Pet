﻿namespace CloudPet.AR
{
    using UnityEngine.Networking;

    /// <summary>
    /// Room Sharing Message Types.
    /// </summary>
    public struct RoomSharingMsgType
    {
        /// <summary>
        /// The Anchor id from room request message type.
        /// </summary>
        public const short AnchorIdFromRoomRequest = MsgType.Highest + 1;

        /// <summary>
        /// The Anchor id from room response message type.
        /// </summary>
        public const short AnchorIdFromRoomResponse = MsgType.Highest + 2;

        public const short CreateObjectFromUserRequest = MsgType.Highest + 3;
    }
}