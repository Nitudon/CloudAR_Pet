using UnityEngine.XR.iOS;

namespace CloudPet.AR
{
    using GoogleARCore;
    using GoogleARCore.CrossPlatform;
    using UnityEngine;
    using UniRx;

    public class CloudAnchorModel
    {
        public ApplicationMode CloudMode{ get; private set; }

        private ReactiveProperty<bool> _isTrackable = new ReactiveProperty<bool>();
        public IReadOnlyReactiveProperty<bool> IsTrackable => _isTrackable;

#if UNITY_ANDROID
        private ReactiveProperty<Component> _placedAnchorRoot = new ReactiveProperty<Component>();
        public IReadOnlyReactiveProperty<Component> PlacedAnchorRoot => _placedAnchorRoot;
#endif

#if UNITY_IOS
        private ReactiveProperty<UnityARUserAnchorComponent> _placedAnchorRoot = new ReactiveProperty<UnityARUserAnchorComponent>();
        public IReadOnlyReactiveProperty<UnityARUserAnchorComponent> PlacedAnchorRoot => _placedAnchorRoot;
#endif

        private ReactiveProperty<XPAnchor> _resolvedAnchorInfo = new ReactiveProperty<XPAnchor>();
        public IReadOnlyReactiveProperty<XPAnchor> ResolvedAnchorInfo => _resolvedAnchorInfo;

        public Pose CurrentAnchor
        {
            get
            {
                switch (CloudMode)
                {
                    case ApplicationMode.Hosting:
                        if(_placedAnchorRoot.Value == null)
                        {
                            Debug.LogError("Missing Anchor");
                            return default(Pose);
                        }
                        return new Pose(_placedAnchorRoot.Value.transform.position, _placedAnchorRoot.Value.transform.rotation);
                    case ApplicationMode.Resolving:
                        if(_resolvedAnchorInfo.Value == null)
                        {
                            Debug.LogError("Missing Anchor");
                            return default(Pose);
                        }
                        return new Pose(_resolvedAnchorInfo.Value.transform.position, _resolvedAnchorInfo.Value.transform.rotation);
                    default:
                        Debug.LogError("Invalid Cloud Anchor Mode For Anchor Owner");
                        return default(Pose);
                }
            }
        }

        public void SetMode(ApplicationMode mode)
        {
            CloudMode = mode;
        }

#if UNITY_ANDROID
        public void SetPlacedAnchorRoot(bool trackable, Component anchor)
        {
            _isTrackable.Value = trackable;
            _placedAnchorRoot.Value = anchor;
        }
#endif

#if UNITY_IOS
        public void SetPlacedAnchorRoot(bool trackable, UnityARUserAnchorComponent anchor)
        {
            _isTrackable.Value = trackable;
            _placedAnchorRoot.Value = anchor;
        }
#endif

        public void SetResolvedAnchorInfo(XPAnchor anchor)
        {
            _resolvedAnchorInfo.Value = anchor;
        }
    }
}
