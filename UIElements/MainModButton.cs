using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModSettingsUI.UIElements
{
    class MainModButton
    {
        private Color lastColor = new Color(0.84f, 0.525f, 0.196f, 1f);
        private Transform settingsTransform;
        private GameObject mainModButton;
        private bool alreadyRendered = false;
        private bool active = false;
        private const string objectName = "MainModButton";
        private MainContainer mainContainer;

        public MainModButton(Transform settingsTransform, MainContainer mainContainer)
        {
            this.mainContainer = mainContainer;
            this.settingsTransform = settingsTransform;
        }
        public void Destroy()
        {
            GameObject.Destroy(mainModButton);
        }
        public bool Render()
        {
            if (alreadyRendered) return true;

            Transform tabButtons = null;
            try
            {
                tabButtons = settingsTransform.Find("panel").Find("TabButtons");
            }
            catch (Exception e)
            {
                Debug.Log("Failed finding settings menu containers... Did something change?");
                Debug.Log("Message: " + e.Message);
                return false;
            }
            if (tabButtons == null)
            {
                Debug.Log("Failed finding settings menu containers without error... Did something change?");
                return false;
            }

            Transform miscTab = tabButtons.Find("Misc");

            mainModButton = new GameObject(ModSettingsUI.objectNamePrefix + objectName, new Type[3]
            {
                typeof(RectTransform),
                typeof(Image),
                typeof(Button)
            });
            Transform panel = settingsTransform.Find("panel");
            mainModButton.transform.SetParent(panel);

            #region General Position
            RectTransform miscTabRect = miscTab.GetComponent<RectTransform>();
            RectTransform modTabRect = mainModButton.GetComponent<RectTransform>();
            RectTransform panelRect = panel.GetComponent<RectTransform>();

            modTabRect.anchorMin = new Vector2(1f, 1f);
            modTabRect.anchorMax = new Vector2(.98f, .98f);
            modTabRect.pivot = new Vector2(1f, 1f);
            modTabRect.anchoredPosition = new Vector2(0f, 0f);
            modTabRect.sizeDelta = new Vector2(120f, 40f);
            #endregion
            #region Image Settings
            Image miscTabImage = miscTab.GetComponent<Image>();
            Image mainModButtonImage = mainModButton.GetComponent<Image>();

            mainModButtonImage.color = miscTabImage.color;
            mainModButtonImage.sprite = miscTabImage.sprite;
            mainModButtonImage.material = miscTabImage.material;
            mainModButtonImage.fillAmount = miscTabImage.fillAmount;
            mainModButtonImage.alphaHitTestMinimumThreshold = miscTabImage.alphaHitTestMinimumThreshold;
            mainModButtonImage.overrideSprite = miscTabImage.overrideSprite;
            mainModButtonImage.sprite = miscTabImage.sprite;
            mainModButtonImage.type = miscTabImage.type;
            mainModButtonImage.fillOrigin = miscTabImage.fillOrigin;
            mainModButtonImage.fillCenter = miscTabImage.fillCenter;
            mainModButtonImage.preserveAspect = miscTabImage.preserveAspect;
            #endregion
            #region Text Settings
            GameObject mainModButtonText = new GameObject(ModSettingsUI.objectNamePrefix + objectName + "Text", new Type[3]{
                typeof(RectTransform),
                typeof(Text),
                typeof(Outline)
            });
            mainModButtonText.transform.SetParent(mainModButton.transform);
            RectTransform mainModButtonTextRect = mainModButtonText.GetComponent<RectTransform>();
            Text mainModButtonTextText = mainModButtonText.GetComponent<Text>();

            mainModButtonTextRect.anchorMin = new Vector2(0f, 0f);
            mainModButtonTextRect.anchorMax = new Vector2(1f, 1f);
            mainModButtonTextRect.pivot = new Vector2(1f, 1f);
            mainModButtonTextRect.anchoredPosition = new Vector2(0f, 0f);
            mainModButtonTextRect.sizeDelta = new Vector2(0f, 0f);

            Text miscTabText = miscTab.Find("Text").GetComponent<Text>();

            mainModButtonTextText.text = "Mods";
            mainModButtonTextText.alignment = TextAnchor.MiddleCenter;
            mainModButtonTextText.color = miscTabText.color;
            mainModButtonTextText.font = miscTabText.font;
            mainModButtonTextText.resizeTextForBestFit = true;
            mainModButtonTextText.resizeTextMinSize = 1;
            mainModButtonTextText.resizeTextMaxSize = 20;
            mainModButtonTextText.fontStyle = miscTabText.fontStyle;

            Outline mainModButtonTextOutline = mainModButtonText.GetComponent<Outline>();
            mainModButtonTextOutline.effectColor = new Color(0f, 0f, 0f, 1f);
            #endregion

            Button modTabButton = mainModButton.GetComponent<Button>();
            modTabButton.onClick.AddListener(new UnityAction(Click));

            alreadyRendered = true;
            return true;
        }

        public void Click()
        {
            //UIPatch.DebugTransform(settingsTransform.Find("panel"),3);
            Transform tabButtons = settingsTransform.Find("panel").Find("TabButtons");
            for (int i = 0; i < settingsTransform.Find("panel").childCount; i++)
            {
                Transform child = settingsTransform.Find("panel").GetChild(i);
                if (child.name.StartsWith(ModSettingsUI.objectNamePrefix) || child.name == "Settings_topic") continue;

                child.gameObject.SetActive(active);
            }

            Image mainModButtonImage = mainModButton.GetComponent<Image>();
            Color tempColor = mainModButtonImage.color;
            mainModButtonImage.color = lastColor;
            lastColor = tempColor;
            
            active = !active;
            mainContainer.ToggleVisibility(active);
        }
    }
}
