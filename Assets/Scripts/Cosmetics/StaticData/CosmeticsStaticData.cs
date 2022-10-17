using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cosmetics.StaticData
{
    [CreateAssetMenu(fileName = "Cosmetics", menuName = "Static Data/Cosmetics")]
    public class CosmeticsStaticData : ScriptableObject
    {
        public CosmeticsType Type;
        public List<CosmeticItemData> Items;

        public List<CosmeticItemData> SortedItems =>
            Items
                .OrderByDescending(x => x.IsOwned)
                .ToList();
    }
}