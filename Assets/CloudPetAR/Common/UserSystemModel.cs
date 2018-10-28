using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;

namespace CloudPet.Common
{
    public class UserSystemModel : UdonBehaviourSingleton<UserSystemModel>
    {
        private string _breederId;
        public string BreederId => _breederId;

        public bool IsHost => !PhotonNetwork.isNonMasterClientInRoom;

        protected override void Init()
        {
            base.Init();
            DontDestroyOnLoad(this);
        }

        public void SetUserInfo(string id)
        {
            _breederId = id;
        }
    }
}
