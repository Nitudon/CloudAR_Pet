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
                    return "Lobby";
                case SceneEnum.Room:
                    return "Room";
                default:
                    return string.Empty;
            }
        }
    }
}