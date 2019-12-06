using FruitScapes.Extensions;
using FruitScapes.Events.Components;
using UnityEngine.Events;

namespace FruitScapes.Events
{
    public static class EventHolder
    {
        public static MoveEvent moveEvent = new MoveEvent();
        public static UnityEvent destroyEvent = new UnityEvent();
        public static SpeedEvent startAnimation = new SpeedEvent();
    }
}

namespace FruitScapes.Events.Components
{
    public class MoveEvent : UnityEvent<FruitWithDir> { };
    public class SpeedEvent : UnityEvent<float> { };
}


