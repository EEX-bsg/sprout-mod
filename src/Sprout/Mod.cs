using System;
using Modding;
using UnityEngine;
using Sprout;

namespace Sprout
{
	public class Mod : ModEntryPoint
	{
        public static string DataDirPath = Application.dataPath + "/Mod/Data/Sprout_2960c24f-cc96-4ea7-adb6-8f46d2f505d9/";
        public static string AssetsDirName = "Assets/";
        public static string TmpDirPath = "tmp/";
        public static string TmpResourcesDirPath = TmpDirPath + "Resources/";

        //���O�֌W
        public static void Log(string msg)
        {
            Debug.Log("Sprout:" + msg);
        }
        public static void Log(double val)
        {
            Log(Convert.ToString(val));
        }
        public static void Warning(string msg)
        {
            Debug.LogWarning("Sprout:" + msg);
        }
        public static void Error(string msg)
        {
            Debug.LogError("Sprout:" + msg);
        }


		public override void OnLoad()
        {
            GameObject sproutController = GameObject.Find("SproutController");
            if (!sproutController)
            {
                UnityEngine.Object.DontDestroyOnLoad(sproutController = new GameObject("SproutController"));
            }
            sproutController.AddComponent<SproutCore>();
            // sproutController.AddComponent<Dev>();//とりあえずここで...
            // sproutController.AddComponent<UI.SproutSettingUI>();//いる?
        }

        public override void OnEntityPrefabCreation(int entityId, GameObject prefab)
        {
            switch (entityId)
            {
                case 1:
                    if(prefab.GetComponent<AssetHolderController>() == null)
                    {
                        prefab.AddComponent<AssetHolderController>();
                    }
                    break;
                case 2:
                    if(prefab.GetComponent<LocationMarkerController>() == null)
                    {
                        prefab.AddComponent<LocationMarkerController>();
                    }
                    break;
            }
        }
	}
}
