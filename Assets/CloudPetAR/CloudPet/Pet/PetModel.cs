using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;
using UniRx;

namespace CloudPet.Pet
{
    public class PetModel
    {
        private ReactiveProperty<PetState> _state = new ReactiveProperty<PetState>(PetState.Idle);
        public IReadOnlyReactiveProperty<PetState> State => _state;
    }
}
