using UnityEngine;

public struct ParticleSpawnedEvent : IGameEvent
{
    public ParticleSystem Particle;
    public ParticleSpawnedEvent(ParticleSystem particle)
    {
        Particle = particle;
    }
}
