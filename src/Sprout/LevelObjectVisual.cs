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
    class LevelObjectVisual:MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        void Awake()
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        void Update()
        {
            if(StatMaster.levelSimulating)
            {
                spriteRenderer.enabled = false;
            }
            else
            {
                spriteRenderer.enabled = true;
            }
        }
        void LateUpdate()
        {
            Transform camera = Camera.main.transform;
            transform.rotation = camera.rotation;
        }
        public void SetTexture(Texture2D texture)
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 150f);
            spriteRenderer.sprite = sprite;
        }
    }
}
