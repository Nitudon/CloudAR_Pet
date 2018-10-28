using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;
using UniRx;

namespace CloudPet.Pet
{
    public class PetModel
    {
        private PetInfo _info;
        public PetInfo Info => _info;

        private ReactiveProperty<PetState> _state = new ReactiveProperty<PetState>(PetState.Idle);
        public IReadOnlyReactiveProperty<PetState> State => _state;

        public PetModel(PetInfo info)
        {
            _info = info;
        }

        public void SetState(PetState state)
        {
            _state.Value = state;
        }
    }
}
