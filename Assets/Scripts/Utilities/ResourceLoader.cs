using System;
using UnityEngine;

/// <summary>
/// 各種リソースの読み込み機構
/// </summary>
namespace CloudPet.Common.Resource
{
    public static class WorldObjectLoader
    {
        public static IPrefabLoader Loader;

        public static T Load<T>(WorldObjectType type, Action<T> initialize = null) where T : UnityEngine.Object
        {
            var path = GetPrefabPath(type);
            var obj = Resources.Load<T>(path);
            initialize?.Invoke(obj);

            return obj;
        }

        private static string GetPrefabPath(WorldObjectType type)
        {
            
        }
    }

    public enum WorldObjectType
    {
        // プレイヤーキャラ
        PlayerCharacter,
    }

    public interface IPrefabLoader
    {
        T Load<T>() where T : Object;
    }
}