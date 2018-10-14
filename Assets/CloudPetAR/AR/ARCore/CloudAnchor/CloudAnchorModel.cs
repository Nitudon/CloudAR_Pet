namespace CloudPet.AR
{
    using GoogleARCore;
    using GoogleARCore.CrossPlatform;
    using UniRx;

    public class CloudAnchorModel
    {
        public ApplicationMode CloudMode{ get; private set; }

        private ReactiveProperty<Anchor> _placedAnchorRoot = new ReactiveProperty<Anchor>();
        public IReadOnlyReactiveProperty<Anchor> PlacedAnchorRoot => _placedAnchorRoot;

        private ReactiveProperty<XPAnchor> _resolvedAnchorInfo = new ReactiveProperty<XPAnchor>();
        public IReadOnlyReactiveProperty<XPAnchor> ResolvedAnchorInfo => _resolvedAnchorInfo;

        public void SetMode(ApplicationMode mode)
        {
            CloudMode = mode;
        }

        public void SetPlacedAnchorRoot(Anchor anchor)
        {
            _placedAnchorRoot.Value = anchor;
        }

        public void SetAnchorInfo(XPAnchor anchor)
        {
            _resolvedAnchorInfo.Value = anchor;
        }
    }
}
