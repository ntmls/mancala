using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mancala.Domain;
using System;
using System.Linq;

namespace Mancala.Tests
{
    internal class MoveTester
    {
        private Game game;
        private readonly GameSerializer serializer = new();

        public MoveTester(Game game)
        {
            this.game = game;
        }

        internal MoveTester Move(int playerNumber, int pitNumber)
        {
            Player player = DetermingPlayer(playerNumber);
            game.TakeTurn(player, pitNumber);
            AssertInvariants();
            return this;
        }


        /*
        internal void AssertMove(int playerNumber, int pitNumber, string expected)
        {
            Move(playerNumber, pitNumber);
            AssertCurrentState(expected);
        }
        */

        internal void AssertGame(string expected)
        {
            var actual = serializer.Serialize(game);
            var fixedExpected = FixExpected(expected);
            var fixedActual = FixExpected(actual);
            Assert.AreEqual(fixedExpected, fixedActual);
        }

        private string FixExpected(string expected)
        {
            var lines = expected.Split('\n');
            var results = lines.Select(x => x.TrimStart()).ToArray();
            return string.Join('\n', results);
        }

        private Player DetermingPlayer(int playerNumber)
        {
            if (playerNumber == 1) return game.Player1;
            if (playerNumber == 2) return game.Player2;
            throw new ArgumentException(nameof(playerNumber));
        }

        private void AssertInvariants()
        {
            AssertGameHasTwentyFourStones();
        }

        private void AssertGameHasTwentyFourStones()
        {
            var players = new Player[] { game.Player1, game.Player2 };
            var count = 0;
            foreach (var player in players)
            {
                foreach (var pit in player.Pits)
                {
                    count += pit.SeedCount;
                }
                count += player.Store.SeedCount;
            }
            Assert.AreEqual(48, count);
        }

    }
}