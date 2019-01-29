using System;
using System.Threading.Tasks;
using PhotonRx;
using UniRx.Async;
using UdonLib.Commons;

namespace CloudPet.Network
{

    public class RoomConnector : AsyncInitializableMono
    {
        public override async UniTask Initialize()
        {
            PhotonNetwork.autoJoinLobby = true;
            var connect = await PhotoTask.ConnectUsingSettings(RoomDefine.CONNECTING_SETTING);
            if (connect.IsFailure)
            {
                InstantLog.StringLogError(connect.ToFailure.Value);
                return;
            }
        }

        public async UniTask ConnectRoom(string roomName = "")
        {
            if (string.IsNullOrWhiteSpace(roomName))
            {
                await FailureHandlingPhotonTask(PhotoTask.JoinRandomRoom(), _ => RoomManager.Instance.Model.JoinRoom(PhotonNetwork.room.Name));
            }
            else
            {
                await FailureHandlingPhotonTask(PhotoTask.JoinRoom(roomName), _ =>
                {
                    var roomProperty = PhotonNetwork.room.CustomProperties;
                    RoomManager.Instance.Model.JoinRoom(roomName);
                    if(roomProperty.TryGetValue(RoomDefine.ANCHOR_KEY, out var anchorId))
                    {
                        RoomManager.Instance.Model.SetAnchorId(anchorId.ToString());
                    }
                });
            }
        }

        public async UniTask CreateRoom(string roomName)
        {
            if (string.IsNullOrWhiteSpace(roomName))
            {
                return;
            }

            var option = RoomUtility.GetCloudRoomTemplate(string.Empty);
            await FailureHandlingPhotonTask(PhotoTask.CreateRoom(roomName, option, null), _ =>
            {
                RoomManager.Instance.Model.CreateRoom(roomName);
                if (option.CustomRoomProperties.TryGetValue(RoomDefine.ANCHOR_KEY, out var anchorId))
                {
                    RoomManager.Instance.Model.SetAnchorId(anchorId.ToString());
                }
            });
        }

        public async UniTask CreateRoom(string roomName, string anchorId)
        {
            if (string.IsNullOrWhiteSpace(roomName))
            {
                return;
            }

            var option = RoomUtility.GetCloudRoomTemplate(anchorId);
            await FailureHandlingPhotonTask(PhotoTask.CreateRoom(roomName, option, null), _ =>
            {
                RoomManager.Instance.Model.CreateRoom(roomName);
                RoomManager.Instance.Model.SetAnchorId(anchorId);
            });
        }

        private async UniTask FailureHandlingPhotonTask(Task<IResult<FailureReason, bool>> task, Action<IResult<FailureReason, bool>> onSuccess = null, Action<IResult<FailureReason, bool>> onFailure = null)
        {
            var result = await task;
            if(result.IsSuccess)
            {
                InstantLog.ObjectLog($"Photon Network Success : {result}", StringExtensions.TextColor.cyan);
                onSuccess?.Invoke(result);
            }
            else
            {
                InstantLog.ObjectLog($"Photon Network Error : {result}", StringExtensions.TextColor.magenta);
                onFailure?.Invoke(result);
            }
        }
    }
}
