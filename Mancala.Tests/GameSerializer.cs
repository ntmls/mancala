using Mancala.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mancala.Tests
{
    internal class GameSerializer
    {
        internal string Serialize(Game game)
        {
            var pits1 = game.Player1.Pits;
            string values1 = BuildPitString(pits1);
            var store1 = game.Player1.Store.SeedCount.ToString("00");

            var pits2 = game.Player2.Pits.Reverse();
            string values2 = BuildPitString(pits2);
            var store2 = game.Player2.Store.SeedCount.ToString("00");

            return $@"
PLAYER 2 | STORE({store2}) {values2}
PLAYER 1 |           {values1} STORE({store1})
";
        }

        private static string BuildPitString(IEnumerable<Pit> pits)
        {
            var values = new List<string>();
            foreach (var pit in pits)
            {
                string count = pit.SeedCount.ToString("00");
                string value = $"{pit.Number}({count})";
                values.Add(value);
            }
            var parta = String.Join(" ", values);
            return parta;
        }
    }
}