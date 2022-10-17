using System.Collections.Generic;
using System.Linq;
using Cosmetics.StaticData;
using UnityEngine;

namespace Cosmetics
{
    public class CosmeticsDataProvider : MonoBehaviour
    {
        public CosmeticItem CosmeticItemPrefab;
        public CosmeticsSelector CosmeticsSelector;

        public List<CosmeticsScrollFiller> ScrollFillers;
        public List<CosmeticsStaticData> StaticDataObjects;

        private Dictionary<CosmeticsType, List<CosmeticItemData>> _cosmeticItemsDictionary;

        private void Awake()
        {
            _cosmeticItemsDictionary = StaticDataObjects.ToDictionary(data => data.Type, data => data.SortedItems);

            CosmeticsSelector.Initialize();
            
            foreach (var scrollFiller in ScrollFillers)
            {
                var type = scrollFiller.Type;
                if (type == CosmeticsType.Unknown) continue;
                
                scrollFiller.Construct(CosmeticItemPrefab, CosmeticsSelector);

                if (!CosmeticItemsExistInData(ofType: type, out List<CosmeticItemData> items)) continue;
                
                var equippedItem = GetEquippedItemFromPlayerPrefs(type) ?? GetAndRegisterFirstItem(type, items);
                scrollFiller.Initialize(items, equippedItem);
            }
        }

        private static CosmeticItemData GetAndRegisterFirstItem(CosmeticsType type, List<CosmeticItemData> items)
        {
            var item = items[0];
            
            CosmeticsPlayerPrefs.SetEquippedCosmetics(type, item);
            
            return items[0];
        }

        private static CosmeticItemData GetEquippedItemFromPlayerPrefs(CosmeticsType type) =>
            CosmeticsPlayerPrefs.GetEquippedCosmetics(type);

        private bool CosmeticItemsExistInData(CosmeticsType ofType, out List<CosmeticItemData> items) =>
            _cosmeticItemsDictionary.TryGetValue(ofType, out items);
    }
}