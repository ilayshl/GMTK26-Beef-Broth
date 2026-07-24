using UnityEngine;

public struct CollisionEvent : IGameEvent
{
    public float Magnitude;
    public Rigidbody Hitter;
    public Rigidbody Taker;
    public Vector3 PositionOfImpact;

    public CollisionEvent(float mag, Rigidbody hitter, Rigidbody taker, Vector3 pos)
    {
        Magnitude = mag;
        Hitter = hitter;
        Taker = taker;
        PositionOfImpact = pos;
    }
}
