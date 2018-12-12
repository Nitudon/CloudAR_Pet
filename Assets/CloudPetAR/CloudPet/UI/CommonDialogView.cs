using System;
using UnityEngine;
using UdonLib.UI;
using DG.Tweening;

namespace CloudPet.UI
{
    public class CommonDialogView : DialogViewBase
    {
        private const float ANIMATION_DURATION = 0.5f;

        public override void OpenDialogAnimation(Action onComplete = null)
        {
            RectTransform.DOKill();
            RectTransform.localScale = Vector3.zero;
            RectTransform.DOScale(1f, ANIMATION_DURATION).OnComplete(() => onComplete?.Invoke());
        }

        public override void CloseDialogAnimation(Action onComplete = null)
        {
            RectTransform.DOKill();
            RectTransform.DOScale(0f, ANIMATION_DURATION).OnComplete(() => onComplete?.Invoke());
        }
    }
}
