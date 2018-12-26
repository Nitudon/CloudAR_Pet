using UnityEngine;
using UdonLib.UI;
using DG.Tweening;
using UniRx.Async;
using UdonLib.Async;

namespace CloudPet.UI
{
    public class CommonDialogView : DialogViewBase
    {
        private const float ANIMATION_DURATION = 0.3f;

        public override async UniTask OpenDialogAnimation()
        {
            RectTransform.DOKill();
            RectTransform.localScale = Vector3.zero;
            await RectTransform.DOScale(1f, ANIMATION_DURATION);
        }

        public override async UniTask CloseDialogAnimation()
        {
            RectTransform.DOKill();
            await RectTransform.DOScale(0f, ANIMATION_DURATION);
        }
    }
}
