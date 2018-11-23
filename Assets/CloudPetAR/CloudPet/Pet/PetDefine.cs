using System;
using UnityEngine;

namespace CloudPet.Pet
{
    public struct PetInfo
    {
        public string Name;
        public PetAvater Avater;
    }

    public struct ActivateInfo : IEquatable<ActivateInfo>
    {
        public readonly bool Activatable;
        public readonly Pose Anchor;
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;

        public Vector3 AnchoredWorldPosition
        {
            get
            {
                return AnchorPositionUtility.GetWorldPointFromAnchorPoint(Anchor, Position);
            }
        }

        public ActivateInfo(bool activatable, Pose anchor, Vector3 position, Quaternion rotation)
        {
            Activatable = activatable;
            Anchor = anchor;
            Position = position;
            Rotation = rotation;
        }

        public bool Equals(ActivateInfo other)
        {
            return
                Activatable == other.Activatable &&
                Anchor.position == other.Anchor.position &&
                Anchor.rotation == other.Anchor.rotation &&
                Position == other.Position &&
                Rotation == other.Rotation;
        }

        public override int GetHashCode()
        {
            return Activatable.GetHashCode() ^ Anchor.position.GetHashCode() ^ Anchor.rotation.GetHashCode() ^ Position.GetHashCode() ^ Rotation.GetHashCode();
        }
    }

    public struct PetAvater
    {

    }

    public enum PetState
    {
        None,
        Idle,
        Eat,
        Play,
    }

    public static class PetDefine
    {
        public static readonly string PET_PREFAB_PATH = "PetCharacter";
    }
}
