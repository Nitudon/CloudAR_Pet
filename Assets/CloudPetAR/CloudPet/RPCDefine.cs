namespace CloudPet.Commons
{
    /// <summary>
    /// Photon同期用RPCの定義
    /// </summary>
    public static class RPCDefine
    {
        /// <summary>
        /// 同期オブジェクト全般のRPC
        /// </summary>
        public static class ObjectRPC
        {
            /// <summary>
            /// オブジェクトの同期メソッド
            /// </summary>
            public enum RPCEnum
            {
                // 位置同期
                Translate,
            }

            /// <summary>
            /// ブリーダーの同期メソッド名リスト
            /// </summary>
            public static readonly string[] RPCMethods =
            {
                "RPCTranslate",
            };

            /// <summary>
            /// ブリーダーの同期メソッド名を返す
            /// </summary>
            public static string GetRPCMethod(RPCEnum type)
            {
                return RPCMethods[(int)type];
            }
        }

        /// <summary>
        /// ブリーダー周りのRPC
        /// </summary>
        public static class BreederRPC
        {
            /// <summary>
            /// ブリーダーの同期メソッド
            /// </summary>
            public enum RPCEnum
            {
                // ペット召喚
                PetActivate,
            }

            /// <summary>
            /// ブリーダーの同期メソッド名リスト
            /// </summary>
            public static readonly string[] RPCMethods =
            {
                "RPCPetActivate",
            };

            /// <summary>
            /// ブリーダーの同期メソッド名を返す
            /// </summary>
            public static string GetRPCMethod(RPCEnum type)
            {
                return RPCMethods[(int)type];
            }
        }

        /// <summary>
        /// ペット周りのRPC
        /// </summary>
        public static class PetRPC
        {
            /// <summary>
            /// ペットの同期メソッド
            /// </summary>
            public enum RPCEnum
            {
                // モーション再生
                PlayMotion,
            }

            /// <summary>
            /// ペットの同期メソッド名リスト
            /// </summary>
            public static readonly string[] RPCMethods =
            {
                "RPCPlayMotion",
            };

            /// <summary>
            /// ペットの同期メソッド名を返す
            /// </summary>
            public static string GetRPCMethod(RPCEnum type)
            {
                return RPCMethods[(int)type];
            }
        }

        /// <summary>
        /// システム周りのRPC
        /// </summary>
        public static class SystemRPC
        {
            /// <summary>
            /// ペットの同期メソッド
            /// </summary>
            public enum RPCEnum
            {
                // ネットワーク周りのイベント通知
                PublishNetworkEvent,
                // 部屋の中でのイベント通知
                PublishRoomEvent
            }

            /// <summary>
            /// システムの同期メソッド名リスト
            /// </summary>
            public static readonly string[] RPCMethods =
            {
                "RPCPublishNetworkEvent",
                "RPCPublishRoomEvent",
            };

            /// <summary>
            /// システムの同期メソッド名を返す
            /// </summary>
            public static string GetRPCMethod(RPCEnum type)
            {
                return RPCMethods[(int)type];
            }
        }
    }
}
