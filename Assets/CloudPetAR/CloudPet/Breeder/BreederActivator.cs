using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudPet.Common;
using CloudPet.AR;
using UdonLib.Commons;

namespace CloudPet.Pet
{
    public class BreederActivator : UdonBehaviour
    {
        private CloudAnchorSystem _anchorSystem;

        private Transform _breederRoot;
        private Vector3 _activatePosition;

        private void ActivateRoot()
        {
           _anchorSystem.SetActiveTouchDetector(true);

           if (UserSystemModel.Instance.IsHost)
           {
               
           }
           else
           {
               
           }
        }

        private void ActivatePet()
        {
            var pet = PetPresenter.Create(_breederRoot);
            pet.SetPosition(_activatePosition);
        }
    }
}
