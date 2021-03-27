using System;
using UnityEngine;
using UnityEngine.UI;

namespace ModSettingsUI.Components
{
    class DecimalInput : ChangeableOption
    {        
        public DecimalInput(string title,object initialValue, SettingsSavedCallback callback) : base(title,initialValue, callback)
        {
        }

        public override void Render(string objectName, string sectionTitle, Transform container)
        {
            GameObject localContainer = RenderLocalContainer(objectName, sectionTitle, container);

            GameObject input = new GameObject(objectName + "_ListItem_" + sectionTitle + "_Content_" + title + "_DecimalInput", new Type[4]
            {
                typeof(RectTransform),
                typeof(Image),
                typeof(InputField),
                typeof(LayoutElement)
            });
            input.transform.SetParent(localContainer.transform);

            GameObject placeholder = new GameObject(objectName + "_ListItem_" + sectionTitle + "_Content_" + title + "_DecimalInput_Placeholder", new Type[2]
            {
                typeof(RectTransform),
                typeof(Text)
            });
            placeholder.transform.SetParent(input.transform);

            RectTransform placeholderRectTransform = placeholder.GetComponent<RectTransform>();
            placeholderRectTransform.offsetMin = new Vector2(0f, 0f);
            placeholderRectTransform.offsetMax = new Vector2(0f, 0f);
            placeholderRectTransform.anchorMin = new Vector2(0.05f, 0f);
            placeholderRectTransform.anchorMax = new Vector2(1f, 1f);

            Text placeholderText = placeholder.GetComponent<Text>();
            placeholderText.text = "Value...";
            placeholderText.font = ModSettingsUI.refTextDefault.font;
            placeholderText.fontStyle = ModSettingsUI.refTextDefault.fontStyle;
            placeholderText.fontSize = 16;
            placeholderText.alignment = TextAnchor.MiddleLeft;
            placeholderText.color = new Color(0.4f, 0.4f, 0.4f, 1f);

            GameObject text = new GameObject(objectName + "_ListItem_" + sectionTitle + "_Content_" + title + "_DecimalInput_Text", new Type[2]
            {
                typeof(RectTransform),
                typeof(Text)
            });
            text.transform.SetParent(input.transform);

            RectTransform textRectTransform = text.GetComponent<RectTransform>();
            textRectTransform.offsetMin = new Vector2(0f, 0f);
            textRectTransform.offsetMax = new Vector2(0f, 0f);
            textRectTransform.anchorMin = new Vector2(0.05f, 0f);
            textRectTransform.anchorMax = new Vector2(1f, 1f);

            Text textText = text.GetComponent<Text>();
            textText.font = ModSettingsUI.refTextDefault.font;
            textText.fontStyle = ModSettingsUI.refTextDefault.fontStyle;
            textText.fontSize = 16;
            textText.alignment = TextAnchor.MiddleLeft;
            textText.color = new Color(0f, 0f, 0f, 1f);

            LayoutElement inputLayoutElement = input.GetComponent<LayoutElement>();
            inputLayoutElement.preferredHeight = 25f;
            inputLayoutElement.preferredWidth = 100f;
            inputLayoutElement.flexibleWidth = 0f;

            InputField inputInputfield = input.GetComponent<InputField>();
            inputInputfield.contentType = InputField.ContentType.DecimalNumber;
            inputInputfield.placeholder = placeholderText;
            inputInputfield.targetGraphic = textText;
            inputInputfield.textComponent = textText;
            inputInputfield.text = value.ToString();

            inputInputfield.onValueChanged.AddListener(delegate
            {
                try
                {
                    newValue = float.Parse(inputInputfield.text);
                } catch (FormatException e)
                {
                    newValue = 0.0f;
                }
            });

            GameObject label = new GameObject(objectName + "_ListItem_" + sectionTitle + "_Content_" + title + "_DecimalInputTitle", new Type[3]
            {
                typeof(RectTransform),
                typeof(Text),
                typeof(LayoutElement)
            });
            label.transform.SetParent(localContainer.transform);

            LayoutElement labelLayoutElement = label.GetComponent<LayoutElement>();
            labelLayoutElement.preferredHeight = 25f;
            labelLayoutElement.flexibleWidth = 1f;

            Text labelText = label.GetComponent<Text>();
            labelText.text = title;
            labelText.font = ModSettingsUI.refTextAction.font;
            labelText.fontStyle = ModSettingsUI.refTextAction.fontStyle;
            labelText.fontSize = 16;
            labelText.alignment = TextAnchor.MiddleLeft;

        }
    }
}
