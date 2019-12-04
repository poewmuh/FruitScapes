using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FruitScapes.Components
{
    public class Animeted : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SmoothCreate()
        {
            animator.SetTrigger("Start");
        }

        public void HintScale()
        {
            animator.SetTrigger("HintScale");
        }

        public void HintUp()
        {
            animator.SetTrigger("HintUp");
        }

        public void HintRight()
        {
            animator.SetTrigger("HintRight");
        }

        public void HintDown()
        {
            animator.SetTrigger("HintDown");
        }

        public void HintLeft()
        {
            animator.SetTrigger("HintLeft");
        }
    }
}
