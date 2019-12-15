using FruitScapes.Extensions;
using FruitScapes.Events.Components;
using UnityEngine.Events;
using System.Collections.Generic;
using FruitScapes.Components;

namespace FruitScapes.Events
{
    public static class EventHolder
    {
        public static MoveEvent moveEvent = new MoveEvent();
        public static UnityEvent destroyEvent = new UnityEvent();
        public static SpeedEvent startAnimation = new SpeedEvent();
        public static DestroyFrits destroyFruits = new DestroyFrits();
    }
}

namespace FruitScapes.Events.Components
{
    public class MoveEvent : UnityEvent<FruitWithDir> { };
    public class SpeedEvent : UnityEvent<float> { };
    public class DestroyFrits : UnityEvent<List<Combine>> { };
}


