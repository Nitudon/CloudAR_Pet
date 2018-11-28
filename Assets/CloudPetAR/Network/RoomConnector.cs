namespace CloudPet.Network
{
    using System;
    using System.Threading.Tasks;
    using PhotonRx;
    using UniRx;
    using UnityEngine;
    using UdonLib.Commons;

    public class RoomConnector : AsyncInitializableMono
    {
        private static readonly string CONNECTING_SETTING = "v1";
        private static readonly byte MAX_ROOM_USER = 4;
        private static readonly string ANCHOR_KEY = "AnchorId";

        public override async Task Initialize()
        {
            PhotonNetwork.autoJoinLobby = true;
            var connect = await PhotoTask.ConnectUsingSettings(CONNECTING_SETTING);
            if (connect.IsFailure)
            {
                InstantLog.StringLogError(connect.ToFailure.Value);
                return;
            }
        }

        public async Task ConnectRoom(string roomName = "")
        {
            if (string.IsNullOrWhiteSpace(roomName))
            {
                await FailureHandlingPhotonTask(PhotoTask.JoinRandomRoom(), _ => RoomManager.Instance.Model.SetRoomName(PhotonNetwork.room.Name));
            }
            else
            {
                await FailureHandlingPhotonTask(PhotoTask.JoinRoom(roomName), _ =>
                {
                    var roomProperty = PhotonNetwork.room.CustomProperties;
                    RoomManager.Instance.Model.SetRoomName(roomName);
                    object anchorId;
                    if(roomProperty.TryGetValue(ANCHOR_KEY, out anchorId))
                    {
                        RoomManager.Instance.Model.SetAnchorId(anchorId.ToString());
                    }
                });
            }
        }

        public async Task CreateRoom(string roomName, string anchorId)
        {
            if (string.IsNullOrWhiteSpace(roomName))
            {
                return;
            }

            var option = GetCloudRoomTemplate(anchorId);
            await FailureHandlingPhotonTask(PhotoTask.CreateRoom(roomName, option, null), _ =>
            {
                RoomManager.Instance.Model.SetRoomName(roomName);

            });
        }

        private async Task FailureHandlingPhotonTask(Task<IResult<FailureReason, bool>> task, Action<IResult<FailureReason, bool>> onSuccess = null, Action<IResult<FailureReason, bool>> onFailure = null)
        {
            var result = await task;
            if(result.IsSuccess)
            {
                onSuccess?.Invoke(result);
            }
            else
            {
                InstantLog.StringLogError($"Photon Network Error : {result.ToFailure.Value}");
                onFailure?.Invoke(result);
            }
        }

        private RoomOptions GetCloudRoomTemplate(string anchorId, bool visible = true)
        {
            var option = new RoomOptions();
            option.IsVisible = visible;
            option.IsOpen = true;
            option.MaxPlayers = MAX_ROOM_USER;
            option.CustomRoomProperties 
                = new ExitGames.Client.Photon.Hashtable() { 
                    { ANCHOR_KEY, anchorId }
                };
            option.CustomRoomPropertiesForLobby = new string[] { 
                    ANCHOR_KEY 
                };

            return option;
        }
    }
}
