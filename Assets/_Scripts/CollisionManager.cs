using System;
using MoreMountains.Feedbacks;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private MMFeedbacks collisionFeedback;
    [SerializeField] private MMFeedbacks hitFeedback;
    [SerializeField] private MMFeedbacks uiFeedback;
    private float lastCollisionTime;

    void Awake()
    {
        EventBus.Subscribe<CollisionEvent>(OnCollisionEvent);
    }

    void OnDestroy()
    {
        EventBus.Unsubscribe<CollisionEvent>(OnCollisionEvent);
    }

    private void OnCollisionEvent(CollisionEvent ev)
    {
        if (lastCollisionTime == Time.time) return;
        lastCollisionTime = Time.time;

        ContactPoint contact = ev.Collision.GetContact(0);
        Vector3 hitPoint = contact.point;

        Rigidbody aRb = ev.Collision.thisRigidbody;
        Rigidbody bRb = ev.Collision.rigidbody;

        float aDot = Vector3.Dot(
            ev.Sender.Head.forward,
            (hitPoint - ev.Sender.Head.position).normalized);

        float bDot = Vector3.Dot(
            ev.Collided.Head.forward,
            (hitPoint - ev.Collided.Head.position).normalized);

        aDot = Mathf.Max(0f, aDot);
        bDot = Mathf.Max(0f, bDot);

        float aForwardSpeed = Mathf.Max(0f, Vector3.Dot(aRb.linearVelocity, ev.Sender.Head.forward));
        float bForwardSpeed = Mathf.Max(0f, Vector3.Dot(bRb.linearVelocity, ev.Collided.Head.forward));

        float aScore = Mathf.Pow(aDot, 3f) * aForwardSpeed;
        float bScore = Mathf.Pow(bDot, 3f) * bForwardSpeed;

        CollisionDetector dealer;
        CollisionDetector receiver;
        
        if (aScore >= bScore)
        {
            dealer = ev.Sender;
            receiver = ev.Collided;
        }
        else
        {
            dealer = ev.Collided;
            receiver = ev.Sender;
        }
        
        float damage = Mathf.Max(aScore, bScore);
        
        /* if (damage <= 0f) //No attacker identified
            return; */

        receiver.ReceiveHit(damage);

        uiFeedback.PlayFeedbacks();

        Debug.Log(
            $"A Score: {aScore:F2} | B Score: {bScore:F2} | " +
            $"Dealer: {dealer.name} | Damage: {damage:F2}");
    }
}
