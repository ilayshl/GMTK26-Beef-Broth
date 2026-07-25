using UnityEngine;

public struct GameStartedEvent : IGameEvent
{
    public Rigidbody Player;

    public GameStartedEvent(Rigidbody player)
    {
        Player = player;
    }
}
