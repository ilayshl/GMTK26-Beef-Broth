using UnityEngine;

public struct CarDestroyedEvent : IGameEvent
{
    public CarBrain Brain;

    public CarDestroyedEvent(CarBrain brain)
    {
        Brain = brain; 
    }
}
