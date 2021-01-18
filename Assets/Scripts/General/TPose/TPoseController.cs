using System;
using UnityEngine;

namespace General.TPose
{
    public class TPoseController : SkeletonManager
    {
        public GameObject progressBarObject;

        public int durationUntilNextSceneInSeconds;

        public delegate void TPoseControllerEventHandler(object source, EventArgs args);

        public event TPoseControllerEventHandler TPoseDetected;
        public event TPoseControllerEventHandler TPoseStopped;

        public new void Start()
        {
            base.Start();
            SetupProgressBar();
        }

        public void SetDurationUntilNextSceneInSeconds(int seconds)
        {
            durationUntilNextSceneInSeconds = seconds;
            SetupProgressBar();
        }
        
        protected override GameObject AddAdditionalSpecimenForSkeleton(GameObject skeleton)
        {
            var script = skeleton.GetComponent<Skeleton.Skeleton>();
            script.CollisionStarted += OnFullCollisionStart;
            script.CollisionStopped += OnFullCollisionStop;
            return skeleton;
        }
        
        private void SetupProgressBar()
        {
            var animationTimeMultiplier = 1.0f / durationUntilNextSceneInSeconds;
            var animator = progressBarObject.GetComponent<Animator>();
            animator.SetFloat("timeMultiplier", animationTimeMultiplier);
        }

        private void OnFullCollisionStart(object source, EventArgs args)
        {
            Debug.Log("Collision started. Will start calibration");
            StartAnimatingProgressBar();
            OnTPoseDetectedStart();
        }

        private void OnFullCollisionStop(object source, EventArgs args)
        {
            Debug.Log("Collision stopped. Resetting scene...");
            StopAnimatingProgressBar();
            OnTPoseDetectionStop();
        }
        
        private void StartAnimatingProgressBar()
        {
            var animator = progressBarObject.GetComponent<Animator>();

            if (animator == null) return; // Exists
            if (animator.GetBool("active")) return; // Is not already shown
            animator.SetBool("active", true);
        }

        private void StopAnimatingProgressBar()
        {
            var animator = progressBarObject.GetComponent<Animator>();
            
            if (!animator.GetBool("active")) return;
            animator.SetBool("active", false);
        }

        protected virtual void OnTPoseDetectedStart()
        {
            TPoseDetected?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnTPoseDetectionStop()
        { 
            TPoseStopped?.Invoke(this, EventArgs.Empty);  
        }
    }
}