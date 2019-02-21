using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CloudPet.AR;
using CloudPet.Commons;
using UdonLib.Commons;
using UniRx;
using UniRx.InternalUtil;
using UniRx.Triggers;
using UnityEngine;

namespace CloudPet.Network
{
    public class CloudObject : InitializableMono
    {
        [SerializeField]
        protected PhotonView _photonView;

        public bool IsMine => _photonView.isMine;

        private IDisposable _cloudTranslateDisposable;

        public override void Initialize()
        {
            if (IsMine)
            {
                _cloudTranslateDisposable =
                    this
                        .FixedUpdateAsObservable()
                        .Subscribe(_ => UpdateOtherPosition())
                        .AddTo(gameObject);
            }
        }

        public void OtherTranslate(CloudTransformInfo info)
        {
            if (!IsMine)
            {
                return;
            }

            var translateInfo =
                new CloudTransformInfo(AnchorPositionUtility.GetWorldPointFromAnchorPoint(CloudAnchorManager.Instance.AnchorModel.CurrentAnchor, info.Position),
                    AnchorPositionUtility.GetWorldPointFromAnchorPoint(CloudAnchorManager.Instance.AnchorModel.CurrentAnchor, info.Forward));

            _photonView.RPC(RPCDefine.ObjectRPC.GetRPCMethod(RPCDefine.ObjectRPC.RPCEnum.Translate), PhotonTargets.Others, translateInfo);
        }

        private void UpdateOtherPosition()
        {
            OtherTranslate(new CloudTransformInfo(transform.position, transform.forward));
        }

        #region RPC Methods
        [PunRPC]
        public void RPCTranslate(CloudTransformInfo info)
        {
            transform.position = AnchorPositionUtility.GetAnchorPointFromWorldPoint(CloudAnchorManager.Instance.AnchorModel.CurrentAnchor, info.Position);
            transform.LookAt(AnchorPositionUtility.GetAnchorPointFromWorldPoint(CloudAnchorManager.Instance.AnchorModel.CurrentAnchor, info.Forward));
        }
        #endregion
    }

}
