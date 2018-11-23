using UniRx;
using UdonLib.Commons;
using UnityEngine;
using CloudPet.AR;

namespace CloudPet.Pet
{
    /// <summary>
    /// ブリーダーの各ロジックのバインド
    /// </summary>
    public class BreederPresenter : InitializableMono
    {
        [SerializeField]
        private BreederUIView _view;

        [SerializeField]
        private PlaneDetectionGesture _planeDetectionGesture;

        [SerializeField]
        private CloudAnchorSystem _cloudAnchorSystem;
        
        private BreederModel _model;
        public BreederModel Model => _model;
        
        private BreederActivatorUseCase _activatorUseCase;
        private BreederARUseCase _arUseCase;

        public override void Initialize()
        {
            //_model = new BreederModel();

            _activatorUseCase = new BreederActivatorUseCase(_model, _cloudAnchorSystem);
            _arUseCase = new BreederARUseCase(_planeDetectionGesture, _cloudAnchorSystem.AnchorModel);

            Bind();
        }

        private void Bind()
        {
            _model
                .OnActivatePet
                .Subscribe(info => _activatorUseCase.ActivatePet(info.Position))
                .AddTo(gameObject);
        }
    }
}
