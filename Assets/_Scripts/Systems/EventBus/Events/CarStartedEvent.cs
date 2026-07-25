using UnityEngine;

public struct CarStartedEvent : IGameEvent
{
    public CarBrain Brain;

    public CarStartedEvent(CarBrain brain)
    {
        Brain = brain; 
    }
}
