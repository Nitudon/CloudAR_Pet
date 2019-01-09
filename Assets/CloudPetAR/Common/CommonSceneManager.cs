using UdonLib.Commons;
using UdonLib.UI;
using UnityEngine;
using UniRx.Async;

namespace CloudPet.Commons
{
    public class CommonSceneManager : SceneManager<CommonSceneManager>
    {
        [SerializeField]
        private FadeUIGroup _shadowFade;

        // TODO とりあえずプロジェクト側でユニークにasync/await加工する、あとでSceneManager側を修正
        public async UniTask SceneLoadAsync(string scene)
        {
            await ShowLoading();
            await new WaitWhile(() => IsLoading);
            await HideLoading();
        }

        private async UniTask ShowLoading()
        {
            await _shadowFade.FadeGroupAsync(true);
        }

        private async UniTask HideLoading()
        {
            await _shadowFade.FadeGroupAsync(false);
        }
    }
}
