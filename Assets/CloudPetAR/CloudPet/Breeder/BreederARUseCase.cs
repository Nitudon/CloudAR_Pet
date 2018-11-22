using UniRx;
using UdonLib.Commons;
using CloudPet.AR;
using UnityEngine;

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

        public IReadOnlyReactiveProperty<ActivateInfo> ActivateInfoEveryChanged
        {
            get
            {
                return _planeDetectionGesture
                        .DetectedPose
                        .Where(_ => !_planeDetectionGesture.IsDestroyed)
                        .Select(info => new ActivateInfo(info.Item1, _anchorModel.CurrentAnchorPosition, info.Item2.position, info.Item2.rotation))
                        .ToReactiveProperty();
            }
        }
    }
}
