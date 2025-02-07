using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using UnityEngine;
using Modding;
using Sprout;

namespace Sprout
{
    class FileIO
    //あんまりFileIOという名前は適切でないかもしれない
    {
        public static AssetBundle ReadAssetBundleAsync(string path)
        {
            //そのうち非同期処理化する
            Mod.Warning("This is method in dev.");
            return ReadAssetBundle(path);
        }

        public static AssetBundle ReadAssetBundle(string path) {
            if (!Modding.ModIO.ExistsFile(path, data: true))
            {
                Mod.Error("Not found  " + path);
                return null;
            }
            try
            {
                byte[] assetBundleRaw = Modding.ModIO.ReadAllBytes(path, data: true);
                AssetBundle assetBundle = AssetBundle.LoadFromMemory(assetBundleRaw);
                return assetBundle;
            }
            catch(Exception e)
            {
                Mod.Error("ReadAssetBundle in FileIO");
                Debug.LogError(e);
                return null;
            }
        }

        public static String ReadAssetBundleAsBase64(string path)
        {
            if (!Modding.ModIO.ExistsFile(path, data: true))
            {
                Mod.Error("Not found  " + path);
                return "";
            }
            try
            {
                byte[] assetBundleRaw = Modding.ModIO.ReadAllBytes(path, data: true);
                String textData = Convert.ToBase64String(assetBundleRaw);
                return textData;
            }
            catch(Exception e)
            {
                Mod.Error("ReadAssetBundleAsBase64 in FileIO");
                Debug.LogError(e);
                return "";
            }
        }
        public static AssetBundle ConvertAssetBundleFromBase64(string textData)
        {
            try{
                byte[] assetBundleRaw = Convert.FromBase64String(textData);
                AssetBundle assetBundle = AssetBundle.LoadFromMemory(assetBundleRaw);
                return assetBundle;
            }
            catch(Exception e)
            {
                Mod.Error("ConvertAssetBundleFromBase64 in FileIO");
                Debug.LogError(e);
                return null;
            }
        }
        public static void CreateDirectory(string path, bool data)
        {
            if(!Modding.ModIO.ExistsDirectory(path, data))
            {
                Modding.ModIO.CreateDirectory(path, data);
            }
        }
    }
}
