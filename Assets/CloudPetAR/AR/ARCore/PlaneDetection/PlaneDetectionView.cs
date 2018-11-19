using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;

namespace CloudPet.AR
{
    public class PlaneDetectionView : UdonBehaviour
    {
        [SerializeField]
        private GameObject _detectionMarker;

        public void SetMarkerEnable(bool enable)
        {
            _detectionMarker.SetActive(enable);
        }
    }
}
