using System;
using System.Collections;
using System.Collections.Generic;
using CloudPet.Commons;
using GoogleARCore;
using UnityEngine;
using UdonLib.Commons;

namespace CloudPet.AR
{
    public class PlaneDetectionView : UdonBehaviour
    {

        [SerializeField]
        private MeshFilter _detectionPointsMesh;

        private DetectionMarkerView _detectionMarker;

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

        public void MarkPoint(bool hit, Vector3 position)
        {
            if (!hit)
            {
                _detectionMarker.SetActive(false);
                return;
            }

            _detectionMarker.SetActive(true);
            if (_detectionMarker == null)
            {
                _detectionMarker = Instantiate(Resources.Load<DetectionMarkerView>(ResourceDefine.DETECTION_MARKER_PATH));
            }
            _detectionMarker.transform.position = position;
        }
    }
}
