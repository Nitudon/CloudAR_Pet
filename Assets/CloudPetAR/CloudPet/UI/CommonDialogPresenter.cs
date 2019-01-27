using UnityEngine;
using UdonLib.Commons;
using UdonLib.UI;
using UniRx.Async;

namespace CloudPet.UI
{
    public class CommonDialogPresenter<TView, TResult> : DialogPresenterBase<TView, TResult> where TView : CommonDialogView
    {

    }
}
