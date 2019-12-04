using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FruitScapes.MapController;
using FruitScapes.Components;

namespace FruitScapes.Hint
{
    public class HintManager : MonoBehaviour
    {
        [SerializeField]
        private ObjectMover mover;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                FindBigHint();
        }


        private void FindLowHint()
        {
            GameObject[,] allObjects = mover.GetAllObjects();
            for (int i = 0; i < allObjects.GetLength(0); i++)
                for (int j = 0; j < allObjects.GetLength(1); j++)
                {

                    Movable curMovable = allObjects[i, j].GetComponent<Movable>();
                    if (curMovable == null)
                        continue;
                    Movable rightMovable = curMovable.GetNeighborMovable(Vector2Int.right, allObjects);
                    Movable upMovable = curMovable.GetNeighborMovable(Vector2Int.up, allObjects);
                    if (rightMovable != null)
                    {
                        if (TryAnimateHint(curMovable, rightMovable, allObjects, false) == true)
                            return;
                    }
                    if (upMovable != null)
                    {
                        if (TryAnimateHint(curMovable, upMovable, allObjects, false) == true)
                            return;
                    }

                }
        }

        private void FindBigHint()
        {
            GameObject[,] allObjects = mover.GetAllObjects();
            for (int i = 0; i < allObjects.GetLength(0); i++)
                for (int j = 0; j < allObjects.GetLength(1); j++)
                {

                    Movable curMovable = allObjects[i, j].GetComponent<Movable>();
                    if (curMovable == null)
                        continue;
                    Movable rightMovable = curMovable.GetNeighborMovable(Vector2Int.right, allObjects);
                    Movable upMovable = curMovable.GetNeighborMovable(Vector2Int.up, allObjects);
                    if (rightMovable != null)
                    {
                        if (TryAnimateHint(curMovable, rightMovable, allObjects, true) == true)
                            return;
                    }
                    if (upMovable != null)
                    {
                        if (TryAnimateHint(curMovable, upMovable, allObjects, true) == true)
                            return;
                    }

                }
        }

        private List<Combine> GetPotentialMatchs(Movable fruit1, Movable fruit2, GameObject[,] allObjects, int matchCount)
        {
            ObjectFinder finder = new ObjectFinder();
            mover.ChangeFruits(fruit1, fruit2, false, true);
            List<Combine> matchList = finder.GetCombinedObjects(allObjects);
            int different = 0;
            if (matchList.Count > 0)
            {
                for (int i = 1; i < matchList.Count; i++)
                {
                    if (matchList[0].Id != matchList[i].Id)
                        different++;
                }
                if (different > (matchList.Count / 2))
                {
                    for (int i = 1; i < matchList.Count; i++)
                    {
                        if (matchList[0].Id == matchList[i].Id)
                        {
                            matchList.RemoveAt(i);
                            i--;
                        }
                    }
                    matchList.RemoveAt(0);
                }
                else
                {
                    for (int i = 1; i < matchList.Count; i++)
                    {
                        if (matchList[0].Id != matchList[i].Id)
                        {
                            matchList.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }

            if (matchList.Count > matchCount)
            {
                mover.ChangeFruits(fruit1, fruit2, false, true);
                return matchList;
            }
            mover.ChangeFruits(fruit1, fruit2, false, true);
            return null;
        }

        private bool TryAnimateHint(Movable curMovable, Movable otherMovable, GameObject[,] allObjects, bool findBigMatch)
        {
            int maxMatch;
            if (findBigMatch)
                maxMatch = 3;
            else
                maxMatch = 0;
           
            List<Combine> combineList = GetPotentialMatchs(curMovable, otherMovable, allObjects, maxMatch);
            if (combineList == null)
                combineList = GetPotentialMatchs(curMovable, otherMovable, allObjects, maxMatch);
            if (combineList != null)
            {
                foreach (Combine combine in combineList)
                    Destroy(combine.gameObject);
                return true;
            }
            return false;
        }
    }
}
