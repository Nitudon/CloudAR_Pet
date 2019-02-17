namespace CloudPet.AR
{
    using System;
    using System.Collections;
    using GoogleARCore;
    using GoogleARCore.CrossPlatform;
    using GoogleARCore.Examples.CloudAnchor;
    using UnityEngine;
    using UdonLib.Commons;
    using UniRx;

    /// <summary>
    /// Controller for the Cloud Anchor Example.
    /// </summary>
    public class CloudAnchorManager : UdonBehaviourSingleton<CloudAnchorManager>
    {
        private CloudAnchorModel _anchorModel = new CloudAnchorModel();
        public CloudAnchorModel AnchorModel => _anchorModel;

        public Pose CurrentAnchor => _anchorModel.CurrentAnchor;

#if UNITY_ANDROID
        [Header("ARCore")]

        [SerializeField]
        private Transform ARCoreRoot;
#endif
        private bool _isHost;

        public override void Initialize()
        {
            ResetStatus();

            MainThreadDispatcher.StartUpdateMicroCoroutine(UpdateEnumerator());
        }

        private IEnumerator UpdateEnumerator()
        {
            UpdateApplicationLifecycle();
            yield return null;
        }

        public void SetHostMode()
        {
            if (_anchorModel.CloudMode == ApplicationMode.Hosting)
            {
                _anchorModel.SetMode(ApplicationMode.Ready);
                ResetStatus();
                return;
            }

            _anchorModel.SetMode(ApplicationMode.Hosting);
        }

        /// <summary>
        /// Handles a user intent to enter a mode where they can input an anchor to be resolved or exit this mode if
        /// already in it.
        /// </summary>
        public void SetResolverMode()
        {
            if (_anchorModel.CloudMode == ApplicationMode.Resolving)
            {
                _anchorModel.SetMode(ApplicationMode.Ready);
                ResetStatus();
                return;
            }

            _anchorModel.SetMode(ApplicationMode.Resolving);
        }

        public void PlaceAnchor()
        {

        }

        /// <summary>
        /// Hosts the user placed cloud anchor and associates the resulting Id with the current room.
        /// </summary>
        public void HostLastPlacedAnchor()
        {
#if !UNITY_IOS
            var anchor = (Anchor)_anchorModel.PlacedAnchorRoot.Value;
#else
            var anchor = (UnityEngine.XR.iOS.UnityARUserAnchorComponent)_anchorModel.PlacedAnchorRoot.Value;
#endif
            XPSession.CreateCloudAnchor(anchor).ThenAction(result =>
            {
                if (result.Response != CloudServiceResponse.Success)
                {
                    InstantLog.StringLogError("Failed to host anchor");
                    return;
                }

                _anchorModel.SetPlacedAnchorRoot(true, result.Anchor);
            });
        }

        public void ResolveAnchorFromId(string cloudAnchorId)
        {
            XPSession.ResolveCloudAnchor(cloudAnchorId).ThenAction((System.Action<CloudAnchorResult>)(result =>
            {
                if (result.Response != CloudServiceResponse.Success)
                {
                    InstantLog.StringLogError("Failed to host anchor");
                    return;
                }

                _anchorModel.SetResolvedAnchorInfo(result.Anchor);
            }));
        }

        private void ResetStatus()
        {
            // Reset internal status.
            _anchorModel.SetMode(ApplicationMode.Ready);
            if (_anchorModel.PlacedAnchorRoot.Value != null)
            {
                Destroy(_anchorModel.PlacedAnchorRoot.Value.gameObject);
            }

            _anchorModel.SetPlacedAnchorRoot(false, null);
            if (_anchorModel.ResolvedAnchorInfo.Value != null)
            {
                Destroy(_anchorModel.ResolvedAnchorInfo.Value.gameObject);
            }

            _anchorModel.SetResolvedAnchorInfo(null);
        }

        /// <summary>
        /// Check and update the application lifecycle.
        /// </summary>
        private void UpdateApplicationLifecycle()
        {
            var sleepTimeout = SleepTimeout.NeverSleep;

#if !UNITY_IOS
            // Only allow the screen to sleep when not tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                const int lostTrackingSleepTimeout = 15;
                sleepTimeout = lostTrackingSleepTimeout;
            }
#endif

            Screen.sleepTimeout = sleepTimeout;

            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                ShowAndroidToastMessage("Camera permission is needed to run this application.");
            }
            else if (Session.Status.IsError())
            {
                ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
            }
        }

        /// <summary>
        /// Show an Android toast message.
        /// </summary>
        /// <param name="message">Message string to show in the toast.</param>
        private void ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                        message, 0);
                    toastObject.Call("show");
                }));
            }
        }
    }
}
