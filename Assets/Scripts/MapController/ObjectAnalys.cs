using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FruitScapes.Components;
using FruitScapes.Tiles;
using FruitScapes.Events;


namespace FruitScapes.MapController
{
    public class ObjectAnalys
    {
        public bool FindMatches(GameObject[,] allObjects, bool onlycheck = false)
        {
            for (int i = 0; i < allObjects.GetLength(0); i++)
            {
                for (int j = 0; j < allObjects.GetLength(1); j++)
                {
                    Combine middleCombine = allObjects[i, j].GetComponent<Combine>();
                    if (middleCombine == null)
                        continue;
                    Combine leftGemCode = middleCombine.GetNeighborCombine(Vector2Int.left, allObjects);
                    Combine rightGemCode = middleCombine.GetNeighborCombine(Vector2Int.right, allObjects);
                    Combine upGemCode = middleCombine.GetNeighborCombine(Vector2Int.up, allObjects);
                    Combine downGemCode = middleCombine.GetNeighborCombine(Vector2Int.down, allObjects);

                    if (leftGemCode != null && rightGemCode != null)
                        if (middleCombine.Id == leftGemCode.Id && middleCombine.Id == rightGemCode.Id)
                        {
                            if (onlycheck) return true;
                            middleCombine.GetDamage();
                            leftGemCode.GetDamage();
                            rightGemCode.GetDamage();
                        }
                    if (upGemCode != null && downGemCode != null)
                        if (middleCombine.Id == upGemCode.Id && middleCombine.Id == downGemCode.Id)
                        {
                            if (onlycheck) return true;
                            middleCombine.GetDamage();
                            upGemCode.GetDamage();
                            downGemCode.GetDamage();
                        }
                }
            }

            return false;
        }

        public IEnumerator GemsDestroy(GameObject[,] allObjects, float destroyTime)
        {
            TilesController tilesControll = new TilesController();
            yield return new WaitForSeconds(destroyTime + 0.01f);
            for (int i = 0; i < allObjects.GetLength(0); i++)
            {
                for (int j = 0; j < allObjects.GetLength(1); j++)
                {
                    Combine combinedFruit = allObjects[i, j].GetComponent<Combine>();
                    if (combinedFruit != null)
                    {
                        if (combinedFruit.iDie)
                        {
                            Vector2 fruitPos = allObjects[i, j].transform.position;
                            Object.Destroy(allObjects[i, j]);
                            allObjects[i, j] = tilesControll.CreateEmpty(fruitPos);
                            allObjects[i, j].GetComponent<Movable>().SetColRow(i, j);
                        }
                    }
                }
            }
            EventHolder.destroyEvent.Invoke();
            yield break;
        }
    }
}
