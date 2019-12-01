using FruitScapes.Extensions;
using FruitScapes.Events.Components;
using UnityEngine.Events;

namespace FruitScapes.Events
{
    public static class EventHolder
    {
        public static MoveEvent moveEvent = new MoveEvent();
        public static UnityEvent destroyEvent = new UnityEvent();
        public static UnityEvent startAnimation = new UnityEvent();
    }
}

namespace FruitScapes.Events.Components
{
    public class MoveEvent : UnityEvent<FruitWithDir> { };
}
