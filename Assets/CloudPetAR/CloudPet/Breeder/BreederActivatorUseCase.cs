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
        public void ActivatePet(Transform petRoot, ActivateInfo info)
        {
            var pet = PetPresenter.Create(petRoot);

            pet.SetPosition(info.AnchoredWorldPosition);
        }
    }
}
