using System;
using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CenterRayPlaneHandler : MonoBehaviour
{
    [SerializeField]
    private Camera _firstPersonCamera;

    private ReactiveProperty<Vector3> _markerPosition;
    public IReadOnlyReactiveProperty<Vector3> MarkerPosition;

    private IDisposable _markerDisposable;

    private Touch _inputTouch;

    public void Initialize()
    {
        _markerDisposable =
            this
                .UpdateAsObservable()
                .Subscribe(_ => RaycastPlaneHit())
                .AddTo(gameObject);
    }

    public void Dispose()
    {
        _markerPosition.Dispose();
    }

    private void RaycastPlaneHit()
    {
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast(_inputTouch.position.x, _inputTouch.position.y, raycastFilter, out hit))
        {
            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if ((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(_firstPersonCamera.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                _markerPosition.Value = hit.Pose.position;
            }
        }
    }
}
