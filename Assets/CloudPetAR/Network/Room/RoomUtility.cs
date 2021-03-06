﻿using UdonLib.Commons.Extensions;

namespace CloudPet.Network
{
    public static class RoomUtility
    {
        public static RoomOptions GetCloudRoomTemplate(string anchorId, bool visible = true)
        {
            var option = new RoomOptions();
            option.IsVisible = visible;
            option.IsOpen = true;
            option.MaxPlayers = RoomDefine.MAX_ROOM_USER;

            var id = anchorId.IsNullOrWhiteSpace() ? System.Guid.NewGuid().ToString() : anchorId;

            option.CustomRoomProperties
                = new ExitGames.Client.Photon.Hashtable() {
                    { RoomDefine.ANCHOR_KEY, id }
                };
            option.CustomRoomPropertiesForLobby = new string[] {
                    RoomDefine.ANCHOR_KEY
                };

            return option;
        }
    }
}
