using Mancala.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mancala.Tests
{
    [TestClass]
    public class GameSerializerTests
    {
        private GameSerializer serializer; 

        [TestInitialize]
        public void Setup()
        {
            serializer = new GameSerializer(); 
        }

        [TestMethod]
        public void ShouldSerializeNewGame()
        {
            var game = Game.Setup();
            var actual = serializer.Serialize(game);
            var expected = $@"
PLAYER 2 | STORE(00) 6(04) 5(04) 4(04) 3(04) 2(04) 1(04)
PLAYER 1 |           1(04) 2(04) 3(04) 4(04) 5(04) 6(04) STORE(00)
";
            Assert.AreEqual(expected, actual);
        }
    }
}
