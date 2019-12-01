using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FruitScapes.Components.Basic;

namespace FruitScapes.Components
{
    public class MapObject : MonoBehaviour, IObject
    {
        public void SetSprite(Sprite newSprite)
        {
            GetComponent<SpriteRenderer>().sprite = newSprite;
        }
    }
}
