using System;
using UnityEngine;

namespace Cosmetics.StaticData
{
    [Serializable]
    public class CosmeticItemData
    {
        public string Name;
        public Sprite Sprite;
        public bool IsOwned;

        public static bool Equals(CosmeticItemData itemData1, CosmeticItemData itemData2) =>
            itemData1.Name == itemData2.Name &&
            itemData1.Sprite == itemData2.Sprite &&
            itemData1.IsOwned == itemData2.IsOwned;
    }
}