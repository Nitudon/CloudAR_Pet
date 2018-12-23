using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudPet.AR;

namespace CloudPet.Pet
{
    /// <summary>
    /// ブリーダーのペット管理周りのユースケース
    /// </summary>
    public class BreederActivatorUseCase
    {
        private CloudAnchorSystem _anchorSystem;

        private readonly BreederModel _model;

        public BreederActivatorUseCase(BreederModel model, CloudAnchorSystem cloudAnchorSystem)
        {
            _model = model;
            _anchorSystem = cloudAnchorSystem;
        }

        public void ActivatePet(Transform petRoot, ActivateInfo info)
        {
            var pet = PetPresenter.Create(petRoot);
            _model.SetPet(pet);

            pet.SetPosition(info.AnchoredWorldPosition);
        }
    }
}
