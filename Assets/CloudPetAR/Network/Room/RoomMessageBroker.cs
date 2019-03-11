using System;
using System.Collections;
using System.Collections.Generic;
using CloudPet.Commons;
using UdonLib.Commons;
using UniRx;
using UnityEngine;

namespace CloudPet.Network
{
    public class RoomMessageBroker : UdonBehaviour, IInitializable
    {
        [SerializeField]
        private PhotonView _photonView;

        private MessageBroker _messageBroker;

        public IObservable<INetworkEvent> NetworkEventMessageReceiver => _messageBroker.Receive<INetworkEvent>();
        public IObservable<IRoomEvent> RoomEventMessageReceiver => _messageBroker.Receive<IRoomEvent>();

        public void Initialize()
        {
            _messageBroker = new MessageBroker();
        }

        public void PublishNetworkEvent(INetworkEvent pub)
        {
            if (!_photonView.isMine)
            {
                return;
            }

            _photonView.RPC(RPCDefine.SystemRPC.GetRPCMethod(RPCDefine.SystemRPC.RPCEnum.PublishNetworkEvent), PhotonTargets.All, pub);
        }

        public void PublishRoomEvent(IRoomEvent pub)
        {
            if (!_photonView.isMine)
            {
                return;
            }

            _photonView.RPC(RPCDefine.SystemRPC.GetRPCMethod(RPCDefine.SystemRPC.RPCEnum.PublishRoomEvent), PhotonTargets.All, pub);
        }

        [PunRPC]
        private void RPCPublishNetworkEvent(INetworkEvent pub)
        {
            _messageBroker.Publish(pub);
        }

        [PunRPC]
        private void RPCPublishRoomEvent(IRoomEvent pub)
        {
            _messageBroker.Publish(pub);
        }
    }

    public interface INetworkEvent
    {
        int ClientId { get; }
    }

    public interface IRoomEvent
    {
        int ClientId { get; }
    }
}
