using UnityEngine;
using UnityEngine.Networking;

namespace CloudPet.AR
{
    public class CreateObjectFromUserRequestMessage : MessageBase
    {
        public readonly int ObjectId;
        public readonly Vector3 Position;
        public readonly Vector3 Forward;
    }
}
