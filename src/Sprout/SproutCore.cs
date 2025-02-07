using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Modding;
using Sprout;
using UnityEngine.SceneManagement;

namespace Sprout
{
    //modの中心的なこと?
    class SproutCore : MonoBehaviour
    {
        public GameObject SproutCustomLevel;
        public void Awake()
        {
            StartCoroutine(CheckVersion());//バージョン表示呼び出し
            SceneManager.activeSceneChanged += OnSceneChanged;
            if(SceneManager.GetActiveScene().buildIndex == 11)//マルチプレイヤーシーン対策(シーンチェンジ後にmod起動される為)
            {
                OnSceneChanged(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
            }
        }
        private void OnSceneChanged(Scene arg0, Scene arg1)
        {
            Debug.Log("SceneChange");
            //レベルエディタ、マルチロビー、マルチの時に、SproutCustomLevelを生成
            if (SceneManager.GetActiveScene().buildIndex == 11)
            {
                Debug.Log("Add SproutCustomLevel object");
                SproutCustomLevel = GameObject.Find("SproutCustomeLevel");
                if (!SproutCustomLevel)
                {
                    SproutCustomLevel = new GameObject("SproutCustomeLevel");
                    SproutCustomLevel.AddComponent<LevelManager>();
                }
            }

        }
        private IEnumerator CheckVersion()//コンソールへのバージョン表示
        {
            yield return new WaitForSeconds(1f);
            Mod.Log("Version" + Mods.GetVersion(new Guid("2960c24f-cc96-4ea7-adb6-8f46d2f505d9")));
        }

    }
}
