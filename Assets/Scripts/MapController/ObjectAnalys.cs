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
        public void MakeDamage(List<Combine> matchesList)
        {
            foreach (Combine match in matchesList)
                match.GetDamage();
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
