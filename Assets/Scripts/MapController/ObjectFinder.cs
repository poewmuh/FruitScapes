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
            for (int i = 0; i < allObjects.GetLength(0); i++)
            {
                Empty tempEmty = allObjects[i, row].GetComponent<Empty>();
                if (tempEmty != null)
                {
                    lastEmpty = tempEmty;
                    break;
                }
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

        public List<Combine> GetCombinedObjects(GameObject[,] allObjects)
        {
            List<Combine> matchesList = new List<Combine>();

            for (int i = 0; i < allObjects.GetLength(0); i++)
            {
                for (int j = 0; j < allObjects.GetLength(1); j++)
                {
                    Combine middleCombine = allObjects[i, j].GetComponent<Combine>();
                    if (middleCombine == null)
                        continue;
                    Combine leftCombine = middleCombine.GetNeighborCombine(Vector2Int.left, allObjects);
                    Combine rightCombine = middleCombine.GetNeighborCombine(Vector2Int.right, allObjects);
                    Combine upCombine = middleCombine.GetNeighborCombine(Vector2Int.up, allObjects);
                    Combine downCombine = middleCombine.GetNeighborCombine(Vector2Int.down, allObjects);

                    if (leftCombine != null && rightCombine != null)
                        if (middleCombine.Id == leftCombine.Id && middleCombine.Id == rightCombine.Id)
                        {
                            if (!matchesList.Contains(middleCombine))
                                matchesList.Add(middleCombine);
                            if (!matchesList.Contains(leftCombine))
                                matchesList.Add(leftCombine);
                            if (!matchesList.Contains(rightCombine))
                                matchesList.Add(rightCombine);
                        }
                    if (upCombine != null && downCombine != null)
                        if (middleCombine.Id == upCombine.Id && middleCombine.Id == downCombine.Id)
                        {
                            if (!matchesList.Contains(middleCombine))
                                matchesList.Add(middleCombine);
                            if (!matchesList.Contains(upCombine))
                                matchesList.Add(upCombine);
                            if (!matchesList.Contains(downCombine))
                                matchesList.Add(downCombine);
                        }
                }
            }

            return matchesList;
        }
    }
}
