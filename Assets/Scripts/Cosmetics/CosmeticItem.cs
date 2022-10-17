using Cosmetics.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace Cosmetics
{
    public class CosmeticItem : MonoBehaviour
    {
        public Image Image;
		public Button Button;
        public Image EquippedIcon;
        public Image LockedIcon;
        public UIOutline Outline;

        public CosmeticsType Type { get; private set; }
        public CosmeticItemData Data { get; private set; }

        public bool IsEquipped
        {
            get => _isEquipped;
            set
            {
                _isEquipped = value;
                UpdateEquippedIcon(value);
            }
        }

        private bool _isEquipped;

        private CosmeticsSelector _selector;
	

        public void Initialize(CosmeticsType type, CosmeticItemData data, bool isEquippedItem,
            CosmeticsSelector selector)
        {
            Type = type;
            Data = data;
            IsEquipped = isEquippedItem;
            _selector = selector;

            UpdateSprite(Data.Sprite);
            UpdateLockedIcon(Data.IsOwned);

            HideOutline();
			
			Button.onClick.AddListener(OnClicked);
        }

        public void HideOutline() =>
            Outline.enabled = false;

        public void ShowOutline() =>
            Outline.enabled = true;
		
        private void OnClicked() =>
            _selector.OnSelect(this);

        private void UpdateEquippedIcon(bool isEquipped) =>
            EquippedIcon.enabled = isEquipped;

        private void UpdateLockedIcon(bool isOwned) =>
            LockedIcon.enabled = !isOwned;

        private void UpdateSprite(Sprite sprite) =>
            Image.sprite = sprite;
    }
}