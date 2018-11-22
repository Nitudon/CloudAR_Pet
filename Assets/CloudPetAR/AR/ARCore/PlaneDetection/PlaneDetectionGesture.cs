using System;
using System.Collections;
using UnityEngine;
using UdonLib.Commons;
using UniRx;
using GoogleARCore;

namespace CloudPet.AR
{
    /// <summary>
    /// 平面検出タッチハンドラ
    /// </summary>
    public class PlaneDetectionGesture : InitializableMono, IDisposable
    {
        [SerializeField]
        private PlaneDetectionView view;

        private static readonly Vector2 DETECT_RAY_CENTER = Vector2.zero;

        private ReactiveProperty<Tuple<bool, Pose>> _detectedPose;
        public IReadOnlyReactiveProperty<Tuple<bool, Pose>> DetectedPose => _detectedPose;

        public Subject<Unit> OnTouched { get; private set; }

        private IDisposable _touchDetector;

        public override void Initialize()
        {
            Input.multiTouchEnabled = false;
            OnTouched = new Subject<Unit>();
            _detectedPose = new ReactiveProperty<Tuple<bool, Pose>>();
        }

        public void SetDetectionActive(bool active)
        {
            if(active)
            {
                _touchDetector = Observable.EveryFixedUpdate().Subscribe(_ => _detectedPose.Value = RayCastPose(DETECT_RAY_CENTER)).AddTo(gameObject);
            }
            else
            {
                _touchDetector.Dispose();
            }
        }

        public void Dispose()
        {
            _detectedPose.Dispose();
            OnTouched.Dispose();
            _touchDetector.Dispose();
        }

        public void OnDestroy()
        {
            Dispose();
        }

        private Tuple<bool, Pose> TouchPlaneDetect()
        {
            if (Input.touchCount <= 0)
            {
                return default(Tuple<bool, Pose>);
            }

            Touch touch = Input.GetTouch(0);
            OnTouched.OnNext(Unit.Default);

            return RayCastPose(touch.position);
        }

        private Tuple<bool, Pose> RayCastPose(Vector2 position)
        {

#if UNITY_ANDROID
            TrackableHit hit;
            bool isHit = Frame.Raycast(position.x, position.y, TrackableHitFlags.PlaneWithinPolygon, out hit);

            return new Tuple<bool, Pose>(isHit, hit.Pose);
#endif

#if UNITY_IOS
            Pose hitPose;
            if (m_ARKit.RaycastPlane(ARKitFirstPersonCamera, touch.position.x, touch.position.y, out hitPose))
            {
                _anchorModel.SetPlacedAnchorRoot(m_ARKit.CreateAnchor(hitPose));
            }
#endif
        }
    }
}
