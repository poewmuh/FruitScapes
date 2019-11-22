using System.Collections;
using System.Collections.Generic;
using FruitScapes.Extensions;
using UnityEngine;

namespace FruitScapes.Data
{
    [CreateAssetMenu(menuName = "FruitScapes/ObjectsData")]
    public class ObjectsData : ScriptableObject
    {
        [SerializeField] private List<ObjectAppearance> _objAppearance;

        public ObjectAppearance GetRandomAppearance()
        {
            return _objAppearance[Random.Range(0, _objAppearance.Count)];
        }
    }
}
