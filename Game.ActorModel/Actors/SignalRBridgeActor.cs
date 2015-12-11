using Akka.Actor;
using Game.ActorModel.ExternalSystems;
using Game.ActorModel.Messages;

namespace Game.ActorModel.Actors
{
    public class SignalRBridgeActor : ReceiveActor
    {
        private readonly IGameEventsPusher _gameEventsPusher;
        private readonly IActorRef _gameController;

        public SignalRBridgeActor(IGameEventsPusher gameEventsPusher, IActorRef gameController)
        {
            _gameEventsPusher = gameEventsPusher;
            _gameController = gameController;

            Receive<JoinGameMessage>(m =>
            {
                _gameController.Tell(m);
            });

            Receive<AttackPlayerMessage>(m =>
            {
                _gameController.Tell(m);
            });

            Receive<PlayerStatusMessage>(m =>
            {
                _gameEventsPusher.PlayerJoined(m.PlayerName, m.Health);
            });

            Receive<PlayerHealthChangedMessage>(m =>
            {
                _gameEventsPusher.UpdatePlayerHealth(m.PlayerName, m.Health);
            });
        }
    }
}