using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;
using UniRx;

namespace CloudPet.Pet
{
    public class BreederModel
    {
        private string _id;
        public string Id => _id;

        private string _name;
        public string Name => _name;

        private PetPresenter _petPresenter;
        public PetPresenter PetPresenter => _petPresenter;

        public PetInfo OwnPet => _petPresenter.Model.Info;

        public BreederModel(string id, string name)
        {
            _id = id;
            _name = name;
        }

        public void SetPet(PetPresenter pet)
        {
            _petPresenter = pet;
        }
    }
}
