﻿namespace CloudPet.AR
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
    public class CloudAnchorSystem : InitializableMono
    {
        private CloudAnchorModel _anchorModel = new CloudAnchorModel();
        public CloudAnchorModel AnchorModel => _anchorModel;

        [SerializeField]
        private CloudAnchorUIController UIController;

#if UNITY_ANDROID
        [Header("ARCore")]

        [SerializeField]
        private Transform ARCoreRoot;
#endif
        private bool _isHost;

        private const string LOOK_BACK_IP = "127.0.0.1";
        private const float OBJECT_ROTATION_OFFSET = 180.0f;

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
            UIController.ShowHostingModeBegin();
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
            UIController.ShowResolvingModeBegin();
        }

        /// <summary>
        /// Handles the user intent to resolve the cloud anchor associated with a room they have typed into the UI.
        /// </summary>
        public void OnResolveRoomClick()
        {
            var roomToResolve = UIController.GetRoomInputValue();
            if (roomToResolve == 0)
            {
                UIController.ShowResolvingModeBegin("Invalid room code.");
                return;
            }

            UIController.ShowResolvingModeAttemptingResolve();
            // Resolve Anchor 処理
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
            UIController.ShowHostingModeAttemptingHost();
            XPSession.CreateCloudAnchor(anchor).ThenAction(result =>
            {
                if (result.Response != CloudServiceResponse.Success)
                {
                    UIController.ShowHostingModeBegin(
                        string.Format("Failed to host cloud anchor: {0}", result.Response));
                    return;
                }

                _anchorModel.SetPlacedAnchorRoot(true, result.Anchor);
                UIController.ShowHostingModeBegin("Cloud anchor was created and saved.");
            });
        }

        public void ResolveAnchorFromId(string cloudAnchorId)
        {
            XPSession.ResolveCloudAnchor(cloudAnchorId).ThenAction((System.Action<CloudAnchorResult>)(result =>
            {
                if (result.Response != CloudServiceResponse.Success)
                {
                    UIController.ShowResolvingModeBegin(string.Format("Resolving Error: {0}.", result.Response));
                    return;
                }

                _anchorModel.SetResolvedAnchorInfo(result.Anchor);
                UIController.ShowResolvingModeSuccess();
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
            UIController.ShowReadyMode();
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
