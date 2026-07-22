public struct GameStartedEvent : IGameEvent
{
    public CharacterBrain Player;

    public GameStartedEvent(CharacterBrain player)
    {
        Player = player;
    }
}
