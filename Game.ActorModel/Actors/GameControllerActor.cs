using System.Collections.Generic;
using Akka.Actor;
using Game.ActorModel.Messages;

namespace Game.ActorModel.Actors
{
    public class GameControllerActor : ReceiveActor
    {
        private readonly IDictionary<string, IActorRef> _players;

        public GameControllerActor()
        {
            _players = new Dictionary<string, IActorRef>();

            Receive<JoinGameMessage>(m => JoinGame(m));
            Receive<AttackPlayerMessage>(m => _players[m.PlayerName].Forward(m));
        }

        private void JoinGame(JoinGameMessage message)
        {
            if (_players.ContainsKey(message.PlayerName))
                return;

            _players[message.PlayerName] = Context.ActorOf(Props.Create(() => new PlayerActor(message.PlayerName)), message.PlayerName);

            foreach (var player in _players.Values)
                player.Tell(new RefreshPlayerStatusMessage(), Sender);
        }
    }
}