using UniRx;

namespace CloudPet.Pet
{
    public class PetModel
    {
        private ReactiveProperty<PetInfo> _info = new ReactiveProperty<PetInfo>();
        public IReadOnlyReactiveProperty<PetInfo> Info => _info;

        private ReactiveProperty<PetState> _state = new ReactiveProperty<PetState>(PetState.Idle);
        public IReadOnlyReactiveProperty<PetState> State => _state;

        public void SetInfo(PetInfo info)
        {
            _info.Value = info;
        }

        public void SetState(PetState state)
        {
            _state.Value = state;
        }
    }
}
