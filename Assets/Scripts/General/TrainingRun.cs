using System;
using General.Trainings;
using UnityEngine;

namespace General
{
    public class TrainingRun : MonoBehaviour
    {

        [SerializeField] private Training selectedTraining;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void SetSelectedTraining(Training training)
        {
            selectedTraining = training;
        }
    }
}