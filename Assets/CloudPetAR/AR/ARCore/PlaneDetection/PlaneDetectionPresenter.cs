﻿using System;
using System.Collections;
using UnityEngine;
using UdonLib.Commons;
using UniRx;
using GoogleARCore;

namespace CloudPet.AR
{
    public class PlaneDetectionPresenter : InitializableMono, IDisposable
    {
        [SerializeField]
        private PlaneDetectionView view;

        private ReactiveProperty<Pose> _detectedPose;
        public IReadOnlyReactiveProperty<Pose> DetectedPose => _detectedPose;

        private IDisposable _touchDetector;

        public override void Initialize()
        {
            Input.multiTouchEnabled = false;
            _detectedPose = new ReactiveProperty<Pose>();
        }

        public void SetDetectionActive(bool active)
        {
            if(active)
            {
                _touchDetector = MainThreadDispatcher.StartFixedUpdateMicroCoroutine(TouchPlaceDetectEnumerator());
            }
            else
            {
                _touchDetector.Dispose();
            }
        }

        public void Dispose()
        {
            _detectedPose.Dispose();
            _touchDetector.Dispose();
        }

        private IEnumerator TouchPlaceDetectEnumerator()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                RayCastPose(touch.position.x, touch.position.y);
            }

            yield return null;
        }

        private void RayCastPose(float x, float y)
        {
#if UNITY_ANDROID
            TrackableHit hit;
            if (Frame.Raycast(x, y, TrackableHitFlags.PlaneWithinPolygon, out hit))
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
