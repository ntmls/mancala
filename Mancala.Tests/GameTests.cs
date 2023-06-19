using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mancala.Domain;

namespace Mancala.Tests
{

    // rules: https://www.scholastic.com/content/dam/teachers/blogs/alycia-zimmerman/migrated-files/mancala_rules.pdf

    [TestClass]
    public class GameTests
    {
        private Game game;
        private MoveTester moves;

        [TestInitialize]
        public void Setup()
        {
            game = Game.Setup();
            game.SetStartingPlayer(game.Player1);
            moves = new MoveTester(game);
        }

        [TestMethod]
        public void TestInitialState()
        {
            moves.AssertGame(
                $@"
                PLAYER 2 | STORE(00) 6(04) 5(04) 4(04) 3(04) 2(04) 1(04)
                PLAYER 1 |           1(04) 2(04) 3(04) 4(04) 5(04) 6(04) STORE(00)
                ");
        }

        [TestMethod]
        public void PlayerTakesTurnEntirelyOnThierSide()
        {
            moves.Move(1, 1).AssertGame(
                $@"
                PLAYER 2 | STORE(00) 6(04) 5(04) 4(04) 3(04) 2(04) 1(04)
                PLAYER 1 |           1(00) 2(05) 3(05) 4(05) 5(05) 6(04) STORE(00)
                ");
        }

        [TestMethod]
        public void Player1TakesTurnThatGoesToOtherSide()
        {
            moves.Move(1, 5).AssertGame(
                $@"
                PLAYER 2 | STORE(00) 6(04) 5(04) 4(04) 3(04) 2(05) 1(05)
                PLAYER 1 |           1(04) 2(04) 3(04) 4(04) 5(00) 6(05) STORE(01)
                ");
        }

        [TestMethod]
        public void Player2TakesTurnThatGoesToOtherSide()
        {
            game.SetStartingPlayer(game.Player2);
            moves.Move(2, 4).AssertGame(
                $@"
                PLAYER 2 | STORE(01) 6(05) 5(05) 4(00) 3(04) 2(04) 1(04)
                PLAYER 1 |           1(05) 2(04) 3(04) 4(04) 5(04) 6(04) STORE(00)
                ");
        }

        [TestMethod]
        public void LastStonePlacedOnEmptyPitOnPlayersSideTakesOtherPlayersStones()
        {
            moves.Move(1, 5).AssertGame(
                $@"
                PLAYER 2 | STORE(00) 6(04) 5(04) 4(04) 3(04) 2(05) 1(05)
                PLAYER 1 |           1(04) 2(04) 3(04) 4(04) 5(00) 6(05) STORE(01)
                ");

            moves.Move(2, 1).AssertGame(
                $@"
                PLAYER 2 | STORE(00) 6(05) 5(05) 4(05) 3(05) 2(06) 1(00)
                PLAYER 1 |           1(04) 2(04) 3(04) 4(04) 5(00) 6(05) STORE(01)
                ");

            moves.Move(1, 1).AssertGame( 
                $@"
                PLAYER 2 | STORE(00) 6(05) 5(05) 4(05) 3(05) 2(00) 1(00)
                PLAYER 1 |           1(00) 2(05) 3(05) 4(05) 5(00) 6(05) STORE(08)
                ");
        }

        [TestMethod]
        [ExpectedException(typeof(GameAlreadyStartedException))]
        public void CannotChangeStartingPlayerAfterGameIsStarted()
        {
            moves.Move(1, 5);
            game.SetStartingPlayer(game.Player2);
        }

        [TestMethod]
        [ExpectedException(typeof(NotPlayersTurnException))]
        public void PlayerCannotMoveoutOfTurn()
        {
            moves.Move(1, 5);
            moves.Move(1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidMoveException))]
        public void CannotMoveOnEmptyPit()
        {
            moves.Move(1, 5);
            moves.Move(2, 1);
            moves.Move(1, 5);
        }

        [TestMethod]
        public void PlayerGetsToGoAgainIfTheLastSeedIsPlayedInTheirStore()
        {
            moves.Move(1, 3);
            moves.Move(1, 5);
        }

    }
}
