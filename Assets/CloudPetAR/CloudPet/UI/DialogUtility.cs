using System;
using UniRx.Async;
using CloudPet.Commons;
using UnityEngine;
using UdonLib.UI;
using UdonLib.Commons;

namespace CloudPet.UI
{
    public static class DialogUtility
    {
        public static async UniTask<TDialog> CreateDialog<TDialog>(DialogType dialogType, Action<TDialog> onInitialized = null)
            where TDialog : DialogPresenterBase
        {
            RectTransform parent = UICanvasManager.Instance.DialogRoot;
            TDialog prefab = PrefabLoader.LoadPrefab<TDialog>(dialogType);
            return await DialogManager.Instance.CreateDialog(parent, prefab, onInitialized);
        }
    }
}
