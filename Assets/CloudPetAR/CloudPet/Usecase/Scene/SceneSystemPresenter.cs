using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UniRx.Async;

namespace UdonLib.Commons
{
    public class SceneSystemPresenter : UdonBehaviour
    {
        [SerializeField]
        private InitializableMono[] _initializers;

        [SerializeField]
        private AsyncInitializableMono[] _asyncInitializers;

        protected override async void Start()
        {
            base.Start();
            _initializers.ForEach(x => x.Initialize());
            await UniTask.WhenAll(_asyncInitializers.Select(task => task.Initialize()));
        }
    }
}
