using UniRx;
using UdonLib.Commons;
using CloudPet.AR;

namespace CloudPet.Pet
{
    public class BreederARUseCase : InitializableMono
    {
        private PlaneDetectionGesture _planeDetectionGesture;

        public override void Initialize()
        {
            //_model = new BreederModel();

            Bind();
        }

        private void Bind()
        {

        }
    }
}
