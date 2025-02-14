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
    class LocationMarkerController : MonoBehaviour
    {
        public readonly int windowId = ModUtility.GetWindowId();
        private bool openUI = false;
        private string prefabPath = "";
        public GameObject LevelObject;
        private GameObject visualController;
        private MText PrefabPath;
        public MToggle LoadButton;
        private GenericEntity GE;
        private LevelManager levelManager;
        private Rect windowRect = new Rect(20, 100, 280, 120);
        private SphereCollider sphereCollider;
        void Awake()
        {
            GE = GetComponent<GenericEntity>();
            sphereCollider = base.transform.GetComponentInChildren<SphereCollider>();
            levelManager = GameObject.Find("SproutCustomeLevel").GetComponent<LevelManager>();
            levelManager.OnAssetLoaded += OnAssetLoaded;
            AddMapper();
        }
        void Start()
        {
            LoadButton.Toggled += LoadButton_Toggled;
            visualController = new GameObject("IconVisual");
            visualController.AddComponent<LevelObjectVisual>().SetTexture(ModResource.GetTexture("pin_tex"));
            visualController.transform.position = transform.position;
            visualController.transform.SetParent(transform);
            if(levelManager.assetBundle != null)
            {
                OnAssetLoaded();
            }
            prefabPath = PrefabPath.Value;
        }
        void Update()
        {
            if(StatMaster.levelSimulating)
            {
                sphereCollider.enabled = false;
            }
            else
            {
                sphereCollider.enabled = true;
            }
            if(LevelObject == null)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        void OnDestroy()
        {
            levelManager.OnAssetLoaded -= OnAssetLoaded;
            Destroy(LevelObject);
            LevelObject = null;
            Destroy(visualController);
            visualController = null;
        }
        void OnGUI()
        {
            if(openUI)
            {
                windowRect = GUI.Window(windowId, windowRect, delegate (int windowId) {
                    GUI.Label(new Rect(25, 30, 230, 20), "Plase input the Asset(Prefab) name");
                    prefabPath = GUI.TextField(new Rect(25, 60, 230, 20), prefabPath);
                    if (GUI.Button(new Rect(25, 90, 230, 20), "Apply"))
                    {
                        PrefabPath.Value = prefabPath;
                        OnAssetLoaded();
                        openUI = false;
                    }
                    GUI.DragWindow();
                }, "Asset(Prefab) Name");
            }
        }
        private void OnAssetLoaded()
        {
            if(PrefabPath.Value == "") return;
            if(LevelObject != null)
            {
                Destroy(LevelObject);
                LevelObject = null;
            }
            AssetBundle assetBundle = levelManager.assetBundle;
            try
            {
                LevelObject = Instantiate(assetBundle.LoadAsset<GameObject>(PrefabPath.Value));
            }
            catch(Exception e)
            {
                Mod.Error("No assets found in " + PrefabPath.Value);
                Debug.LogError(e);
                visualController.SetActive(true);
                return;
            }
            LevelObject.transform.position = this.transform.position;
            LevelObject.transform.rotation = this.transform.rotation;
            LevelObject.transform.localScale = this.transform.localScale;
            LevelObject.transform.SetParent(this.transform.Find("Pointer"));
            if(visualController) visualController.SetActive(false);
        }
        private void LoadButton_Toggled(bool flag)
        {
            if(flag)
            {
                openUI = true;
                LoadButton.SetValue(false);
            }
        }
        private void AddMapper()
        {
            LoadButton = GE.AddToggle("Load Prefab", "load-prefab", false);
            PrefabPath = GE.AddText("Prefab Path", "prefab-path", "");
        }
    }
}
