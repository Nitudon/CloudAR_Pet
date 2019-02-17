using UnityEngine;

namespace CloudPet.Network
{
    public struct CloudTransformInfo
    {
        public readonly Vector3 Position;
        public readonly Vector3 Forward;

        public CloudTransformInfo(Vector3 _position, Vector3 _forward)
        {
            Position = _position;
            Forward = _forward;
        }
    }
}