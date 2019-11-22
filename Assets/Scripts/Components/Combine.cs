using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FruitScapes.Components.Basic;

namespace FruitScapes.Components
{
    public class Combine : MonoBehaviour, ICombine
    {
        public int Id { get; set; }

        public void GetDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}
