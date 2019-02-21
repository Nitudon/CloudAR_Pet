using System.Collections;
using System.Collections.Generic;
using CloudPet.AR;
using CloudPet.Commons;
using UdonLib.Commons;
using UnityEngine;

namespace CloudPet.Network
{
    public class CloudObject : InitializableMono
    {
        public override void Initialize()
        {

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
