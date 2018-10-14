using System;
using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TouchPlaneHandler : MonoBehaviour 
{
    [SerializeField]
    private Camera _firstPersonCamera;

    public Action OnTouchPlaneEvent;

    private Touch _inputTouch;
	
    public void Initialize()
    {
        this
            .UpdateAsObservable()
            .Where(_ => Input.touchCount < 1 || (_inputTouch = Input.GetTouch(0)).phase != TouchPhase.Began)
            .Where(_ => OnTouchPlaneEvent != null && RaycastPlaneHit())
            .Subscribe(_ => OnTouchPlaneEvent.Invoke())
            .AddTo(gameObject);
    }

    private bool RaycastPlaneHit()
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
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }
}
