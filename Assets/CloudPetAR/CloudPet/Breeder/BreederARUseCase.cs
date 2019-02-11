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
    /// ブリーダーのAR周りのロジック
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
        }

        /// <summary>
        /// 自動で画面中央からレイキャストで平面検地するやつのオンオフ
        /// </summary>
        /// <param name="active"></param>
        public void SelectAutomaticTrackingEnable(bool active)
        {
            _planeDetectionGesture.SetAutomaticDetectionActive(active);
        }

        /// <summary>
        /// プレイヤー操作でタップ箇所からレイキャストで平面検地するやつのオンオフ
        /// </summary>
        /// <param name="active"></param>
        public void SelectManualTrackingEnable(bool active)
        {
            _planeDetectionGesture.SetManualDetectionActive(active);
        }

#if UNITY_ANDROID
        /// <summary>
        /// 平面検知情報の購読用ストリーム、検知したかどうかとその検知した地点を返す
        /// </summary>
        public IObservable<Tuple<bool, Anchor>> AutomaticCenterTrackingHitInfo
        {
            get
            {
                return _planeDetectionGesture
                    .AutomaticCenterTrackingDetectedPose
                    .Where(info => !_planeDetectionGesture.IsDestroyed && info != null)
                    .Select(info => new Tuple<bool, Anchor>(info.Item1,
                        info.Item1 ? info.Item2.Trackable.CreateAnchor(info.Item2.Pose) : null));
            }
        }

        /// <summary>
        /// 平面検知情報の購読用ストリーム、検知したかどうかとその検知した地点を返す
        /// </summary>
        public IObservable<Tuple<bool, Anchor>> ManualTouchTrackingHitInfo
        {
            get
            {
                return _planeDetectionGesture
                    .ManualTouchTrackingDetectedPose
                    .Where(info => !_planeDetectionGesture.IsDestroyed && info != null)
                    .Select(info => new Tuple<bool, Anchor>(info.Item1,
                        info.Item1 ? info.Item2.Trackable.CreateAnchor(info.Item2.Pose) : null));
            }
        }
#endif

#if UNITY_IOS
        /// <summary>
        /// 平面検知情報の購読用ストリーム、検知したかどうかとその検知した地点を返す
        /// </summary>
        public IObservable<Tuple<bool, UnityARUserAnchorComponent>> TrackingHitInfoEveryChanged
        {
            get
            {
                return _planeDetectionGesture
                    .DetectedPose
                    .Where(info => !_planeDetectionGesture.IsDestroyed && info != null)
                    .Select(info => new Tuple<bool, UnityARUserAnchorComponent>(info.Item1, info.Item1 ? _iosARHelper.CreateAnchor(info.Item2) : null));
            }
        }
#endif
        /// <summary>
        /// ペット召喚情報の購読用ストリーム
        /// </summary>
        public IReadOnlyReactiveProperty<ActivateInfo> ActivateInfoEveryChanged
        {
            get
            {
                return _planeDetectionGesture
                        .AutomaticCenterTrackingDetectedPose
                        .Where(_ => !_planeDetectionGesture.IsDestroyed)
                        .Select(info => new ActivateInfo(info.Item1, _anchorModel.CurrentAnchor, info.Item2.Pose.position, info.Item2.Pose.rotation))
                        .ToReactiveProperty();
            }
        }
    }
}
