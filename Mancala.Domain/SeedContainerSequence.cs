using System;
using static Mancala.Domain.Game;

namespace Mancala.Domain
{
    internal class SeedContainerSequence
    {

        private SeedContainerSequence(Player player, Player other)
        {
            _containers = new SeedContainer[13];
            var enumer1 = player.Pits.GetEnumerator(); 
            for (var i = 0; i < 6; i++) 
            {
                enumer1.MoveNext();
                _containers[i] = enumer1.Current;
            }

            _containers[6] = player.Store;

            var enumer2 = other.Pits.GetEnumerator();
            for (var i = 7; i < 13; i++)
            {
                enumer2.MoveNext();
                _containers[i] = enumer2.Current;
            }

        }

        private int _currentIndex = -1;
        private SeedContainer[] _containers;

        public SeedContainer Container
        {
            get
            {
                return _containers[_currentIndex];
            }
        }

        internal void Start(int pitNumber)
        {
            _currentIndex = pitNumber - 1;
        }

        internal void Next()
        {
            _currentIndex += 1;
            if (_currentIndex > 12) _currentIndex = 0;
        }

        internal static SeedContainerSequence Initialize(Player player, Player other)
        {
            return new SeedContainerSequence(player, other); 
        }
    }
}