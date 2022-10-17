using System.Collections.Generic;
using System.Linq;
using Cosmetics.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cosmetics
{
    public class CosmeticsSelector : MonoBehaviour
    {
        public TextMeshProUGUI ItemNameText;
        public Button SubmitButton;
        public TextMeshProUGUI NotAvailableText;
        public AnimateUI AnimatedImage;

        public List<CosmeticsDisplayConfig> DisplayConfigs;

        private Dictionary<CosmeticsType, Image> _displayImages;

        private Dictionary<CosmeticsType, CosmeticItem> _equippedItems;

        private CosmeticItem _selectedItem;

        public void Initialize()
        {
            _displayImages = DisplayConfigs.ToDictionary(config => config.CosmeticsType, config => config.Image);
            _equippedItems = new Dictionary<CosmeticsType, CosmeticItem>();

            SubmitButton.onClick.AddListener(Submit);
            ToggleAvailability(true);
        }

        public void AddEquippedItem(CosmeticItem item)
        {
            if (!DisplayImageExists(ofType: item.Type, out var displayImage))
            {
                Debug.LogError($"DisplayImage of type {item.Type} doesn't exist");
                return;
            }

            UpdateDisplayImage(item, displayImage);
            _equippedItems.Add(item.Type, item);
        }

        private void Start() =>
            UpdateDisplayText(_equippedItems.ElementAt(0).Value);

        public void OnSelect(CosmeticItem newItem)
        {
            if (newItem == _selectedItem)
            {
                DeselectPreviousItem();
                return;
            }

            if (_selectedItem != null)
                _selectedItem.HideOutline();


            if (ItemOfDifferentTypeSelected(newItem))
                DeselectPreviousItem();

            var displayImage = _displayImages[newItem.Type];
            UpdateDisplayImage(newItem, displayImage);
            UpdateDisplayText(newItem);

            _selectedItem = newItem;
            _selectedItem.ShowOutline();

            ToggleAvailability(_selectedItem.Data.IsOwned);
        }

        private void AnimateCharacterImage() =>
            AnimatedImage.Animate();


        public void OnTabSwitched(CosmeticsType type)
        {
            DeselectPreviousItem();
            UpdateDisplayText(_equippedItems[type]);
        }

        private void Submit()
        {
            if (_selectedItem == null) return;

            UnequipPreviousItem();
            EquipSelectedItem();
            DeselectPreviousItem();
        }

        private void DeselectPreviousItem()
        {
            if (_selectedItem == null) return;

            _selectedItem.HideOutline();

            var type = _selectedItem.Type;
            var equippedItem = _equippedItems[type];

            if (_selectedItem != equippedItem)
            {
                UpdateDisplayImage(equippedItem, _displayImages[type]);
                UpdateDisplayText(equippedItem);
                ToggleAvailability(itemIsOwned: true);
            }

            _selectedItem = null;
        }

        private void EquipSelectedItem()
        {
            _selectedItem.IsEquipped = true;
            _equippedItems[_selectedItem.Type] = _selectedItem;
            CosmeticsPlayerPrefs.SetEquippedCosmetics(_selectedItem.Type, _selectedItem.Data);
        }

        private void UnequipPreviousItem()
        {
            if (EquippedItemExists(_selectedItem.Type, out CosmeticItem item))
                item.IsEquipped = false;
        }

        private void UpdateDisplayImage(CosmeticItem item, Image displayImage)
        {
            displayImage.sprite = item.Data.Sprite;

            if (item.Type == CosmeticsType.Character)
                AnimateCharacterImage();
        }

        private void UpdateDisplayText(CosmeticItem newItem) =>
            ItemNameText.text = newItem.Data.Name;

        private void ToggleAvailability(bool itemIsOwned)
        {
            SubmitButton.gameObject.SetActive(itemIsOwned);
            NotAvailableText.gameObject.SetActive(!itemIsOwned);
        }

        private bool DisplayImageExists(CosmeticsType ofType, out Image displayImage) =>
            _displayImages.TryGetValue(ofType, out displayImage);

        private bool EquippedItemExists(CosmeticsType ofType, out CosmeticItem item) =>
            _equippedItems.TryGetValue(ofType, out item);

        private bool ItemOfDifferentTypeSelected(CosmeticItem item) =>
            _selectedItem != null && item.Type != _selectedItem.Type;
    }
}