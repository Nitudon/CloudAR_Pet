using System.Collections;
using System.Collections.Generic;
using CloudPet.Commons;
using UdonLib.Commons;
using UnityEngine;

namespace CloudPet.Network
{
    public class CloudObject : UdonBehaviour
    {
        [PunRPC]
        public void RPCTranslate(CloudTransformInfo info)
        {
            transform.position = AnchorPositionUtility.GetAnchorPointFromWorldPoint(Manager, info.Position);
        }
    }

}
