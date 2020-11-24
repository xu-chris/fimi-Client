using System;
using System.Collections;
using _Project.Scripts.Source.DomainObjects.Configurations;
using _Project.Scripts.Source.Periphery.Configurations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Source.PreExercise.Calibration
{
    public class CalibrationSkeletonSceneController : SkeletonSceneController
    {
        public TextAsset calibrationConfigurationFile;
        public GameObject progressBarObject;
        public string nextSceneName;
        public GameObject overlay;

        private CalibrationConfiguration calibrationConfiguration;

        private bool calibrated = false;
        private bool calibrationAborted = false;

        public new void Start()
        {
            base.Start();
            calibrationConfiguration = new CalibrationConfigurationService(calibrationConfigurationFile).configuration;
            maxNumberOfPeople = 1;
            SetupProgressBar();
        }

        private void SetupProgressBar()
        {
            var animationTimeMultiplier = 1.0f / calibrationConfiguration.durationOfCalibrationInSeconds;
            progressBarObject.GetComponent<Animator>().SetFloat("calibrationTimeMultiplier", animationTimeMultiplier);
        }

        public void OnFullCollisionStart(object source, EventArgs args)
        {
            Debug.Log("Collision started. Will start calibration");
            StartCoroutine(CheckCalibrationSuccess());
            StartAnimatingProgressBar();
            StartCalibration();
        }

        public void OnFullCollisionStop(object source, EventArgs args)
        {
            Debug.Log("Collision stopped. Resetting scene...");
            StopAnimatingProgressBar();
            ResetCalibration();
        }

        private void StartAnimatingProgressBar()
        {
            var animator = progressBarObject.GetComponent<Animator>();

            if (animator == null) return; // Exists
            if (animator.GetBool("calibrating")) return; // Is not already shown
            animator.SetBool("calibrating", true);
        }

        private void StopAnimatingProgressBar()
        {
            var animator = progressBarObject.GetComponent<Animator>();
            
            if (!animator.GetBool("calibrating")) return;
            animator.SetBool("calibrating", false);
        }
        
        private IEnumerator CheckCalibrationSuccess()
        {
            yield return new WaitForSeconds(calibrationConfiguration.durationOfCalibrationInSeconds);
            if (calibrationAborted) yield break;
            
            calibrated = true;
            OnCalibrationComplete();
        }

        private void OnCalibrationComplete()
        {
            overlay.GetComponent<Animator>().SetBool("blendOut", true);
            StartCoroutine(TransitionToNewScene());
        }

        private IEnumerator TransitionToNewScene()
        {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(nextSceneName);
        }

        private void StartCalibration()
        {
            calibrationAborted = false;
            webSocketClient.SendRescaleSkeletons();
        }

        private void ResetCalibration()
        {
            if (!calibrated)
                calibrationAborted = true;
        }

        protected override GameObject AddAdditionalSpecimenForSkeleton(GameObject skeleton)
        {
            var script = skeleton.GetComponent<CalibrationSkeleton>();
            script.CollisionStarted += OnFullCollisionStart;
            script.CollisionStopped += OnFullCollisionStop;
            return skeleton;
        }
    }
}