using System;
using System.Collections.Generic;

namespace Mancala.Domain
{
    public class Player
    {
        public readonly int Number;
        public IEnumerable<Pit> Pits => _pits;
        public readonly Store Store;

        internal static Player Initialize(int playerNumber)
        {
            var pits = InitializePits(playerNumber);
            var Store = new Store(playerNumber);
            var player = new Player(playerNumber, pits, Store);
            return player;
        }

        internal void InitializeSequence(Player other)
        {
            _sequence = SeedContainerSequence.Initialize(this, other);
        }

        internal void TakeTurn(Game game, int pitNumber)
        {
            AssertPitNotEmpty(pitNumber);

            _sequence.Start(pitNumber);
            var hand = _sequence.Container.SeedCount;
            _sequence.Container.Clear();
            SeedContainer lastContainerAddedTo = null;
            for (var i = 0; i < hand; i++)
            {
                _sequence.Next();
                _sequence.Container.AddSeed();
                lastContainerAddedTo = _sequence.Container;
            }

            //TODO: Type cassting... should move behaviour to pit and store.

            if (lastContainerAddedTo is Pit)
            {
                if(lastContainerAddedTo.SeedCount == 1)
                {
                    if (lastContainerAddedTo.PlayerNumber == this.Number)
                    {
                        var otherPit = GetCorrespondingPit(game, lastContainerAddedTo as Pit);
                        var seedsToMove = otherPit.SeedCount + 1; //other plus this one
                        otherPit.Clear();
                        lastContainerAddedTo.Clear();
                        Store.AddSeeds(seedsToMove);
                    }
                }
            }

            if (!(lastContainerAddedTo is Store))
            {
                game.SwitchPlayers(this);
            }
        }

        private Pit GetCorrespondingPit(Game game, Pit pit)
        {
            var otherNumber = 6 - pit.Number + 1;
            var otherPlayer = game.GetOtherPlayer(this);
            var otherPit = otherPlayer._pits[otherNumber - 1];
            return otherPit;
        }

        private SeedContainerSequence _sequence;
        private readonly List<Pit> _pits;

        private Player(
            int number,
            List<Pit> pits,
            Store store)
        {
            this.Number = number;
            _pits = pits ?? throw new System.ArgumentNullException(nameof(pits));
            Store = store ?? throw new System.ArgumentNullException(nameof(store));
        }

        private void AssertPitNotEmpty(int pitNumber)
        {
            if (_pits[pitNumber - 1].SeedCount == 0) throw new InvalidMoveException();
        }

        private static List<Pit> InitializePits(int playerNumber)
        {
            var pits = new List<Pit>();
            for (var i = 0; i < 6; i++)
            {
                pits.Add(new Pit(playerNumber, i + 1));
            }
            return pits;
        }
    }

}