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
    /// <summary>
    /// アセットバンドルデータを保持するオブジェクト
    /// </summary>
    class AssetHolderController : MonoBehaviour
    {
        public readonly int windowId = ModUtility.GetWindowId();
        public MToggle OpenDirectoryButton;
        public MToggle ApplyButton;
        public MText FileName;
        public MText AssetData;
        public MText AssetDataUnix;
        private bool openUI = false;
        private bool isUnix = false;
        private string fileName = "";
        private GenericEntity GE;
        private LevelManager levelManager;
        private GameObject visualController;
        private SphereCollider sphereCollider;
        private Rect windowRect = new Rect(20, 100, 280, 150);

        void Awake()
        {
            GE = GetComponent<GenericEntity>();
            sphereCollider = base.transform.GetComponentInChildren<SphereCollider>();
            levelManager = GameObject.Find("SproutCustomeLevel").GetComponent<LevelManager>();
            levelManager.OnLevelLoading += OnLevelLoading;
            AddMapper();
        }
        void Update()
        {
            transform.localScale = new Vector3(1,1,1);
            if(StatMaster.levelSimulating)
            {
                sphereCollider.enabled = false;
            }
            else
            {
                sphereCollider.enabled = true;
            }
        }
        void Start()
        {
            visualController = new GameObject("IconVisual");
            visualController.AddComponent<LevelObjectVisual>().SetTexture(ModResource.GetTexture("seed_tex"));
            visualController.transform.position = transform.position;
            visualController.transform.SetParent(transform);

            ApplyButton.Toggled += ApplyButton_Toggled;
            OpenDirectoryButton.Toggled += OpenDirectoryButton_Toggled;
            fileName = FileName.Value;
        }
        void OnDestroy()
        {
            levelManager.OnLevelLoading -= OnLevelLoading;
            Destroy(visualController);
            visualController = null;
        }
        void OnGUI()
        {
            if(openUI)
            {
                windowRect = GUI.Window(windowId, windowRect, delegate (int windowId) {
                    GUI.Label(new Rect(25, 30, 230, 20), "Plase input the AssetBundle name");
                    fileName = GUI.TextField(new Rect(25, 60, 230, 20), fileName);
                    isUnix = GUI.Toggle(new Rect(25, 90, 230, 20), isUnix, "Mac/Unix");
                    if (GUI.Button(new Rect(25, 120, 230, 20), "Apply"))
                    {
                        FileName.Value = fileName;
                        LoadAssets(isUnix);
                        openUI = false;
                    }
                    GUI.DragWindow();
                }, "AssetBundle Name");
            }
        }
        public void OnLevelLoading()
        {
            if(CheckAssetExist())
            {
                if(Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    levelManager.SetAssetBundle(AssetData.Value);//Windows用
                }
                else
                {
                    levelManager.SetAssetBundle(AssetDataUnix.Value);//Unix系
                }
            }
        }
        private bool CheckAssetExist()
        {
            return (AssetData.Value != "");
        }
        private void ApplyButton_Toggled(bool flag)
        {
            //専用UIでファイル名を指定させる予定
            if(flag)
            {
                openUI = true;
                ApplyButton.SetValue(false);
            }
        }
        private void LoadAssets(bool platform)
        {
            //platform=trueでMac falseでWindows
            if(!platform)
            {
                AssetData.Value = FileIO.ReadAssetBundleAsBase64(Mod.AssetsDirName + FileName.Value);//Windows用
            }
            else
            {
                AssetDataUnix.Value = FileIO.ReadAssetBundleAsBase64(Mod.AssetsDirName + FileName.Value);//Unix系
            }
            OnLevelLoading();
        }
        private void OpenDirectoryButton_Toggled(bool flag)
        {
            if(flag)
            {
                FileIO.CreateDirectory(Mod.AssetsDirName, true);
                Modding.ModIO.OpenFolderInFileBrowser(Mod.AssetsDirName, true);
                OpenDirectoryButton.SetValue(false);
            }
        }
        private void AddMapper()
        {
            OpenDirectoryButton = GE.AddToggle("Open Directory", "open-dict-button", false);
            ApplyButton = GE.AddToggle("Load AssetBundle", "apply-button", false);
            FileName = GE.AddText("File Name", "file-name", "");
            AssetData = GE.AddText("AssetData", "asset-data", "");
            AssetDataUnix = GE.AddText("AssetDataUnix", "asset-data-unix", "");
            AssetData.DisplayInMapper = false;
            AssetDataUnix.DisplayInMapper = false;
        }
    }
}
