namespace _Project.Scripts.Source.DomainValues
{
    public readonly struct BoneIndexes
    {
        public readonly int indexA;
        public readonly int indexB;

        public BoneIndexes(int indexA, int indexB)
        {
            this.indexA = indexA;
            this.indexB = indexB;
        }
    }
}