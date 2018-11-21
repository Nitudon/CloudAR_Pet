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

        private ReactiveProperty<Pose> _detectedPose;
        public IReadOnlyReactiveProperty<Pose> DetectedPose => _detectedPose;

        public Subject<Unit> OnTouched { get; private set; }

        private IDisposable _touchDetector;

        public override void Initialize()
        {
            Input.multiTouchEnabled = false;
            OnTouched = new Subject<Unit>();
            _detectedPose = new ReactiveProperty<Pose>();
        }

        public void SetDetectionActive(bool active)
        {
            if(active)
            {
                _touchDetector = Observable.EveryFixedUpdate().Subscribe(_ => RayCastPose()).AddTo(gameObject);
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

        private void RayCastPose()
        {

            if (Input.touchCount <= 0)
            {
                return;
            }

            Touch touch = Input.GetTouch(0);
            OnTouched.OnNext(Unit.Default);

#if UNITY_ANDROID
            TrackableHit hit;
            if (Frame.Raycast(touch.position.x, touch.position.y, TrackableHitFlags.PlaneWithinPolygon, out hit))
            {
                _detectedPose.Value = hit.Pose;
            }
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
