using System;
using UnityEngine;
using UnityEngine.UI;

namespace ModSettingsUI.Components
{
    class Checkbox : ChangeableOption
    {        
        public Checkbox(string title,object initialValue, SettingsSavedCallback callback) : base(title,initialValue, callback)
        {
        }

        public override void Render(string objectName, string sectionTitle, Transform container)
        {
            GameObject localContainer = RenderLocalContainer(objectName, sectionTitle, container);

            GameObject toggle = new GameObject(objectName + "_ListItem_" + sectionTitle + "_Content_" + title + "_Checkbox", new Type[3]
            {
                typeof(RectTransform),
                typeof(Toggle),
                typeof(LayoutElement)
            });
            toggle.transform.SetParent(localContainer.transform);

            RectTransform toggleRectTransform = toggle.GetComponent<RectTransform>();
            toggleRectTransform.offsetMin = new Vector2(0f, 0f);
            toggleRectTransform.offsetMax = new Vector2(0f, 0f);
            toggleRectTransform.anchorMin = new Vector2(0f, 0f);
            toggleRectTransform.anchorMax = new Vector2(0f, 1f);

            LayoutElement toggleLayoutElement = toggle.GetComponent<LayoutElement>();
            toggleLayoutElement.preferredHeight = 25f;
            toggleLayoutElement.preferredWidth = 100f;
            toggleLayoutElement.flexibleWidth = 0f;

           

            GameObject background = GameObject.Instantiate(ModSettingsUI.refToggle.transform.Find("Background").gameObject);
            background.transform.SetParent(toggle.transform);
            
            RectTransform backgroundRectTransform = background.GetComponent<RectTransform>();
            backgroundRectTransform.offsetMin = new Vector2(0f, 0f);
            backgroundRectTransform.offsetMax = new Vector2(0f, 0f);
            backgroundRectTransform.anchorMin = new Vector2(0.375f, 0f);
            backgroundRectTransform.anchorMax = new Vector2(0.625f, 1f);
            

            Image backgroundImage = background.GetComponent<Image>();

            Image checkmarkImage = background.transform.Find("Checkmark").GetComponent<Image>();

            Toggle toggleToggle = toggle.GetComponent<Toggle>();
            toggleToggle.targetGraphic = backgroundImage;
            toggleToggle.graphic = checkmarkImage;
            toggleToggle.isOn = (bool)value;
            toggleToggle.onValueChanged.AddListener(delegate
            {
                try
                {
                    newValue = toggleToggle.isOn;
                } catch (FormatException e)
                {
                    newValue = false;
                }
            });
            
            GameObject label = new GameObject(objectName + "_ListItem_" + sectionTitle + "_Content_" + title + "_CheckboxTitle", new Type[3]
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
