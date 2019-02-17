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
        private readonly BreederModel _model;

        public BreederActivatorUseCase(BreederModel model)
        {
            _model = model;
        }

        /// <summary>
        /// ペットを呼ぶ
        /// </summary>
        /// <param name="petRoot">ペットをぶらさげるRoot</param>
        /// <param name="info">ペットを呼んだ際の通知用info</param>
        public PetPresenter ActivatePet(Transform petRoot, Vector3 position, Vector3 forward)
        {
            var pet = PetPresenter.Create(petRoot);

            pet.SetPosition(position);
            pet.transform.LookAt(forward);

            _model.SetMode(UIMode.Breed);
            return pet;
        }
    }
}
