﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FruitScapes.Components.Basic;
using FruitScapes.Animations;
using FruitScapes.Events;

namespace FruitScapes.Components
{
    public class Movable : MonoBehaviour, IMovable
    {
        private int _col, _row;
        private Queue<Vector2> movePosList;
        


        public int GetCol => _col;
        public int GetRow => _row;
        

        private void Awake()
        {
            movePosList = new Queue<Vector2>();
        }

        private void Start()
        {
            EventHolder.startAnimation.AddListener(AnimationStart);
        }

        private void AnimationStart(float speed)
        {
            if (movePosList.Count > 0)
            {
                StartCoroutine(AnimateMe(speed));
            }
        }


        

        private IEnumerator AnimateMe(float speed)
        {
            StartCoroutine(AllAnimations.SimpleLerp(gameObject, movePosList.Peek(), speed));
            movePosList.Dequeue();
            if (movePosList.Count > 0)
            {
                yield return new WaitForSeconds(speed + 0.01f);
                AnimationStart(speed);
            }
        }

        public bool CanFit(Vector2Int dir, GameObject[,] allObjects)
        {
            return allObjects.GetLength(0) > _col + dir.y && _col + dir.y >= 0 &&
                   allObjects.GetLength(1) > _row + dir.x && _row + dir.x >= 0 ?
                   true : false;
        }

        public void SetColRow(int col, int row, GameObject[,] allObjects = null)
        {
            _col = col;
            _row = row;

            if (allObjects != null)
                FixMe(allObjects);
        }

        public void LerpMeTo(Vector2 pos)
        {
            movePosList.Enqueue(pos);
        }

        private void FixMe(GameObject[,] allObjects)
        {
            allObjects[_col, _row] = gameObject;
        }

        public Movable GetNeighborMovable(Vector2Int dir, GameObject[,] allObjects)
        {
            if (CanFit(dir, allObjects))
                return allObjects[_col + dir.y, _row + dir.x].GetComponent<Movable>();
            return null;
        }

        public Empty GetEmptyNeighboor(Vector2Int dir, GameObject[,] allObjects)
        {
            if (CanFit(dir, allObjects))
                return allObjects[_col + dir.y, _row + dir.x].GetComponent<Empty>();
            return null;
        }

        public MapObject GetObjectNeighboor(Vector2Int dir, GameObject[,] allObjects)
        {
            if (CanFit(dir, allObjects))
                return allObjects[_col + dir.y, _row + dir.x].GetComponent<MapObject>();
            return null;
        }
    }
}
