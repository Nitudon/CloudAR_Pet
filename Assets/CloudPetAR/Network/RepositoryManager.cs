using UdonLib.Commons;

namespace CloudPet.Network
{
    public class RepositoryManager : UdonBehaviourSingleton<RepositoryManager>
    {
        protected override bool IsDontDestroy => true;

        private RoomRepository _roomRepository;
        public RoomRepository RoomRepository => _roomRepository;

        private IRepository[] _repositoryList;

        public override void Initialize()
        {
            base.Initialize();

            _roomRepository = new RoomRepository();

            _repositoryList = new IRepository[]
            {
                _roomRepository
            };
        }

        public void BindAll()
        {
            _repositoryList.ForEach(repository => repository.Bind());
        }
    }
}
