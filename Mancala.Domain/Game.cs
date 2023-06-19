using System;

namespace Mancala.Domain
{
    public partial class Game
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public void SetStartingPlayer(Player player)
        {
            _startingPlayer = player; 
            if (!_gameStarted)
            {
                _currentPlayer = _startingPlayer; 
            } else
            {
                throw new GameAlreadyStartedException();
            }
        }

        public static Game Setup()
        {
            var game = new Game();
            var player1 = Player.Initialize(1);
            var player2 = Player.Initialize(2);
            game.InitializePlayers(player1, player2);
            return game;
        }

        public void TakeTurn(Player player, int pitNumber)
        {
            AssertIsPlayersTurn(player);
            StartGameIfNotAlreadyStarted();
            player.TakeTurn(this, pitNumber);
        }

        private Game() { }
        private bool _gameStarted;
        private Player _startingPlayer;
        private Player _currentPlayer;

        private void InitializePlayers(Player player1, Player player2)
        {
            this.Player1 = player1;
            this.Player2 = player2;
            player1.InitializeSequence(player2);
            player2.InitializeSequence(player1);
        }

        private void AssertIsPlayersTurn(Player player)
        {
            if (!IsPlayersTurn(player)) throw new NotPlayersTurnException();
        }

        private bool IsPlayersTurn(Player player)
        {
            return object.ReferenceEquals(_currentPlayer, player);
        }

        internal void SwitchPlayers(Player player)
        {
            _currentPlayer = GetOtherPlayer(player);
        }

        internal Player GetOtherPlayer(Player player)
        {
            if (object.ReferenceEquals(player, Player1)) return Player2;
            return Player1;
        }

        private void StartGameIfNotAlreadyStarted()
        {
            if (!_gameStarted) _gameStarted = true;
        }

    }

}