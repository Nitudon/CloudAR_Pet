using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudPet.Common;
using CloudPet.AR;
using UdonLib.Commons;

namespace CloudPet.Pet
{
    /// <summary>
    /// ブリーダーのペット管理周りのユースケース
    /// </summary>
    public class BreederActivatorUseCase
    {
        private CloudAnchorSystem _anchorSystem;

        private BreederModel _model;

        private Transform _breederRoot;

        public BreederActivatorUseCase(BreederModel model)
        {
            _model = model;
        }

        public void ActivateRoot(Vector3 activatePosition)
        {
           if (UserSystemModel.Instance.IsHost)
           {
               _model.OnActivatePet.OnNext(new ActivateInfo(_anchorSystem.AnchorModel.PlacedAnchorRoot.Value.transform.position, activatePosition));
           }
           else
           {
               _model.OnActivatePet.OnNext(new ActivateInfo(_anchorSystem.AnchorModel.ResolvedAnchorInfo.Value.transform.position, activatePosition));
           }
        }

        public void ActivatePet(Vector3 activatePosition)
        {
            var pet = PetPresenter.Create(_breederRoot);
            _model.SetPet(pet);
            pet.SetPosition(activatePosition);
        }
    }
}
