using Cosmetics;
using Cosmetics.StaticData;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tabs
{
    [RequireComponent(typeof(Image))]
    public class Tab : MonoBehaviour, IPointerClickHandler
    {
        public string TabName;

        [SerializeField] private Image image;
        [SerializeField] private Canvas linkedCanvas;
        
        public CosmeticsType Type { get; private set; }

        private TabGroup _tabGroup;
        private ScrollRect _scroll;

        public void Initialize(TabGroup tabGroup)
        {
            _scroll = linkedCanvas.GetComponentInChildren<ScrollRect>();

            _tabGroup = tabGroup;
        }

        private void Start() =>
            Type = linkedCanvas.GetComponent<CosmeticsScrollFiller>().Type;

        public void OnPointerClick(PointerEventData eventData) =>
            _tabGroup.OnTabSelected(this);

        private void Reset()
        {
            image = GetComponent<Image>();
            TabName = name;
        }

        public void SetSprite(Sprite sprite) =>
            image.sprite = sprite;

        public void ShowCanvas()
        {
            _scroll.verticalNormalizedPosition = 1;
            linkedCanvas.enabled = true;
        }

        public void HideCanvas() =>
            linkedCanvas.enabled = false;
    }
}