using System.Collections;
using System.Collections.Generic;
using CloudPet.Network;
using UnityEngine;
using UdonLib.Commons;
using UniRx;

namespace CloudPet.Pet
{
    /// <summary>
    /// ペットのロジック
    /// </summary>
    public class PetPresenter : CloudObject
    {

        [SerializeField]
        private PetController _petController;

        private PetModel _model;
        public PetModel Model => _model;

        public override void Initialize()
        {
            _model = new PetModel();
            _petController.Initialize();
            Bind();
        }

        private void Bind()
        {
            _model
                .State
                .Where(state => state != PetState.None)
                .Subscribe(_petController.PlayMotion)
                .AddTo(gameObject);
        }

        public static PetPresenter Create(Transform root, Vector3 localPosition = new Vector3(), Vector3 localEulerAngle = new Vector3())
        {
            var instance = Instantiate(Resources.Load<PetPresenter>(PetDefine.PET_PREFAB_PATH));
            instance.SetLocalPosition(localPosition);
            instance.SetLocalEulerAngles(localEulerAngle);
            instance.Initialize();

            return instance;
        }

        #region RPC Method

        [PunRPC]
        public void RPCPlayMotion(PetState state)
        {
            _model.SetState(state);
        }

        #endregion
    }
}
