namespace CloudPet.Commons
{
    public enum SceneEnum
    {
        None,
        Lobby,
        Room,
    }

    public static class CommonUtility
    {
        public static string GetSceneName(SceneEnum scene)
        {
            switch (scene)
            {
                case SceneEnum.Lobby:
                    return "RoomStep";
                case SceneEnum.Room:
                #if UNITY_ANDROID
                    return "CloudPetAR_Android";
                #endif
                #if UNITY_IOS
                    return "CloudPetAR_iOS";
                #endif
                default:
                    return string.Empty;
            }
        }
    }
}