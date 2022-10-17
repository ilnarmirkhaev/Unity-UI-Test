using System.Collections.Generic;
using Cosmetics.StaticData;
using UnityEngine;

namespace Cosmetics
{
    public class CosmeticsScrollFiller : MonoBehaviour
    {
        public CosmeticsType Type;
        public Transform ContentParent;

        private CosmeticItem _itemPrefab;
        private CosmeticsSelector _selector;

        private List<CosmeticItemData> _itemsData;
        private CosmeticItemData _equippedItem;

        private bool _equippedItemIsFound;

        public void Construct(CosmeticItem itemPrefab, CosmeticsSelector selector)
        {
            _itemPrefab = itemPrefab;
            _selector = selector;
        }

        public void Initialize(List<CosmeticItemData> itemsData, CosmeticItemData equippedItem)
        {
            _itemsData = itemsData;
            _equippedItem = equippedItem;

            InitializeItemsAndFillContent();
        }

        private void InitializeItemsAndFillContent()
        {
            foreach (var itemData in _itemsData)
            {
                var isEquippedItem = IsEquippedItem(itemData);

                CosmeticItem item = Instantiate(_itemPrefab, ContentParent);
                item.Initialize(Type, itemData, isEquippedItem, _selector);

                if (isEquippedItem)
                    _selector.AddEquippedItem(item);
            }
        }

        private bool IsEquippedItem(CosmeticItemData itemData)
        {
            if (_equippedItemIsFound) return false;

            var isEqual = EquippedItemDataIsEqual(to: itemData);
            if (isEqual) _equippedItemIsFound = true;

            return isEqual;
        }

        private bool EquippedItemDataIsEqual(CosmeticItemData to) =>
            CosmeticItemData.Equals(to, _equippedItem);
    }
}