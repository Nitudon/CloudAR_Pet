namespace CloudPet.Pet
{
    /// <summary>
    /// ブリーダーのUIの段階
    /// </summary>
    public enum UIMode
    {
        None,
        // 平面検出
        Anchor,
        // ペットを出す
        Activate,
        // ペットと遊ぶ（恒常）
        Breed
    }

    public struct BreederInfo
    {
        public string Id;
        public string Name;
    }

    public static class BreederDefine
    {
        /// <summary>
        /// ブリーダーの同期メソッド
        /// </summary>
        public enum BreederRPC
        {
            // ペット召喚
            PetActivate,
        }

        /// <summary>
        /// ブリーダーの同期メソッド名リスト
        /// </summary>
        public static readonly string[] BreederRPCMethods =
        {
            "RPCPetActivate",
        };

        /// <summary>
        /// ブリーダーの同期メソッド名を返す
        /// </summary>
        public static string GetBreederRPCMethod(BreederRPC type)
        {
            return BreederRPCMethods[(int) type];
        }
    }
}
