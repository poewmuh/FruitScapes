using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FruitScapes.MapController;
using FruitScapes.Components;
using FruitScapes.Events;

namespace FruitScapes.Hint
{
    public class HintManager : MonoBehaviour
    {
        [SerializeField]
        private ObjectMover mover;
        [SerializeField]
        private float timeToHint;
        private float timer = 0;

        private void Start()
        {
            EventHolder.destroyEvent.AddListener(ClearTimer);
        }


        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > timeToHint)
            {
                if (TryFindHint(true) == false)
                    TryFindHint(false);
                timer = 0;
            }
        }

        private void ClearTimer()
        {
            timer = 0;
        }


        private bool TryFindHint(bool isBigHint)
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
                        if (TryAnimateHint(curMovable, rightMovable, Vector2Int.right, allObjects, isBigHint) == true)
                            return true;
                    }
                    if (upMovable != null)
                    {
                        if (TryAnimateHint(curMovable, upMovable, Vector2Int.up, allObjects, isBigHint) == true)
                            return true;
                    }

                }
            return false;
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

        private bool TryAnimateHint(Movable curMovable, Movable otherMovable, Vector2Int dir, GameObject[,] allObjects, bool findBigMatch)
        {
            int maxMatch;
            if (findBigMatch)
                maxMatch = 3;
            else
                maxMatch = 0;

            List<Combine> combineList = GetPotentialMatchs(curMovable, otherMovable, allObjects, maxMatch);
            if (combineList != null)
            {
                if (combineList.Contains(curMovable.GetComponent<Combine>()))
                {
                    if (dir == Vector2Int.up)
                        curMovable.GetComponent<Animeted>().HintUp();
                    else
                        curMovable.GetComponent<Animeted>().HintRight();
                }
                else
                {
                    if (dir == Vector2Int.up)
                        otherMovable.GetComponent<Animeted>().HintDown();
                    else
                        otherMovable.GetComponent<Animeted>().HintLeft();
                }

                foreach (Combine combine in combineList)
                {
                    if (combine.gameObject == curMovable.gameObject)
                    {
                        continue;
                    }
                    combine.GetComponent<Animeted>().HintScale();
                }


                return true;
            }
            return false;
        }
    }
}
