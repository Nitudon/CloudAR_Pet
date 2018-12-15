using UnityEngine;
using UdonLib.Commons;

namespace CloudPet.UI
{
    public class UICanvasManager : UdonBehaviourSingleton<UICanvasManager>
    {
        [SerializeField]
        private RectTransform _uiRoot;
        public RectTransform UIRoot => _uiRoot;

        [SerializeField]
        private RectTransform _dialogRoot;
        public RectTransform DialogRoot => _dialogRoot;

        [SerializeField]
        private Canvas _uiCanvas;
    }
}
