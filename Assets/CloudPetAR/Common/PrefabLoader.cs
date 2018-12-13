using System;
using UnityEngine;
using UdonLib.Commons;
using UdonLib.Commons.Extensions;

public static class PrefabLoader
{
    public static T LoadPrefab<T>(DialogType type) where T : UnityEngine.Object
    {
        string path = GetDialogPrefabPath(type);
        if (path.IsNullOrWhiteSpace())
        {
            Debug.LogError($"Missing Dialog Prefab : Type Of {typeof(T)}");
            return default(T);
        }
        return Resources.Load<T>(GetDialogPrefabPath(type));
    }

    #region Dialog

    public enum DialogType
    {
        // 部屋名入力ダイアログ
        RoomDialog,
    }

    private static string GetDialogPrefabPath(DialogType type)
    {
        switch (type)
        {
            case DialogType.RoomDialog:
                return "RoomDialog";

            default:
                return string.Empty;
        }
    }

    #endregion
}
