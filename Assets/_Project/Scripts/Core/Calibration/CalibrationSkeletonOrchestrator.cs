using _Project.Scripts.Core.InTraining;
using _Project.Scripts.DomainObjects;
using UnityEngine;

namespace _Project.Scripts.Core.Calibration
{
    public class CalibrationSkeletonOrchestrator : ISkeletonOrchestrator
    {
        private readonly int maxNumberOfPeople;
        private CalibrationSkeleton[] skeletons;

        public CalibrationSkeletonOrchestrator(int maxNumberOfPeople)
        {
            this.maxNumberOfPeople = maxNumberOfPeople;
            InitializeAllSkeletons();
        }

        public void Update(Person[] detectedPersons)
        {
            if (detectedPersons == null)
                return;

            for (var p = 0; p < maxNumberOfPeople; p++)
            {
                // Init skeleton if not given.
                if (skeletons[p] == null)
                {
                    skeletons[p] = new CalibrationSkeleton(p);
                    Debug.LogError("Initialized a new skeleton which should be already there 🤔. p: " + p);
                }

                // Set and activate only skeletons that are detected.
                if (p >= 0 && detectedPersons.Length > p && p == detectedPersons[p].id)
                    UpdateSkeleton(skeletons[p], detectedPersons[p]);
                else
                    skeletons[p].SetIsVisible(false);
            }
        }

        public void InitializeAllSkeletons()
        {
            skeletons = new CalibrationSkeleton[maxNumberOfPeople];
            for (var p = 0; p < maxNumberOfPeople; p++)
            {
                skeletons[p] = new CalibrationSkeleton(p);
                skeletons[p].SetIsVisible(false);
            }
        }

        private void UpdateSkeleton(CalibrationSkeleton skeleton, Person person)
        {
            skeleton.SetSkeleton(person.joints, person.lowestY);
            skeleton.SetIsVisible(true);
        }
    }
}