using UnityEngine;
using UdonLib.Commons;
using UdonLib.UI;

namespace CloudPet.UI
{
    public class CommonDialogPresenter<TView, TResult> : DialogPresenterBase<TView, TResult> where TView : CommonDialogView
    {
        [SerializeField]
        private HitRectArea _blocking;
    }
}
