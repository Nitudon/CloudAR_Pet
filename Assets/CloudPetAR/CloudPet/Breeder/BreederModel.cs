using UniRx;

namespace CloudPet.Pet
{
    /// <summary>
    /// ブリーダーのパラメータモデル
    /// </summary>
    public class BreederModel
    {
        /// <summary>
        /// 識別ID
        /// </summary>
        private string _id;
        public string Id => _id;

        /// <summary>
        /// ブリーダーのユーザー名
        /// </summary>
        private string _name;
        public string Name => _name;

        /// <summary>
        /// 現在のブリーダーの操作モード
        /// </summary>
        private ReactiveProperty<UIMode> _mode = new ReactiveProperty<UIMode>();
        public IReadOnlyReactiveProperty<UIMode> Mode => _mode;

        /// <summary>
        /// ブリーダーのペット生成時の通知
        /// </summary>
        private Subject<ActivateInfo> _onActivatePet = new Subject<ActivateInfo>();
        public Subject<ActivateInfo> OnActivatePet => _onActivatePet;

        public void SetBreeder(string id, string name)
        {
            _id = id;
            _name = name;
        }

        public void SetMode(UIMode mode)
        {
            _mode.Value = mode;
        }
    }
}
