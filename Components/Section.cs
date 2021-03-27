using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModSettingsUI.Components
{
    class Section
    {
        public string title;
        private List<ChangeableOption> options;
        private bool visible = false;

        public Section(string title)
        {
            this.title = title;
            options = new List<ChangeableOption>();
        }

        public void AddOption(OptionType type, string optionTitle, object initialValue, SettingsSavedCallback callback)
        {
            switch (type)
            {
                case OptionType.DecimalInput:
                    options.Add(new DecimalInput(optionTitle, initialValue, callback));
                    break;
                case OptionType.Checkbox:
                    options.Add(new Checkbox(optionTitle, initialValue, callback));
                    break;
            }
        }

        public void Render(string objectName, Transform contentTransform)
        {
            #region List Item
            GameObject listItem = new GameObject(objectName + "_ListItem_" + title, new Type[4]
                {
                    typeof(RectTransform),
                    typeof(Image),
                    typeof(VerticalLayoutGroup),
                    typeof(ContentSizeFitter)
                });
            listItem.transform.SetParent(contentTransform);

            Image listItemImage = listItem.GetComponent<Image>();
            listItemImage.color = new Color(0f, 0f, 0f, 0.1f);

            VerticalLayoutGroup listItemVerticalLayoutGroup = listItem.GetComponent<VerticalLayoutGroup>();
            listItemVerticalLayoutGroup.spacing = 0;
            listItemVerticalLayoutGroup.childAlignment = TextAnchor.UpperLeft;
            listItemVerticalLayoutGroup.childControlHeight = true;
            listItemVerticalLayoutGroup.childControlWidth = true;
            listItemVerticalLayoutGroup.childForceExpandHeight = false;
            listItemVerticalLayoutGroup.childForceExpandWidth = true;

            ContentSizeFitter listItemContentSizeFitter = listItem.GetComponent<ContentSizeFitter>();
            listItemContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            listItemContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            #endregion

            #region Header
            GameObject header = new GameObject(objectName + "_ListItem_" + title + "_Header", new Type[6]
            {
                typeof(RectTransform),
                typeof(Image),
                typeof(HorizontalLayoutGroup),
                typeof(ContentSizeFitter),
                typeof(LayoutElement),
                typeof(Button)
            });
            header.transform.SetParent(listItem.transform);

            Image headerImage = header.GetComponent<Image>();
            headerImage.color = new Color(0.3f, 0.3f, 0.3f, 0.2f);

            HorizontalLayoutGroup headerHorizontalLayoutGroup = header.GetComponent<HorizontalLayoutGroup>();
            headerHorizontalLayoutGroup.spacing = 0;
            headerHorizontalLayoutGroup.padding = new RectOffset(5, 5, 5, 5);
            headerHorizontalLayoutGroup.childAlignment = TextAnchor.MiddleLeft;
            headerHorizontalLayoutGroup.childControlHeight = true;
            headerHorizontalLayoutGroup.childControlWidth = true;
            headerHorizontalLayoutGroup.childForceExpandHeight = false;
            headerHorizontalLayoutGroup.childForceExpandWidth = false;
            headerHorizontalLayoutGroup.spacing = 15;

            ContentSizeFitter headerContentSizeFitter = header.GetComponent<ContentSizeFitter>();
            headerContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            headerContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;


            GameObject headerRedTriangle = new GameObject(objectName + "_ListItem_" + title + "_HeaderRedTriangle", new Type[3]
            {
                typeof(RectTransform),
                typeof(Image),
                typeof(LayoutElement)
            });
            headerRedTriangle.transform.SetParent(header.transform);

            Image headerRedTriangleImage = headerRedTriangle.GetComponent<Image>();
            headerRedTriangleImage.sprite = ModSettingsUIPlugin.rightRedTriangle;

            LayoutElement headerRedTriangleLayoutElement = headerRedTriangle.GetComponent<LayoutElement>();
            headerRedTriangleLayoutElement.flexibleWidth = 0f;
            headerRedTriangleLayoutElement.flexibleHeight = 0f;
            headerRedTriangleLayoutElement.preferredHeight = 15f;
            headerRedTriangleLayoutElement.preferredWidth = 15f;

            headerRedTriangle.SetActive(!visible);

            GameObject headerGreenTriangle = new GameObject(objectName + "_ListItem_" + title + "_HeaderGreenTriangle", new Type[3]
            {
                typeof(RectTransform),
                typeof(Image),
                typeof(LayoutElement)
            });
            headerGreenTriangle.transform.SetParent(header.transform);

            Image headerGreenTriangleImage = headerGreenTriangle.GetComponent<Image>();
            headerGreenTriangleImage.sprite = ModSettingsUIPlugin.downGreenTriangle;

            LayoutElement headerGreenTriangleLayoutElement = headerGreenTriangle.GetComponent<LayoutElement>();
            headerGreenTriangleLayoutElement.flexibleWidth = 0f;
            headerGreenTriangleLayoutElement.flexibleHeight = 0f;
            headerGreenTriangleLayoutElement.preferredHeight = 15f;
            headerGreenTriangleLayoutElement.preferredWidth = 15f;

            headerGreenTriangle.SetActive(visible);

            GameObject headerTextArea = new GameObject(objectName + "_ListItem_" + title + "_HeaderTextArea", new Type[4]
            {
                    typeof(RectTransform),
                    typeof(Text),
                    typeof(Outline),
                    typeof(LayoutElement)
            });
            headerTextArea.transform.SetParent(header.transform);

            Text headerTextAreaText = headerTextArea.GetComponent<Text>();
            headerTextAreaText.text = title;
            headerTextAreaText.alignment = TextAnchor.MiddleLeft;
            headerTextAreaText.color = ModSettingsUI.refTextAction.color;
            headerTextAreaText.font = ModSettingsUI.refTextAction.font;
            headerTextAreaText.fontStyle = ModSettingsUI.refTextAction.fontStyle;
            headerTextAreaText.fontSize = ModSettingsUI.refTextAction.fontSize;
            headerTextAreaText.material = ModSettingsUI.refTextAction.material;

            LayoutElement headerTextAreaLayoutElement = headerTextArea.GetComponent<LayoutElement>();
            headerTextAreaLayoutElement.flexibleHeight = 1f;
            headerTextAreaLayoutElement.flexibleWidth = 1f;

            Outline headerTextAreaOutline = headerTextArea.GetComponent<Outline>();
            headerTextAreaOutline.effectColor = new Color(0f, 0f, 0f, 1f);
            #endregion

            #region Content
            GameObject itemContent = new GameObject(objectName + "_ListItem_" + title + "_Content", new Type[5]
            {
                typeof(RectTransform),
                typeof(Image),
                typeof(VerticalLayoutGroup),        
                typeof(ContentSizeFitter),
                typeof(LayoutElement)
            });
            itemContent.transform.SetParent(listItem.transform);
            
            Image itemContentImage = itemContent.GetComponent<Image>();
            itemContentImage.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);

            VerticalLayoutGroup itemContentVerticalLayoutGroup = itemContent.GetComponent<VerticalLayoutGroup>();
            itemContentVerticalLayoutGroup.padding = new RectOffset(20, 0, 7, 10);
            itemContentVerticalLayoutGroup.spacing = 5;
            itemContentVerticalLayoutGroup.childAlignment = TextAnchor.UpperLeft;
            itemContentVerticalLayoutGroup.childControlHeight = true;
            itemContentVerticalLayoutGroup.childControlWidth = true;
            itemContentVerticalLayoutGroup.childForceExpandHeight = false;
            itemContentVerticalLayoutGroup.childForceExpandWidth = true;

            ContentSizeFitter itemContentContentSizeFitter = itemContent.GetComponent<ContentSizeFitter>();
            itemContentContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            itemContentContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            LayoutElement itemContentLayoutElement = itemContent.GetComponent<LayoutElement>();
            itemContentLayoutElement.flexibleWidth = 1f;

            foreach (ChangeableOption option in options) option.Render(objectName, title, itemContent.transform);
            #endregion

            // Header Button Action
            Button headerButton = header.GetComponent<Button>();
            headerButton.onClick.AddListener(delegate
            {
                headerRedTriangle.SetActive(visible);
                visible = !visible;
                headerGreenTriangle.SetActive(visible);
                itemContent.gameObject.SetActive(visible);       
            });

            itemContent.gameObject.SetActive(visible);
        }

        public void UpdateValue(string optionTitle, object newValue)
        {
            foreach(ChangeableOption option in options)
            {
                if (option.title == optionTitle)
                {
                    option.SetValue(newValue);
                    break;
                }
            }
        }

        public void Save()
        {
            foreach (ChangeableOption option in options)
            {
                option.Save();
            }
        }
    }
}
