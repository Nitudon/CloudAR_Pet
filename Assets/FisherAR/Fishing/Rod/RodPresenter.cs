using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;
using UniRx;

namespace FisherAR.InGame
{
    public class RodPresenter : InitializableMono
    {
        private RodModel _model;

        [SerializeField]
        private RodController _rodController;

        [SerializeField]
        private GyroDetector _gyroDetector;

        [SerializeField]
        private TapDetector _tapDetector;

        public override void Initialize()
        {
            _model = new RodModel();
            
            Bind();
        }

        private void Bind()
        {
            _gyroDetector
                .InputGyroInfo
                .Subscribe(info => _rodController.SetLocalEulerAngles(info.DeltaAngle.Round()))
                .AddTo(gameObject)
                .AddTo(_rodController);

            _tapDetector
                .TapEvent
                .Subscribe(_ => _rodController.CastRod())
                .AddTo(gameObject)
                .AddTo(_rodController);
        }
    }
}
