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
        public PetModel Model => _model;

        public override void Initialize()
        {
            Bind();
        }

        private void Bind()
        {

        }

        public static PetPresenter Create(Transform root, Vector3 localPosition = new Vector3(), Vector3 localEulerAngle = new Vector3())
        {
            var instance = Instantiate(Resources.Load<PetPresenter>(PetDefine.PET_PREFAB_PATH));
            instance.SetLocalPosition(localPosition);
            instance.SetLocalEulerAngles(localEulerAngle);

            return instance;
        }
    }
}
