using UniRx;
using UdonLib.Commons;

namespace CloudPet.Pet
{
    public class BreederPresenter : InitializableMono
    {
        private BreederModel _model;
        public BreederModel Model => _model;

        private BreederActivator _activator;

        public override void Initialize()
        {
            _model = new BreederModel();

            Bind();
        }

        private void Bind()
        {
            _model
                .OnActivatePet
                .Subscribe(info => _activator.ActivatePet)
                .AddTo(gameObject);
        }
    }
}
