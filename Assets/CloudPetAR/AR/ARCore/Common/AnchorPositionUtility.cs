using System;
using CloudPet.Network;
using UnityEngine;

namespace CloudPet.Commons
{
    public static class AnchorPositionUtility
    {
        private static readonly Vector3 SCALE_MATRIX = Vector3.one;

        public static Tuple<Vector3, Vector3> GetAnchorTransform(Pose anchor, CloudTransformInfo info)
        {
            return new Tuple<Vector3, Vector3>(GetAnchorPointFromWorldPoint(anchor, info.Position), GetAnchorPointFromWorldPoint(anchor, info.Forward));
        }

        public static Vector3 GetAnchorPointFromWorldPoint(Transform anchor, Vector3 position)
        {
            return AnchorMatrix(anchor).inverse.MultiplyPoint3x4(position);
        }

        public static Vector3 GetAnchorPointFromWorldPoint(Pose anchor, Vector3 position)
        {
            return AnchorMatrix(anchor).inverse.MultiplyPoint3x4(position);
        }

        public static Vector3 GetWorldPointFromAnchorPoint(Transform anchor, Vector3 position)
        {
            return AnchorMatrix(anchor).MultiplyPoint3x4(position);
        }

        public static Vector3 GetWorldPointFromAnchorPoint(Pose anchor, Vector3 position)
        {
            return AnchorMatrix(anchor).MultiplyPoint3x4(position);
        }

        private static Matrix4x4 AnchorMatrix(Transform anchor)
        {
            return Matrix4x4.TRS(anchor.position, anchor.rotation, SCALE_MATRIX);
        }

        private static Matrix4x4 AnchorMatrix(Pose anchor)
        {
            return Matrix4x4.TRS(anchor.position, anchor.rotation, SCALE_MATRIX);
        }
    }
}
