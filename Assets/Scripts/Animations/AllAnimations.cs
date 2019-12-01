using System.Collections;
using UnityEngine;
using FruitScapes.Components;

namespace FruitScapes.Animations
{
    public static class AllAnimations
    {
        public static IEnumerator SimpleLerp(GameObject who, Vector2 where, float speed)
        {
            Vector2 startPos = who.transform.position;
            float timer = 0;
            while (timer <= speed)
            {
                who.transform.position = Vector2.Lerp(startPos, where, timer / speed);
                timer += Time.deltaTime;
                yield return null;
            }
            who.transform.position = where;
        }
    }
}
