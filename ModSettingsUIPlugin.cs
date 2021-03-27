using System;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using ModSettingsUI.Util;

namespace ModSettingsUI
{
    [BepInPlugin(pluginGUID, pluginName, pluginVersion)]
    public class ModSettingsUIPlugin : BaseUnityPlugin
    {
        const string pluginGUID = "nonamepaul.plugins.modsettingsui";
        const string pluginName = "Mod Settings UI";
        const string pluginVersion = "1.0.0";
        internal readonly Assembly assembly;
        public static ManualLogSource logger;
        public static Harmony harmony;

        public static Sprite rightRedTriangle;
        public static Sprite downGreenTriangle;

        
        public ModSettingsUIPlugin()
        {
            assembly = Assembly.GetExecutingAssembly();
        }
        void Awake()
        {
            logger = Logger;
            harmony = new Harmony(pluginGUID);

            GameObject img2Sprite = new GameObject("IMG2Sprite", typeof(IMG2Sprite));
            rightRedTriangle = IMG2Sprite.instance.LoadNewSprite(Paths.BepInExRootPath + "\\plugins\\ModSettingsUI\\Assets\\right.png");
            downGreenTriangle = IMG2Sprite.instance.LoadNewSprite(Paths.BepInExRootPath + "\\plugins\\ModSettingsUI\\Assets\\down.png");

            try
            {
                harmony.PatchAll(assembly);
            } catch(Exception e)
            {
                Debug.Log(e.Message);
            }

        }
        public void OnDestroy()
        {
            logger.LogInfo("Destroying...");
            ModSettingsUI.Destroy();
            harmony.UnpatchAll(pluginGUID);
        }
        
    }    
}