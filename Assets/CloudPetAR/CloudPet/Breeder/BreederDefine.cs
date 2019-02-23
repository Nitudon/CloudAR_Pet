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
}
