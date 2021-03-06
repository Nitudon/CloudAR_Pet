﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;

namespace CloudPet.Commons
{
    public class UserSystemModel : UdonBehaviourSingleton<UserSystemModel>
    {
        private string _breederId;
        public string BreederId => _breederId;

        public bool IsHost => !PhotonNetwork.isNonMasterClientInRoom;

        public override void Initialize()
        {
            base.Initialize();
            DontDestroyOnLoad(this);
        }

        public void SetUserInfo(string id)
        {
            _breederId = id;
        }
    }
}
