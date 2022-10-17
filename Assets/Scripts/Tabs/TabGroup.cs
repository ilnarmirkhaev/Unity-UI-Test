using System.Collections.Generic;
using Cosmetics;
using TMPro;
using UnityEngine;

namespace Tabs
{
    public class TabGroup : MonoBehaviour
    {
        public CosmeticsSelector CosmeticsSelector;
        public TextMeshProUGUI GroupNameText;

        public List<Tab> TabButtons;

        public Sprite IdleSprite;
        public Sprite SelectedSprite;


        private Tab _currentTab;

        private void Awake() =>
            InitializeTabs();

        private void InitializeTabs()
        {
            foreach (var tab in TabButtons)
            {
                tab.Initialize(this);
                tab.HideCanvas();
            }
        }

        private void Start() =>
            SelectTab(TabButtons[0]);

        public void OnTabSelected(Tab tab)
        {
            if (_currentTab == tab) return;

            DeselectTab();
            SelectTab(tab);

            CosmeticsSelector.OnTabSwitched(tab.Type);
        }

        private void UpdateGroupNameText(Tab tab) =>
            GroupNameText.text = tab.TabName;

        private void SelectTab(Tab tab)
        {
            UpdateGroupNameText(tab);

            _currentTab = tab;
            _currentTab.SetSprite(SelectedSprite);
            _currentTab.ShowCanvas();
        }

        private void DeselectTab()
        {
            if (_currentTab == null) return;

            _currentTab.SetSprite(IdleSprite);
            _currentTab.HideCanvas();
        }
    }
}