namespace Mancala.Domain
{
    public class SeedContainer
    {
        internal SeedContainer(int playerNumber)
        {
            PlayerNumber = playerNumber;
        }

        public int SeedCount { get; private protected set; }
        public int PlayerNumber { get; internal set; }

        internal void AddSeed()
        {
            SeedCount += 1;
        }

        internal void AddSeeds(int count)
        {
            SeedCount += count;
        }

        internal void Clear()
        {
            SeedCount = 0;
        }
    }
}