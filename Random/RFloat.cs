namespace SFBehaviourTree.Random
{
    public static class RFloat
    {
        public static float GetRandom()
        {
            return (float)RInt.GetRandom(int.MaxValue) / int.MaxValue;
        }
    }
}
