using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CloudPet.Pet
{
    public class BreederActivateSequence
    {
        private Transform _breederRoot;
        private Vector3 _activatePosition;

        public BreederActivateSequence()
        {

        }

        private void ActivateRoot()
        {
           if ()
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
