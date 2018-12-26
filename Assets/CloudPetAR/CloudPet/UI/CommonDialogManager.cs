using System;
using System.Collections.Generic;
using System.Linq;
using UdonLib.UI;
using UnityEngine;
using UniRx.Async;

namespace CloudPet.UI
{
    public class CommonDialogManager : DialogManager<CommonDialogManager>
    {
        [SerializeField]
        private GameObject _blocking;

        private Dictionary<string, DialogPresenterBase> _dialogs;
        public List<DialogPresenterBase> DialogList => _dialogs.Values.ToList();

        public override void Initialize()
        {
            base.Initialize();
            _dialogs = new Dictionary<string, DialogPresenterBase>();
        }

        public async UniTask<TDialog> CreateDialog<TDialog>(TDialog prefab, Action<TDialog> onInitialized = null)
            where TDialog : DialogPresenterBase
        {
            _blocking.SetActive(true);
            TDialog dialog = await base.CreateDialog<TDialog>(UICanvasManager.Instance.DialogRoot, prefab, onInitialized);
            dialog.RectTransform.SetAsLastSibling();
            _dialogs.Add(dialog.Id, dialog);
            dialog.onCloseCallback += () => OnDialogClose(dialog.Id);
            return dialog;
        }

        private void OnDialogClose(string id)
        {
            if (_dialogs.ContainsKey(id))
            {
                _dialogs.Remove(id);
            }

            if (_dialogs.Count == 0)
            {
                _blocking.SetActive(false);
            }
        }
    }
}
