using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;
using UdonLib.Commons;

namespace CloudPet.AR
{
    public class PlaneDetectionView : UdonBehaviour
    {
        [SerializeField]
        private GameObject _detectionMarker;

        [SerializeField]
        private Mesh _detectionPointsMesh;

        public void SetMarkerEnable(bool enable)
        {
            _detectionMarker.SetActive(enable);
        }

        public void DrawPoints(int count, Vector3[] points)
        {
            int[] indices = new int[count];
            for (int i = 0; i < count; i++)
            {
                indices[i] = i;
            }

            _detectionPointsMesh.Clear();
            _detectionPointsMesh.vertices = points;
            _detectionPointsMesh.SetIndices(indices, MeshTopology.Points, 0);
        }

        public void MarkPoint()
        {

        }
    }
}
