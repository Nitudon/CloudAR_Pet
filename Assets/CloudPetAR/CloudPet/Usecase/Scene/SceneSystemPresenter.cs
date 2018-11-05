using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            await Task.WhenAll(_asyncInitializers.Select(task => task.Initialize()));
        }
    }
}
