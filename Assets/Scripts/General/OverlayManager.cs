using System;
using UnityEngine;

namespace General
{
    public class OverlayManager : MonoBehaviour
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void FadeOut()
        {
            animator.SetBool("blendOut", true);
        }
    }
}