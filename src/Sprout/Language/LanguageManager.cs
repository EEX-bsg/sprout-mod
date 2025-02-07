using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modding;
using Localisation;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sprout;

namespace Sprout.Language
{

    class LanguageManager : MonoBehaviour
    {
        public static string CurrLangName = SingleInstance<LocalisationManager>.Instance.currLangName;
        public static string SproutSettingUI
        {
            get
            {
                switch (CurrLangName)
                {
                    case "日本語":
                        return "Sprout設定画面";
                    default:
                        return "SproutSettingUI";
                }
            }
        }
    }
}
