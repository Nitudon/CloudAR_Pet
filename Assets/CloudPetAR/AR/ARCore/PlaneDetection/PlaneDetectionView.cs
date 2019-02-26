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
        private MeshFilter _detectionPointsMesh;

        public void SetMarkerEnable(bool enable)
        {
            _detectionMarker.SetActive(enable);
        }

        public void DrawPoints(int count)
        {
            Vector3[] points = new Vector3[count];
            for (int i = 0; i < count; i++)
            {
                points[i] = Frame.PointCloud.GetPointAsStruct(i);
            }

            int[] indices = new int[count];
            for (int i = 0; i < count; i++)
            {
                indices[i] = i;
            }

            _detectionPointsMesh.mesh.Clear();
            _detectionPointsMesh.mesh.vertices = points;
            _detectionPointsMesh.mesh.SetIndices(indices, MeshTopology.Points, 0);
        }

        public void ClearPoints()
        {
            _detectionPointsMesh.mesh.Clear();
        }

        public void MarkPoint(Vector3 position)
        {
            _detectionMarker.transform.position = position;
        }
    }
}
