using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FruitScapes.Components.Basic;
using FruitScapes.Events;
using FruitScapes.Extensions;

namespace FruitScapes.Components
{
    public class Draggable : MonoBehaviour, IDraggable
    {
        private Vector2 _startTouch;
        private Vector2 _endTouch;
        private float _resist = 0.4f;
        private float _swipeAngle;

        private void OnMouseDown()
        {
            _startTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        private void OnMouseUp()
        {
            _endTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TryToMove();
        }

        private void TryToMove()
        {
            if (Mathf.Abs(_startTouch.x - _endTouch.x) > _resist || Mathf.Abs(_startTouch.y - _endTouch.y) > _resist)
            {
                _swipeAngle = Mathf.Atan2(_startTouch.y - _endTouch.y, _startTouch.x - _endTouch.x) * 180 / Mathf.PI;
                EventHolder.moveEvent.Invoke(new FruitWithDir(gameObject, GetDirection(_swipeAngle)));
            }
            else
            {
                Debug.Log("Try Harder!");
            }
        }

        private Vector2Int GetDirection(float swipeAngle)
        {
            if (swipeAngle > -45 && swipeAngle <= 45)
            {
                return Vector2Int.left;
            }
            else if (swipeAngle > 45 && swipeAngle <= 135)
            {
                return Vector2Int.down;
            }
            else if ((swipeAngle > 135 || swipeAngle <= -135))
            {
                return Vector2Int.right;
            }
            else if (swipeAngle < -45 && swipeAngle >= -135)
            {
                return Vector2Int.up;
            }
            else return Vector2Int.zero;
        }
    }
}
