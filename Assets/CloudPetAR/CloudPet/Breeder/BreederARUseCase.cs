using System;
using UniRx;
using UdonLib.Commons;
using CloudPet.AR;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.CrossPlatform;

namespace CloudPet.Pet
{
    /// <summary>
    /// ブリーダーのAR周りのユースケース
    /// </summary>
    public class BreederARUseCase
    {
        private PlaneDetectionGesture _planeDetectionGesture;

        private CloudAnchorModel _anchorModel;

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

        public IReadOnlyReactiveProperty<Tuple<bool, Anchor>> TrackingHitInfoEveryChanged
        {
            get
            {
                return _planeDetectionGesture
                        .DetectedPose
                        .Where(_ => !_planeDetectionGesture.IsDestroyed)
                        .Select(info => new Tuple<bool, Anchor>(info.Item1, info.Item2.Trackable.CreateAnchor(info.Item2.Pose)))
                        .ToReactiveProperty();
            }
        }

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
