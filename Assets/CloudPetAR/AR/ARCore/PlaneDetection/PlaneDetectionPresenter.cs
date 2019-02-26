using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;

namespace CloudPet.AR
{
    public class PlaneDetectionPresenter : InitializableMono
    {
        [SerializeField]
        private PlaneDetectionView _view;

        public override void Initialize()
        {
            _view.SetMarkerEnable(false);
        }
    }
}
