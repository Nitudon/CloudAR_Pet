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

        private RoomModel _model = new RoomModel();
        public RoomModel Model => _model;

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

        public async Task ConnectRoom(string roomName)
        {
            if (string.IsNullOrWhiteSpace(roomName))
            {
                await FailureHandlingPhotonTask(PhotoTask.JoinRandomRoom(), _ => _model.SetRoomName(PhotonNetwork.room.Name));
            }
            else
            {
                await FailureHandlingPhotonTask(PhotoTask.JoinRoom(roomName), _ => _model.SetRoomName(roomName));
            }
        }

        public async Task CreateRoom(string roomName)
        {
            if (string.IsNullOrWhiteSpace(roomName))
            {
                return;
            }

            await FailureHandlingPhotonTask(PhotoTask.JoinRoom(roomName), _ => _model.SetRoomName(roomName));
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
    }
}
