using System.Collections;
using UnityEngine;
using UdonLib.Commons;
using UniRx;

namespace CloudPet.AR
{
    public class PlaneDetectionPresenter : InitializableMono
    {
        [SerializeField]
        private PlaneDetectionView view;

        private ReactiveProperty<Vector3> _detectedPositon;
        public IReadOnlyReactiveProperty<Vector3> DetectedPosition => _detectedPositon;

        public override void Initialize()
        {
            
        }

        private IEnumerator PlaneDetectionEnumerator()
        {

        }
    }
}
