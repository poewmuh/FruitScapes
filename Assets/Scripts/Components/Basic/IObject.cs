using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FruitScapes.Components.Basic
{
    public interface IObject
    {
        void SetSprite(Sprite newSprite);
        void SetColRow(int col, int row, GameObject[,] allObjects = null);
    }
}
