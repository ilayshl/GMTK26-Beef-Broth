using UnityEngine;

public struct CollisionEvent : IGameEvent
{
    public Collision Collision;
    public CollisionDetector Sender;
    public CollisionDetector Collided;

    public CollisionEvent(Collision collision, CollisionDetector sender, CollisionDetector collided)
    {
        Collision = collision;
        Sender = sender;
        Collided = collided;
    }
}
