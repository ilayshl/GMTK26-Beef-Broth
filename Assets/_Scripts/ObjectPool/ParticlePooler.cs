using System.Collections.Generic;
using UnityEngine;

namespace CheesePull.Particles
{
    public class ParticlePooler : MonoBehaviour
    {
        private List<ParticleSystem> activeParticles = new();

        void Awake()
        {
            EventBus.Subscribe<ParticleSpawnedEvent>(OnParticleSpawned);
        }

        void OnDestroy()
        {
            EventBus.Unsubscribe<ParticleSpawnedEvent>(OnParticleSpawned);
        }

        void LateUpdate() //Late update to prevent any changes in the list while iterating
        {
            if (activeParticles.Count == 0) return;

            for (int i = activeParticles.Count - 1; i >= 0; i--)
            {
                //Guard against nulls (e.g. destroyed outside the pooler)
                if (activeParticles[i] == null)
                {
                    activeParticles.RemoveAt(i);
                    continue;
                }

                //IsAlive(true) checks sub-emitters too; withChildren=true is safer for complex FX
                if (!activeParticles[i].IsAlive(true))
                {
                    ObjectPoolManager.ReturnObjectToPool(activeParticles[i].gameObject, PoolType.Particle);
                    activeParticles.RemoveAt(i);
                }
            }
        }

        private void OnParticleSpawned(ParticleSpawnedEvent ev)
        {
            if(activeParticles.Contains(ev.Particle))
            {
                return;
            }
            activeParticles.Add(ev.Particle);
        }
    }
}
