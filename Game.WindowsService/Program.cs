using System;
using Akka.Actor;
using Game.ActorModel.Actors;
using Topshelf;

namespace Game.WindowsService
{
    public class GameStateService
    {
        private ActorSystem _actorSystemInstance;

        public void Start()
        {
            _actorSystemInstance = ActorSystem.Create("GameSystem");
            var gameController = _actorSystemInstance.ActorOf<GameControllerActor>("GameController");
        }

        public void Stop()
        {
            _actorSystemInstance.Shutdown();
            _actorSystemInstance.AwaitTermination(TimeSpan.FromSeconds(2));
        }
    }

    public static class Program
    {
        public static void Main()
        {
            HostFactory.Run(gameService =>
            {
                gameService.Service<GameStateService>(s =>
                {
                    s.ConstructUsing(game => new GameStateService());
                    s.WhenStarted(game => game.Start());
                    s.WhenStopped(game => game.Stop());
                });

                gameService.RunAsLocalSystem();
                gameService.StartAutomatically();

                gameService.SetDescription("PSDemo Game Topshelf Service");
                gameService.SetDisplayName("PSDemoGame");
                gameService.SetServiceName("PSDemoGame");
            });
        }
    }
}