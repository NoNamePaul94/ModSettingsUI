using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ModSettingsUI.Components;

namespace ModSettingsUI.UIElements
{
    public delegate void ClickMainModButton();
    class MainContainer
    {
        private Transform settingsTransform;
        private GameObject mainContainer;
        private GameObject actionButtonContainer;
        private const string objectName = ModSettingsUI.objectNamePrefix + "MainContainer";
        private bool alreadyRendered = false;      
        private List<Section> sections;
        private ClickMainModButton clickMainModButton;

        public MainContainer(Transform settingsTransform, List<Section> sections)
        {
            this.sections = sections;
            this.settingsTransform = settingsTransform;
        }
        public void Destroy()
        {
            GameObject.Destroy(mainContainer);
            GameObject.Destroy(actionButtonContainer);
        }
        public bool Render()
        {
            if (alreadyRendered) return true;

            #region Main Container
            mainContainer = new GameObject(objectName + "_ScrollList", new Type[2]
            {
                typeof(RectTransform),
                typeof(Image)
            });
            mainContainer.transform.SetParent(settingsTransform.Find("panel"));

            RectTransform mainRectTransform = mainContainer.GetComponent<RectTransform>();
            mainRectTransform.offsetMin = new Vector2(0f, 0f);
            mainRectTransform.offsetMax = new Vector2(0f, 0f);
            mainRectTransform.anchorMin = new Vector2(0.05f, 0.16f);
            mainRectTransform.anchorMax = new Vector2(0.95f, 0.88f);

            Image mainImage = mainContainer.GetComponent<Image>();
            mainImage.color = new Color(0f, 0f, 0f, 0f);
            #endregion

            #region Actionbuttons
            actionButtonContainer = new GameObject(objectName + "_ActionButtons", new Type[1]
            {
                typeof(RectTransform)
            });
            actionButtonContainer.transform.SetParent(settingsTransform.Find("panel"));

            RectTransform actionButtonContainerRectTransform = actionButtonContainer.GetComponent<RectTransform>();
            actionButtonContainerRectTransform.offsetMin = new Vector2(0f, 0f);
            actionButtonContainerRectTransform.offsetMax = new Vector2(0f, 0f);
            actionButtonContainerRectTransform.anchorMin = new Vector2(0.05f, 0.045f);
            actionButtonContainerRectTransform.anchorMax = new Vector2(0.95f, 0.13f);

            // OK Button ---
            GameObject okButton = new GameObject(objectName + "_ButtonOk", new Type[3]
            {
                typeof(RectTransform),
                typeof(Image),
                typeof(Button),
            });
            okButton.transform.SetParent(actionButtonContainer.transform);

            RectTransform okButtonRectTransform = okButton.GetComponent<RectTransform>();
            okButtonRectTransform.offsetMin = new Vector2(0f, 0f);
            okButtonRectTransform.offsetMax = new Vector2(0f, 0f);
            okButtonRectTransform.anchorMin = new Vector2(0.65f, 0f);
            okButtonRectTransform.anchorMax = new Vector2(0.90f, 1f);

            Image okButtonImage = okButton.GetComponent<Image>();
            okButtonImage.sprite = ModSettingsUI.refImageAction.sprite;
            okButtonImage.material = ModSettingsUI.refImageAction.material;
            okButtonImage.type = Image.Type.Sliced;
            okButtonImage.pixelsPerUnitMultiplier = 1;
            okButtonImage.fillCenter = true;

            Button okButtonButton = okButton.GetComponent<Button>();
            okButtonButton.onClick.AddListener(new UnityAction(ClickSave));

            GameObject okButtonTextArea = new GameObject(objectName + "_ButtonOkTextArea", new Type[3]
            {
                typeof(RectTransform),
                typeof(Text),
                typeof(Outline)
            });
            okButtonTextArea.transform.SetParent(okButton.transform);

            RectTransform okButtonTextAreaRectTransform = okButtonTextArea.GetComponent<RectTransform>();
            okButtonTextAreaRectTransform.offsetMin = new Vector2(0f, 0f);
            okButtonTextAreaRectTransform.offsetMax = new Vector2(0f, 0f);
            okButtonTextAreaRectTransform.anchorMin = new Vector2(0f, 0f);
            okButtonTextAreaRectTransform.anchorMax = new Vector2(1f, 1f);

            Text okButtonTextAreaText = okButtonTextArea.GetComponent<Text>();
            okButtonTextAreaText.text = "OK";
            okButtonTextAreaText.alignment = TextAnchor.MiddleCenter;            
            okButtonTextAreaText.color = ModSettingsUI.refTextAction.color;
            okButtonTextAreaText.font = ModSettingsUI.refTextAction.font;
            okButtonTextAreaText.resizeTextForBestFit = true;
            okButtonTextAreaText.resizeTextMaxSize = 1;
            okButtonTextAreaText.resizeTextMaxSize = 20;
            okButtonTextAreaText.fontStyle = ModSettingsUI.refTextAction.fontStyle;
            okButtonTextAreaText.fontSize = ModSettingsUI.refTextAction.fontSize;
            okButtonTextAreaText.material = ModSettingsUI.refTextAction.material;

            Outline okButtonTextAreaOutline = okButtonTextArea.GetComponent<Outline>();
            okButtonTextAreaOutline.effectColor = new Color(0f, 0f, 0f, 1f);
            // OK Button +++

            // Back Button ---
            GameObject backButton = new GameObject(objectName + "_ButtonBack", new Type[3]
            {
                typeof(RectTransform),
                typeof(Image),
                typeof(Button),
            });
            backButton.transform.SetParent(actionButtonContainer.transform);

            RectTransform backButtonRectTransform = backButton.GetComponent<RectTransform>();
            backButtonRectTransform.offsetMin = new Vector2(0f, 0f);
            backButtonRectTransform.offsetMax = new Vector2(0f, 0f);
            backButtonRectTransform.anchorMin = new Vector2(0.10f, 0f);
            backButtonRectTransform.anchorMax = new Vector2(0.35f, 1f);

            Image backButtonImage = backButton.GetComponent<Image>();
            backButtonImage.sprite = ModSettingsUI.refImageAction.sprite;
            backButtonImage.material = ModSettingsUI.refImageAction.material;
            backButtonImage.type = Image.Type.Sliced;
            backButtonImage.pixelsPerUnitMultiplier = 1;
            backButtonImage.fillCenter = true;

            Button backButtonButton = backButton.GetComponent<Button>();
            backButtonButton.onClick.AddListener(new UnityAction(ClickBack));

            GameObject backButtonTextArea = new GameObject(objectName + "_ButtonBackTextArea", new Type[3]
            {
                typeof(RectTransform),
                typeof(Text),
                typeof(Outline)
            });
            backButtonTextArea.transform.SetParent(backButton.transform);

            RectTransform backButtonTextAreaRectTransform = backButtonTextArea.GetComponent<RectTransform>();
            backButtonTextAreaRectTransform.offsetMin = new Vector2(0f, 0f);
            backButtonTextAreaRectTransform.offsetMax = new Vector2(0f, 0f);
            backButtonTextAreaRectTransform.anchorMin = new Vector2(0f, 0f);
            backButtonTextAreaRectTransform.anchorMax = new Vector2(1f, 1f);

            Text backButtonTextAreaText = backButtonTextArea.GetComponent<Text>();
            backButtonTextAreaText.text = "Back";
            backButtonTextAreaText.alignment = TextAnchor.MiddleCenter;
            backButtonTextAreaText.color = ModSettingsUI.refTextAction.color;
            backButtonTextAreaText.font = ModSettingsUI.refTextAction.font;
            backButtonTextAreaText.resizeTextForBestFit = true;
            backButtonTextAreaText.resizeTextMaxSize = 1;
            backButtonTextAreaText.resizeTextMaxSize = 20;
            backButtonTextAreaText.fontStyle = ModSettingsUI.refTextAction.fontStyle;
            backButtonTextAreaText.fontSize = ModSettingsUI.refTextAction.fontSize;
            backButtonTextAreaText.material = ModSettingsUI.refTextAction.material;

            Outline backButtonTextAreaOutline = backButtonTextArea.GetComponent<Outline>();
            backButtonTextAreaOutline.effectColor = new Color(0f, 0f, 0f, 1f);
            // Back Button +++
            #endregion

            #region ScrollBar            
            GameObject scrollBar = new GameObject(objectName + "_ScrollBar", new Type[3]
            {
                typeof(RectTransform),
                typeof(Image),
                typeof(Scrollbar)
            });
            scrollBar.transform.SetParent(mainContainer.transform);

            GameObject slidingArea = new GameObject(objectName + "_SlidingArea", new Type[1]
            {
                typeof(RectTransform)
            });
            slidingArea.transform.SetParent(scrollBar.transform);

            GameObject scrollBarHandle = new GameObject(objectName + "_SlidingHandle", new Type[2]
            {
                typeof(RectTransform),
                typeof(Image)
            });
            scrollBarHandle.transform.SetParent(slidingArea.transform);


            RectTransform scrollBarHandleRectTransform = scrollBarHandle.GetComponent<RectTransform>();
            scrollBarHandleRectTransform.offsetMin = new Vector2(0f, 0f);
            scrollBarHandleRectTransform.offsetMax = new Vector2(0f, 0f);

            Image scrollBarHandleImage = scrollBarHandle.GetComponent<Image>();
            scrollBarHandleImage.sprite = ModSettingsUI.refImageAction.sprite;
            scrollBarHandleImage.material = ModSettingsUI.refImageAction.material;
            scrollBarHandleImage.type = Image.Type.Sliced;
            scrollBarHandleImage.pixelsPerUnitMultiplier = 1;
            scrollBarHandleImage.fillCenter = true;


            RectTransform slidingAreaRectTransform = slidingArea.GetComponent<RectTransform>();
            slidingAreaRectTransform.offsetMin = new Vector2(0f, 0f);
            slidingAreaRectTransform.offsetMax = new Vector2(0f, 0f);
            slidingAreaRectTransform.anchorMin = new Vector2(0f, 0f);
            slidingAreaRectTransform.anchorMax = new Vector2(1f, 1f);


            RectTransform scrollBarRectTransform = scrollBar.GetComponent<RectTransform>();
            scrollBarRectTransform.offsetMin = new Vector2(0f, 0f);
            scrollBarRectTransform.offsetMax = new Vector2(0f, 0f);
            scrollBarRectTransform.anchorMin = new Vector2(0.95f, 0f);
            scrollBarRectTransform.anchorMax = new Vector2(1f, 1f);

            Image scrollBarImage = scrollBar.GetComponent<Image>();
            scrollBarImage.color = new Color(0f, 0f, 0f, 0.5f);
            scrollBarImage.sprite = ModSettingsUI.refImageDefault.sprite;
            scrollBarImage.material = ModSettingsUI.refImageDefault.material;
            scrollBarImage.type = Image.Type.Sliced;
            scrollBarImage.pixelsPerUnitMultiplier = 1;
            scrollBarImage.fillCenter = true;            

            Scrollbar scrollBarScrollBar = scrollBar.GetComponent<Scrollbar>();
            scrollBarScrollBar.direction = Scrollbar.Direction.BottomToTop;
            
            scrollBarScrollBar.targetGraphic = scrollBarHandleImage;
            scrollBarScrollBar.handleRect = scrollBarHandleRectTransform;
                     
            #endregion

            #region ViewPort
            GameObject viewPort = new GameObject(objectName + "_ViewPort", new Type[4]
            {
                typeof(RectTransform),
                typeof(ScrollRect),
                typeof(Image),
                typeof(Mask),
            });
            viewPort.transform.SetParent(mainContainer.transform);
            RectTransform scrollViewRectTransform = viewPort.GetComponent<RectTransform>();

            scrollViewRectTransform.offsetMin = new Vector2(0f, 0f);
            scrollViewRectTransform.offsetMax = new Vector2(0f, 0f);
            scrollViewRectTransform.anchorMin = new Vector2(0f, 0f);
            scrollViewRectTransform.anchorMax = new Vector2(0.94f, 1f);

            ScrollRect scrollViewScrollRect = viewPort.GetComponent<ScrollRect>();            
            scrollViewScrollRect.movementType = ScrollRect.MovementType.Clamped;
            scrollViewScrollRect.inertia = false;
            scrollViewScrollRect.horizontal = false;
            scrollViewScrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            scrollViewScrollRect.verticalScrollbar = scrollBarScrollBar;
            scrollViewScrollRect.scrollSensitivity = 50f;

            Image scrollViewImage = viewPort.GetComponent<Image>();
            scrollViewImage.color = new Color(0f, 0f, 0f, 0.2f);
            #endregion

            #region Content            
            GameObject contentContainer = new GameObject(objectName + "_Content", new Type[4]
            {
                typeof(RectTransform),
                typeof(VerticalLayoutGroup),
                typeof(ContentSizeFitter),
                typeof(Image)
            });
            contentContainer.transform.SetParent(viewPort.transform);

            Image contentContainerImage = contentContainer.GetComponent<Image>();
            contentContainerImage.color = new Color(0f, 0f, 0f, 0.1f);

            RectTransform contentRect = contentContainer.GetComponent<RectTransform>();
            contentRect.pivot = new Vector2(0f, 1f);
            contentRect.offsetMin = new Vector2(0f, 0f);
            contentRect.offsetMax = new Vector2(0f, 0f);
            contentRect.anchorMin = new Vector2(0f, 1f);
            contentRect.anchorMax = new Vector2(1f, 1f);

            VerticalLayoutGroup contentContainerVerticalLayoutGroup = contentContainer.GetComponent<VerticalLayoutGroup>();
            contentContainerVerticalLayoutGroup.spacing = 5;
            contentContainerVerticalLayoutGroup.childAlignment = TextAnchor.UpperLeft;
            contentContainerVerticalLayoutGroup.childControlHeight = true;
            contentContainerVerticalLayoutGroup.childControlWidth = true;
            contentContainerVerticalLayoutGroup.childForceExpandHeight = false;
            contentContainerVerticalLayoutGroup.childForceExpandWidth = true;

            ContentSizeFitter contentContainerContentSizeFitter = contentContainer.GetComponent<ContentSizeFitter>();
            contentContainerContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            contentContainerContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            sections.Sort(new Comparison<Section>((a, b) =>
            {
                return a.title.CompareTo(b.title);
            }));
            foreach(Section section in sections)
            {
                section.Render(objectName, contentRect.transform);
            }
            #endregion

            scrollViewScrollRect.content = contentRect;
            scrollBarScrollBar.value = 1f;

            ToggleVisibility(false);
            alreadyRendered = true;
            return true;
        }

        public void ClickSave()
        {
            foreach(Section section in sections)
            {
                section.Save();
            }
            ClickBack();
        }
        public void ClickBack()
        {
            if (clickMainModButton != null)
                clickMainModButton();
        }

        public void setMainModButtonCallback(ClickMainModButton callback)
        {
            clickMainModButton = callback;
        }

        public void ToggleVisibility(bool value)
        {
            mainContainer.SetActive(value);
            actionButtonContainer.SetActive(value);
        }
    }
}
