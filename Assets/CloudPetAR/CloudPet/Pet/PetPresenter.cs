using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;

namespace CloudPet.Pet
{
    public class PetPresenter : InitializableMono
    {
        [SerializeField]
        private PetController _petController;

        private PetModel _model;

        public override void Initialize()
        {
            Bind();
        }

        private void Bind()
        {

        }
    }
}
