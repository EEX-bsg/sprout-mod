using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modding;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Sprout;
using Sprout.Language;

namespace Sprout.UI
{

    class SproutSettingUI : MonoBehaviour
    {
        public LevelManager levelObjectManager;
        public ModKey UIkey;
        public UnityEvent OnLoadButtonClick = new UnityEvent();
        public int WindowId { get; } = ModUtility.GetWindowId();
        private Rect _windowRect = new Rect(20, 100, 250, 310);
        private bool _isTabKeyPressed = false;
        private bool _isUIKeyPressed = false;

        void Awake()
        {
            UIkey = ModKeys.GetKey("setting-ui-key");
            // OnLoadButtonClick.AddListener(()=> { levelObjectManager.LoadLevelData(); });
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _isTabKeyPressed = !_isTabKeyPressed;
            }
            if (UIkey.IsPressed && !_isTabKeyPressed && !StatMaster.isMainMenu && !StatMaster.inMenu && (SceneManager.GetActiveScene().buildIndex == 9))
            {
                _isUIKeyPressed = !_isUIKeyPressed;
            }
        }
        void FixedUpdate()
        {

        }
        void OnGUI()
        {

            if(_isUIKeyPressed && !_isTabKeyPressed && !StatMaster.isMainMenu && !StatMaster.inMenu && (SceneManager.GetActiveScene().buildIndex == 9))
            {
                _windowRect = GUI.Window(WindowId, _windowRect, SettingUIWindow, LanguageManager.SproutSettingUI);
            }

        }
        private void SettingUIWindow(int id)
        {
            if (GUI.Button(new Rect(25, 25, 200, 20), "load"))//仮置き　多分消える
            {
                OnLoadButtonClick?.Invoke();
            }
            GUI.DragWindow();
        }
    }
}
