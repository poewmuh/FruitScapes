using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FruitScapes.Components.Basic;

namespace FruitScapes.Components
{
    public class Combine : MonoBehaviour, ICombine
    {
        public int Id { get; set; }
        public bool iDie => _hp <= 0;

        private Movable _myMovable;
        private int _hp;
        

        public void GetDamage()
        {
            _hp--;
        }

        private void Awake()
        {
            _myMovable = GetComponent<Movable>();
            _hp = 1;
        }

        public Combine GetNeighborCombine(Vector2Int dir, GameObject[,] allObjects)
        {
            if (_myMovable.CanFit(dir, allObjects))
                return allObjects[_myMovable.GetCol + dir.y, _myMovable.GetRow + dir.x].GetComponent<Combine>();
            return null;
        }
    }
}
