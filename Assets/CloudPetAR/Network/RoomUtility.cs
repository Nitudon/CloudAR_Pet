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
            option.CustomRoomProperties
                = new ExitGames.Client.Photon.Hashtable() {
                    { RoomDefine.ANCHOR_KEY, anchorId }
                };
            option.CustomRoomPropertiesForLobby = new string[] {
                    RoomDefine.ANCHOR_KEY
                };

            return option;
        }
    }
}
