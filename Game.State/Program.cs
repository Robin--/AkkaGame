using System;
using Akka.Actor;
using Game.ActorModel.Actors;

namespace Game.State
{
    public static class Program
    {
        private static ActorSystem _actorSystemInstance;

        public static void Main(string[] args)
        {
            _actorSystemInstance = ActorSystem.Create("GameSystem");

            var gameController = _actorSystemInstance.ActorOf<GameControllerActor>("GameController");

            Console.ReadLine();
        }
    }
}