using FruitScapes.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitScapes.MapController
{
    public class ObjectFinder : MonoBehaviour
    {
        public Empty GetLastEmpty(int row, GameObject[,] allObjects)
        {
            Empty lastEmpty = null;
            bool iFind = false;
            for (int i = 0; i < allObjects.GetLength(0); i++)
            {
                Empty tempEmty = allObjects[i, row].GetComponent<Empty>();
                if (tempEmty != null && iFind == false)
                {
                    lastEmpty = tempEmty;
                    iFind = true;
                }
                // TODO: Сделать так что если наткнулся на блокировщик гемов обнулить IEMTY
            }
            return lastEmpty;
        }

        public Movable GetLastMovable(int row, GameObject[,] allObjects)
        {
            Movable movableToReturn = null;
            for (int i = allObjects.GetLength(0) - 1; i > 0; i--)
            {
                if (allObjects[i - 1, row].GetComponent<Empty>() != null && allObjects[i, row].GetComponent<Movable>() != null && allObjects[i, row].GetComponent<Empty>() == null)
                    movableToReturn = allObjects[i, row].GetComponent<Movable>();

            }
            return movableToReturn;
        }

        public List<Movable> GetDiogonalsPair(GameObject[,] allObjects)
        {
            List<Movable> fruits = new List<Movable>();
            for (int i = allObjects.GetLength(0) - 1; i > 0; i--)
                for (int j = 0; j < allObjects.GetLength(1); j++)
                {
                    Movable _movable = allObjects[i, j].GetComponent<Movable>();
                    if (_movable != null && allObjects[i, j].GetComponent<Empty>() == null)
                    {
                        if (allObjects[i - 1, j].GetComponent<Empty>() != null)
                        {
                            return null;
                        }
                        Empty _empty = _movable.GetEmptyNeighboor(new Vector2Int(1, -1), allObjects);
                        if (_movable.GetObjectNeighboor(Vector2Int.right, allObjects) == null && _empty != null)
                        {
                            fruits.Add(_movable);
                            fruits.Add(_empty.GetComponent<Movable>());
                            return fruits;
                        }
                        _empty = _movable.GetEmptyNeighboor(new Vector2Int(-1, -1), allObjects);
                        if (_movable.GetObjectNeighboor(Vector2Int.left, allObjects) == null && _empty != null)
                        {
                            fruits.Add(_movable);
                            fruits.Add(_empty.GetComponent<Movable>());
                            return fruits;
                        }
                    }
                }
            return null;
        }
    }
}
