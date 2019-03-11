using System.Collections;
using System.Collections.Generic;
using UdonLib.Commons;
using UniRx;
using UnityEngine;

namespace CloudPet.Network
{
    public class RoomMessageBroker : UdonBehaviourSingleton<RoomMessageBroker>
    {
        private MessageBroker _messageBroker;

        public override void Initialize()
        {
            base.Initialize();
            _messageBroker = new MessageBroker();
        }
    }
}
