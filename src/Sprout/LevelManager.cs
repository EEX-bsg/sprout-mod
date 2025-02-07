using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Modding;
using Modding.Levels;
using Sprout;

namespace Sprout
{
    class LevelManager : MonoBehaviour
    {
        public AssetBundle assetBundle;
        public Action OnLevelLoading;
        public Action OnAssetLoaded;
        private bool isAssetLoaded = false;
        public void Awake()
        {
            Events.OnLevelLoaded += OnLevelLoaded;
        }
        public void Update()
        {
            if(assetBundle != null && !isAssetLoaded)
            {
                isAssetLoaded = true;
                if(OnAssetLoaded != null)
                {
                    OnAssetLoaded.Invoke();
                }
            }
        }
        public void OnLevelLoaded(Level level)
        {
            UnloadAssetBundle();
            if(OnLevelLoading != null)
            {
                OnLevelLoading.Invoke();//アセットバンドルのロード
                isAssetLoaded = false;
            }
        }
        public void OnDestroy()
        {
            Events.OnLevelLoaded -= OnLevelLoaded;
            UnloadAssetBundle();
        }

        /// <summary>
        /// アセットバンドルのメモリ解放
        /// </summary>
        public void UnloadAssetBundle()
        {
            if(assetBundle == null) return;
            assetBundle.Unload(true);
            assetBundle = null;
        }

        /// <summary>
        /// アセットバンドルをセットする
        /// returnがtrueなら成功。falseで失敗。
        /// </summary>
        public bool SetAssetBundle(string text)
        {
            if(assetBundle != null)//既にアセットバンドルが存在する場合は除外。
            {
                return false;
            }
            try
            {
                assetBundle = FileIO.ConvertAssetBundleFromBase64(text);
                return true;
            }
            catch(Exception e)
            {
                Mod.Error("Failed to create asset bundle from blv.");
                Debug.LogError(e);
                return false;
            }
        }
    }
}
