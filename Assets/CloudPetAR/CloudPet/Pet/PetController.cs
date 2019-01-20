using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;

namespace CloudPet.Pet
{
    public class PetController : InitializableMono
    {
        [SerializeField]
        private SimpleAnimation _animationController;

        [SerializeField]
        private Animator _animator;

        public override void Initialize()
        {
            _animationController.Initialize();
            _animationController.SetAnimator(_animator);
            PlayMotion(PetState.Idle);
        }

        public void PlayMotion(PetState state)
        {
            _animationController.PlayAnimation(PetDefine.GetPetMotion(state));
        }
    }
}