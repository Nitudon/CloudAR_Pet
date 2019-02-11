using System;
using System.Collections;
using UnityEngine;
using UdonLib.Commons;
using UniRx;
using GoogleARCore;
using GoogleARCore.Examples.CloudAnchor;
#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif

namespace CloudPet.AR
{
    /// <summary>
    /// 平面検出タッチハンドラ
    /// </summary>
    public class PlaneDetectionGesture : InitializableMono, IDisposable
    {
        [SerializeField]
        private PlaneDetectionView view;

#if UNITY_IOS
        [Header("ARKit")]

        [SerializeField]
        private Transform ARKitRoot;

        [SerializeField]
        private Camera ARKitFirstPersonCamera;

        private ARKitHelper _iosARHelper = new ARKitHelper();
#endif

        private static readonly Vector2 DETECT_RAY_CENTER = Vector2.zero;

        private Subject<Tuple<bool, TrackableHit>> _automaticCenterTrackingDetectedPose;
        public IObservable<Tuple<bool, TrackableHit>> AutomaticCenterTrackingDetectedPose => _automaticCenterTrackingDetectedPose;

        private Subject<Tuple<bool, TrackableHit>> _manualTouchTrackingDetectedPose;
        public IObservable<Tuple<bool, TrackableHit>> ManualTouchTrackingDetectedPose => _manualTouchTrackingDetectedPose;

        private Subject<Unit> _onTouched;
        public IObservable<Unit> OnTouched => _onTouched;

        private IDisposable _automaticCenterTrackingDetector;
        private IDisposable _manualTouchTrackingDetector;

        public override void Initialize()
        {
#if !UNITY_EDITOR
            Input.multiTouchEnabled = false;
#endif
            _onTouched = new Subject<Unit>();
            _automaticCenterTrackingDetector = new Subject<Tuple<bool, TrackableHit>>();
            _manualTouchTrackingDetector = new Subject<Tuple<bool, TrackableHit>>();
        }

        /// <summary>
        /// 毎フレームの平面検知処理のオンオフ
        /// </summary>
        /// <param name="active"></param>
        public void SetAutomaticDetectionActive(bool active)
        {
            if(active)
            {
                _automaticCenterTrackingDetector =
                    Observable
                        .EveryFixedUpdate()
                        .Subscribe(_ => _automaticCenterTrackingDetectedPose.OnNext(RayCastPose(DETECT_RAY_CENTER)))
                        .AddTo(gameObject);
            }
            else
            {
                _automaticCenterTrackingDetector.Dispose();
            }
        }

        /// <summary>
        /// 画面タップによる平面検知処理のオンオフ
        /// </summary>
        /// <param name="active"></param>
        public void SetManualDetectionActive(bool active)
        {
            if (active)
            {
                _manualTouchTrackingDetector =
                    Observable
                        .EveryFixedUpdate()
                        .Where(_ => Input.touchCount >= 1)
                        .Subscribe(_ => _manualTouchTrackingDetectedPose.OnNext(RayCastPose(Input.GetTouch(0).position)))
                        .AddTo(gameObject);
            }
            else
            {
                _manualTouchTrackingDetector.Dispose();
            }
        }

        public void Dispose()
        {
            _onTouched?.Dispose();
            _automaticCenterTrackingDetectedPose?.Dispose();
            _manualTouchTrackingDetectedPose?.Dispose();
            _automaticCenterTrackingDetector?.Dispose();
            _manualTouchTrackingDetector?.Dispose();
        }

        public void OnDestroy()
        {
            Dispose();
        }

        /// <summary>
        /// タップ位置からのレイキャストで平面検知
        /// </summary>
        /// <returns></returns>
        private Tuple<bool, TrackableHit> TouchPlaneDetect()
        {
            if (Input.touchCount <= 0)
            {
                return new Tuple<bool, TrackableHit>(false, default(TrackableHit));
            }

            Touch touch = Input.GetTouch(0);
            _onTouched.OnNext(Unit.Default);

            return RayCastPose(touch.position);
        }

        /// <summary>
        /// レイを使って平面検知
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private Tuple<bool, TrackableHit> RayCastPose(Vector2 position)
        {

#if UNITY_ANDROID
            TrackableHit hit;
            bool isHit = Frame.Raycast(position.x, position.y, TrackableHitFlags.PlaneWithinPolygon, out hit);

            return new Tuple<bool, TrackableHit>(isHit, hit);
#endif
        }

#if UNITY_IOS
        private Tuple<bool, Pose> RayCastPose(Vector2 position)
        {
            Pose hitPose;
            bool isHit = _iosARHelper.RaycastPlane(ARKitFirstPersonCamera, position.x, position.y, out hitPose);

            return new Tuple<bool, Pose>(isHit, hitPose);
        }
#endif
    }
}
