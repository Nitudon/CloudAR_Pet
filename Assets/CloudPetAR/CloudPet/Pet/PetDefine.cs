namespace CloudPet.Pet
{
    public struct PetInfo
    {
        public string Name;
        public PetAvater Avater;
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
