using UnityEngine;

    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleEffect : MonoBehaviour
    {
        private ParticleSystem _particle;

        void Awake()
        {
            _particle = GetComponent<ParticleSystem>();
        }
        
        void OnEnable()
        {
            EventBus.Publish(new ParticleSpawnedEvent(_particle));
        }
    }
