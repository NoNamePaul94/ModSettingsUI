using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using ModSettingsUI.UIElements;
using ModSettingsUI.Components;

namespace ModSettingsUI
{
    public enum OptionType
    {
        DecimalInput = 1,
        Checkbox = 2,
    }
    public delegate void SettingsSavedCallback(object value);
	[HarmonyPatch(typeof(Settings), "Awake")]
	public class ModSettingsUI
	{
        private static MainModButton mainModButton = null;
        private static MainContainer mainContainer = null;
        private static bool initialized = false;

        public static Image refImageDefault;
        public static Image refImageAction;
        public static Text refTextAction;
        public static Text refTextDefault;

        public static GameObject refToggle;

        public const string objectNamePrefix = "ModSettingsUI_";

        private static List<Section> sections = new List<Section>();
        

		public static void Postfix(Settings __instance)
        {
			ModSettingsUIPlugin.logger.LogInfo("Injecting...");
            Transform settingsTransform = ((Component)__instance).transform;

            refImageDefault = settingsTransform.Find("panel").Find("TabButtons").Find("Misc").GetComponent<Image>();
            refImageAction = settingsTransform.Find("panel").Find("Tabs").Find("Misc").Find("Ok").GetComponent<Image>();
            refTextDefault = settingsTransform.Find("panel").Find("TabButtons").Find("Misc").Find("Text").GetComponent<Text>();
            refTextAction = settingsTransform.Find("panel").Find("Tabs").Find("Misc").Find("Ok").Find("Text").GetComponent<Text>();


            refToggle = settingsTransform.Find("panel").Find("Tabs").Find("Misc").Find("CameraShakeToggle").gameObject;


            mainContainer = new MainContainer(settingsTransform, sections);
            if (!mainContainer.Render())
            {
                ModSettingsUIPlugin.logger.LogInfo("Couldn't render mainContainer. Aborting!");
                return;
            }
            mainModButton = new MainModButton(settingsTransform, mainContainer);            
            if (!mainModButton.Render())
            {
                ModSettingsUIPlugin.logger.LogInfo("Couldn't render mainModButton. Aborting!");
                return;
            }
            mainContainer.setMainModButtonCallback(mainModButton.Click);
            initialized = true;
            ModSettingsUIPlugin.logger.LogInfo("...Done");
        }
       
        
        public static void test(object x)
        {
            ModSettingsUIPlugin.logger.LogInfo(x);
        }
        public static void Prefix(Settings __instance)
        {
            Destroy();
        }

        public static void Destroy()
        {
            if (initialized)
            {
                mainContainer.Destroy();
                mainModButton.Destroy();
                ModSettingsUIPlugin.logger.LogInfo("Destroyed instances");
            }
        }

        public static void DebugTransform(Transform _transform, int depth = 0, int currDepth = 0)
        {
            String prefix = ""; 
            for(int i = 0; i <= (currDepth*3); i++)
            {
                prefix += "-";
            }
            ModSettingsUIPlugin.logger.LogInfo(prefix + _transform);
            if (currDepth >= depth) return;

            for (int i = 0; i < _transform.childCount; i++)
            {
                Transform child = _transform.GetChild(i);
                DebugTransform(child, depth, currDepth + 1);
            }
        }

        public static void AddInputField(string sectionTitle, OptionType type, string optionTitle, object initialValue, SettingsSavedCallback callback)
        {
            Section sec = null;
            foreach(Section section in sections)
            {
                if (section.title == sectionTitle)
                {
                    sec = section;
                    break;
                }
            }
            if (sec == null)
            {
                sec = new Section(sectionTitle);
                sections.Add(sec);
            }
            sec.AddOption(type, optionTitle, initialValue, callback);            
        }

        public static void UpdateValue(string sectionTitle, string optionTitle, object newValue)
        {
            foreach (Section section in sections)
            {
                if (section.title == sectionTitle)
                {
                    section.UpdateValue(optionTitle, newValue);
                    break;
                }
            }
        }
    }
}
