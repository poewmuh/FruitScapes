using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FruitScapes.Components.Basic
{
    public interface ICombine
    {
        int Id { get; set; }
        void GetDamage();
    }
}
