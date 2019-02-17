using System;
using CloudPet.Commons;
using UnityEngine;

namespace CloudPet.Pet
{
    public struct PetInfo : IEquatable<PetInfo>
    {
        public string Name;
        public PetAvater Avater;

        public bool Equals(PetInfo other)
        {
            return string.Equals(Name, other.Name) && Avater.Equals(other.Avater);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is PetInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Avater.GetHashCode();
            }
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

        private static readonly string[] CLIP_PATH =
        {
            "Pet_Idle",
            "Pet_Eat",
            "Pet_Play"
        };

        public static AnimationClip GetPetMotion(PetState state)
        {
            if (state == PetState.None)
            {
                Debug.LogError("Invalid Pet State For Animation Clip Resource Loading");
                return null;
            }

            return Resources.Load<AnimationClip>(CLIP_PATH[(int) state + 1]);
        }
    }
}
