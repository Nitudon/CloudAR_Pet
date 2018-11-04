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
        public readonly Vector3 Anchor;
        public readonly Vector3 Position;

        public ActivateInfo(Vector3 anchor, Vector3 position)
        {
            Anchor = anchor;
            Position = position;
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
