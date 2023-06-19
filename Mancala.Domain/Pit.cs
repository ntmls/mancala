using System;

namespace Mancala.Domain
{
    public class Pit : SeedContainer
    {
        public readonly int Number;

        internal Pit(int playerNumber, int pitNumber) : base(playerNumber) {
            Number = pitNumber;
            SeedCount = 4;
        }
    }
}