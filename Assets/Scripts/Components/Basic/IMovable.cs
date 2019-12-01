using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FruitScapes.Components.Basic
{
    public interface IMovable
    {
        int GetCol { get; }
        int GetRow { get; }
        bool CanFit(Vector2Int dir, GameObject[,] allObjects);
        void SetColRow(int col, int row, GameObject[,] allObjects = null);
        Movable GetNeighborMovable(Vector2Int dir, GameObject[,] allObjects);
        void LerpMeTo(Vector2 pos);
    }
}
