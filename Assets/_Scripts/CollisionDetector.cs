using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public event Action HitReceived;
    public Transform Head => collisionHead;
    [SerializeField] private Transform collisionHead;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody == null) return;

        if (collision.gameObject.TryGetComponent<CollisionDetector>(out var hitDetector))
        {
            EventBus.Publish(new CollisionEvent(collision, this, hitDetector));
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            // Direction pointing away from the wall
            Vector3 pushDirection = collision.contacts[0].normal;

            float pushForce = 2f;
            rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }

    public void ReceiveHit()
    {
        HitReceived?.Invoke();
    }
}
