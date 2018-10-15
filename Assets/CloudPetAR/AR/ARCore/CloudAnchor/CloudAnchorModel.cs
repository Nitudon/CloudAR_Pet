namespace CloudPet.AR
{
    using GoogleARCore;
    using GoogleARCore.CrossPlatform;
    using UnityEngine;
    using UniRx;

    public class CloudAnchorModel
    {
        public ApplicationMode CloudMode{ get; private set; }

        private ReactiveProperty<Component> _placedAnchorRoot = new ReactiveProperty<Component>();
        public IReadOnlyReactiveProperty<Component> PlacedAnchorRoot => _placedAnchorRoot;

        private ReactiveProperty<XPAnchor> _resolvedAnchorInfo = new ReactiveProperty<XPAnchor>();
        public IReadOnlyReactiveProperty<XPAnchor> ResolvedAnchorInfo => _resolvedAnchorInfo;

        public void SetMode(ApplicationMode mode)
        {
            CloudMode = mode;
        }

        public void SetPlacedAnchorRoot(Component anchor)
        {
            _placedAnchorRoot.Value = anchor;
        }

        public void SetAnchorInfo(XPAnchor anchor)
        {
            _resolvedAnchorInfo.Value = anchor;
        }
    }
}
