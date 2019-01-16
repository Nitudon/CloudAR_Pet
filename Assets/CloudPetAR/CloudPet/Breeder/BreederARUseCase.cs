using System;
using UniRx;
using UdonLib.Commons;
using CloudPet.AR;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.CrossPlatform;
using GoogleARCore.Examples.CloudAnchor;
#if UNITY_IOS
using UnityEngine.XR.iOS;
#endif

namespace CloudPet.Pet
{
    /// <summary>
    /// ブリーダーのAR周りの検知ロジック
    /// </summary>
    public class BreederARUseCase
    {
        private PlaneDetectionGesture _planeDetectionGesture;

        private CloudAnchorModel _anchorModel;

#if UNITY_IOS
        private ARKitHelper _iosARHelper = new ARKitHelper();
#endif

        public BreederARUseCase(PlaneDetectionGesture planeDetectionGesture, CloudAnchorModel cloudAnchorModel)
        {
            _planeDetectionGesture = planeDetectionGesture;
            _anchorModel = cloudAnchorModel;

            _planeDetectionGesture.Initialize();
        }

        public void SelectPlaneEnable(bool active)
        {
            _planeDetectionGesture.SetDetectionActive(active);
        }

#if UNITY_ANDROID
        public IReadOnlyReactiveProperty<Tuple<bool, Anchor>> TrackingHitInfoEveryChanged
        {
            get
            {
                return _planeDetectionGesture
                        .DetectedPose
                        .Where(info => !_planeDetectionGesture.IsDestroyed && info != null)
                        .Select(info => new Tuple<bool, Anchor>(info.Item1, info.Item1 ? info.Item2.Trackable.CreateAnchor(info.Item2.Pose) : null))
                        .ToReactiveProperty();
            }
        }
#endif

#if UNITY_IOS
        public IReadOnlyReactiveProperty<Tuple<bool, UnityARUserAnchorComponent>> TrackingHitInfoEveryChanged
        {
            get
            {
                return _planeDetectionGesture
                    .DetectedPose
                    .Where(info => !_planeDetectionGesture.IsDestroyed && info != null)
                    .Select(info => new Tuple<bool, UnityARUserAnchorComponent>(info.Item1, info.Item1 ? _iosARHelper.CreateAnchor(info.Item2) : null))
                    .ToReactiveProperty();
            }
        }
#endif

        public IReadOnlyReactiveProperty<ActivateInfo> ActivateInfoEveryChanged
        {
            get
            {
                return _planeDetectionGesture
                        .DetectedPose
                        .Where(_ => !_planeDetectionGesture.IsDestroyed)
                        .Select(info => new ActivateInfo(info.Item1, _anchorModel.CurrentAnchor, info.Item2.Pose.position, info.Item2.Pose.rotation))
                        .ToReactiveProperty();
            }
        }
    }
}
