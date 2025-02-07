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
    class Dev : MonoBehaviour
    {
        public void Awake()
        {
            Debug.Log("Dev");
            for(int i=0; i<SceneManager.sceneCount; i++)
            {
                Scene n = SceneManager.GetSceneAt(i);
                Debug.Log($"[SceneManager] Scene {n.buildIndex}/{n.name}");
            }
        }
        public void Update()
        {

        }
    }
}
