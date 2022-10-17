using Data;
using UnityEngine;

namespace Cosmetics.StaticData
{
    public static class CosmeticsPlayerPrefs
    {
        public static CosmeticItemData GetEquippedCosmetics(CosmeticsType type) =>
            PlayerPrefs
                .GetString(type.ToString())?
                .ToDeserialized<CosmeticItemData>();

        public static void SetEquippedCosmetics(CosmeticsType type, CosmeticItemData itemData) =>
            PlayerPrefs
                .SetString(type.ToString(), itemData.ToJson());
    }
}