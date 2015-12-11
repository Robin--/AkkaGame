namespace Game.ActorModel.Messages
{
    public class JoinGameMessage
    {
        public JoinGameMessage(string playerName)
        {
            PlayerName = playerName;
        }

        public string PlayerName { get; private set; }
    }
}