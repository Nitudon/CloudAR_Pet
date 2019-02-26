using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;
using UdonLib.Commons;
using UniRx;

namespace CloudPet.AR
{
    public class PlaneDetectionPresenter : InitializableMono
    {
        [SerializeField]
        private PlaneDetectionView _view;

        [SerializeField]
        private PlaneDetectionGesture _detectionGesture;

        private bool _pointsEnable;

        public override void Initialize()
        {
            SetMarkerEnable(false);
            SetPointsEnable(false);

            Bind();
        }

        public void SetMarkerEnable(bool enable)
        {
            _view.SetMarkerEnable(enable);
        }

        public void SetPointsEnable(bool enable)
        {
            _pointsEnable = enable;
            if (!enable)
            {
                _view.ClearPoints();
            }
        }

        private void Bind()
        {
            _detectionGesture
                .ManualTouchTrackingDetectedPose
                .Where(detect => detect.Item1)
                .Select(detect => detect.Item2.Pose.position)
                .Subscribe(_view.MarkPoint)
                .AddTo(gameObject);

            _detectionGesture
                .AutomaticCenterTrackingDetectedPose
                .Where(_ => _pointsEnable)
                .Subscribe(_ => _view.DrawPoints(Frame.PointCloud.PointCount))
                .AddTo(gameObject);
        }
    }
}
