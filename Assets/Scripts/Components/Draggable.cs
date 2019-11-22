using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FruitScapes.Components.Basic;

namespace FruitScapes.Components
{
    public class Draggable : MonoBehaviour, IDraggable
    {
        private Vector2 _startTouch;
        private Vector2 _endTouch;

        private void OnMouseDown()
        {
            _startTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        private void OnMouseUp()
        {
            _endTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
