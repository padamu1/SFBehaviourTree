namespace SFBehaviourTree.Random
{
    public class RInt
    {
        private static readonly Lazy<RInt> instance = new Lazy<RInt>(() => new RInt());

        private static RInt Instance => instance.Value;

        private long seed = 123456789;

        private RInt()
        {

        }

        private long GetSeed()
        {
            long newSeed = Interlocked.Increment(ref seed);
            if (newSeed >= long.MaxValue)
            {
                Interlocked.Exchange(ref seed, 10000);
                return 10000;
            }
            return newSeed;
        }

        private int GetRandomValue(int max)
        {
            long mixed = DateTime.UtcNow.Ticks ^ GetSeed(); 
            mixed ^= (mixed << 21);
            mixed ^= (mixed >> 25);
            mixed ^= (mixed << 4);
            return (int)((mixed & 0x7FFFFFFFFFFFFFFF) % max);
        }

        public static int GetRandom(int max)
        {
            if (max <= 0)
                throw new ArgumentOutOfRangeException(nameof(max), "max must be greater than 0");

            return Instance.GetRandomValue(max);
        }
    }
}
