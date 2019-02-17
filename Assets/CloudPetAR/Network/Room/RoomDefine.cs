using System;
using UdonLib.Commons.Extensions;

namespace CloudPet.Network
{
    public static class RoomDefine
    {
        public static readonly string CONNECTING_SETTING = "v1";
        public static readonly byte MAX_ROOM_USER = 4;
        public static readonly string ANCHOR_KEY = "AnchorId";
    }

    public struct RoomData : IEquatable<RoomData>
    {
        public readonly string AnchorId;
        public readonly int MasterId;
        public readonly string Name;
        public readonly int MaxBreederCount;
        public readonly int BreederCount;
        public readonly bool IsOpen;
        public readonly bool IsVisible;

        public RoomData(RoomInfo info)
        {
            MasterId = info.masterClientIdField;
            Name = info.Name;
            MaxBreederCount = info.MaxPlayers;
            BreederCount = info.PlayerCount;
            IsOpen = info.IsOpen;
            IsVisible = info.IsVisible;

            info.CustomProperties.TryGetValue(RoomDefine.ANCHOR_KEY, out var andhorId);
            AnchorId = (string) andhorId;
        }

        public RoomData(string anchorId, int masterId, string name, int maxBreederCount, int breederCount, bool isOpen, bool isVisible)
        {
            AnchorId = anchorId;
            MasterId = masterId;
            Name = name;
            MaxBreederCount = maxBreederCount;
            BreederCount = breederCount;
            IsOpen = isOpen;
            IsVisible = isVisible;
        }

        public override int GetHashCode()
        {
            return AnchorId.GetNullableHashCode() ^ MasterId.GetHashCode() ^ Name.GetNullableHashCode() ^
                   MaxBreederCount.GetHashCode() ^ BreederCount.GetHashCode() ^ IsOpen.GetHashCode() ^
                   IsVisible.GetHashCode();
        }

        public bool Equals(RoomData other)
        {
            return AnchorId == other.AnchorId && MasterId == other.MasterId && Name == other.Name &&
                   MaxBreederCount == other.MaxBreederCount && BreederCount == other.BreederCount &&
                   IsOpen == other.IsOpen && IsVisible == other.IsVisible;
        }
    }
}
