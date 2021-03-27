using System;
using UnityEngine;
using UnityEngine.UI;

namespace ModSettingsUI.Components
{
    abstract class ChangeableOption
    {
        public string title;
        protected object value;
        protected object newValue;

        protected SettingsSavedCallback callback;
        public ChangeableOption(string title,object initialValue, SettingsSavedCallback callback)
        {
            this.title = title;
            this.value = initialValue;
            this.newValue = initialValue;
            this.callback = callback;
        }
        public void Save()
        {
            value = newValue;
            callback(value);
        }
        public void SetValue(object newVal)
        {
            value = newVal;
            newValue = newVal;
        }
        public abstract void Render(string objectName, string sectionTitle, Transform container);

        protected GameObject RenderLocalContainer(string objectName, string sectionTitle, Transform container)
        {
            GameObject localContainer = new GameObject(objectName + "_ListItem_" + sectionTitle + "_Content_" + title, new Type[4]
            {
                typeof(RectTransform),
                typeof(HorizontalLayoutGroup),
                typeof(ContentSizeFitter),
                typeof(LayoutElement)
            });
            localContainer.transform.SetParent(container);

            HorizontalLayoutGroup localContainerHorizontalLayoutGroup = localContainer.GetComponent<HorizontalLayoutGroup>();
            localContainerHorizontalLayoutGroup.childAlignment = TextAnchor.MiddleLeft;
            localContainerHorizontalLayoutGroup.childControlHeight = true;
            localContainerHorizontalLayoutGroup.childControlWidth = true;
            localContainerHorizontalLayoutGroup.childForceExpandHeight = false;
            localContainerHorizontalLayoutGroup.childForceExpandWidth = false;
            localContainerHorizontalLayoutGroup.spacing = 15;
            

            ContentSizeFitter localContainerContentSizeFitter = localContainer.GetComponent<ContentSizeFitter>();
            localContainerContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            localContainerContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            LayoutElement localContainerLayoutElement = localContainer.GetComponent<LayoutElement>();
            localContainerLayoutElement.flexibleWidth = 1f;

            return localContainer;
        }
    }
}
