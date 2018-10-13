using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using FisherAR.Common;

namespace FisherAR.InGame
{
    public class RodModel
    {
        private static readonly float ROD_DISTANCE_MAX = 50f;

        private ReactiveProperty<RodMode> _rodMode = new ReactiveProperty<RodMode>();
        public IReadOnlyReactiveProperty<RodMode> RodMode => _rodMode;
    }
}
