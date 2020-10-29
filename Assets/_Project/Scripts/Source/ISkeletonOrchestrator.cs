using _Project.Scripts.DomainObjects;

namespace _Project.Scripts.Source
{
    public interface ISkeletonOrchestrator
    {
        void Update(Person[] detectedPersons);
        void InitializeAllSkeletons();
    }
}