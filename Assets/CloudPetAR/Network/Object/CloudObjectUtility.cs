using UnityEngine;

namespace CloudPet.Network
{
    public static class CloudObjectUtility
    {
        public static TObject Instantiate<TObject>(string prefabPath, CloudTransformInfo info)
            where TObject : CloudObject
        {
            TObject instance = PhotonNetwork.Instantiate(prefabPath, info.Position, Quaternion.LookRotation(info.Forward), 0) as TObject;
            instance.MineTranslate(info);
        }
    }
}
