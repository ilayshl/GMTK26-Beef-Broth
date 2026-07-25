using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public event Action HitReceived;
    public Transform Head => collisionHead;
    [SerializeField] private Transform collisionHead;
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.rigidbody == null) return;

        if (collision.gameObject.TryGetComponent<CollisionDetector>(out var hitDetector))
        {
            EventBus.Publish(new CollisionEvent(collision, this, hitDetector));
        }
    }

    public void ReceiveHit()
    {
        HitReceived?.Invoke();
    }
}
