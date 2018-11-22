using UnityEngine;

namespace CloudPet.Pet
{
    public struct PetInfo
    {
        public string Name;
        public PetAvater Avater;
    }

    public struct ActivateInfo
    {
        public readonly bool Activatable;
        public readonly Vector3 Anchor;
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;

        public ActivateInfo(bool activatable, Vector3 anchor, Vector3 position, Quaternion rotation)
        {
            Activatable = activatable;
            Anchor = anchor;
            Position = position;
            Rotation = rotation;
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
