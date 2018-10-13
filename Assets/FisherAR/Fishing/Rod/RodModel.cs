using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using FisherAR.Common;

namespace FisherAR.InGame
{
    public class RodModel
    {
        private ReactiveProperty<RodMode> _rodMode = new ReactiveProperty<RodMode>();
        public IReadOnlyReactiveProperty<RodMode> RodMode => _rodMode;
    }
}
