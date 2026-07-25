using CheesePull;
using MoreMountains.Feedbacks;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private float velocityInfluence = 0.2f;
    [SerializeField] private MMFeedbacks collisionFeedback;
    [SerializeField] private MMFeedbacks hitFeedback;
    [SerializeField] private MMFeedbacks uiFeedback;
    [SerializeField] private ParticleSystem particlePrefab;

    private float lastCollisionTime;

    private void Awake()
    {
        EventBus.Subscribe<CollisionEvent>(OnCollisionEvent);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<CollisionEvent>(OnCollisionEvent);
    }

    private void OnCollisionEvent(CollisionEvent ev)
    {
        if (lastCollisionTime == Time.time)
            return;

        lastCollisionTime = Time.time;

        collisionFeedback?.PlayFeedbacks();

        Rigidbody aRb = ev.Collision.thisRigidbody;
        Rigidbody bRb = ev.Collision.rigidbody;

        if (aRb == null || bRb == null)
            return;

        ContactPoint contact = ev.Collision.GetContact(0);

        Vector3 collisionNormal = contact.normal;

        Vector3 aCollisionDirection = -collisionNormal;
        Vector3 bCollisionDirection = collisionNormal;

        float aHeadAlignment = Mathf.Clamp01(
            Vector3.Dot(
                ev.Sender.Head.forward,
                aCollisionDirection));

        float bHeadAlignment = Mathf.Clamp01(
            Vector3.Dot(
                ev.Collided.Head.forward,
                bCollisionDirection));

        float aSpeed = aRb.linearVelocity.magnitude;
        float bSpeed = bRb.linearVelocity.magnitude;


        float maxSpeed = Mathf.Max(aSpeed, bSpeed, 1f);

        float aVelocityBonus = (aSpeed / maxSpeed) * velocityInfluence;
        float bVelocityBonus = (bSpeed / maxSpeed) * velocityInfluence;

        float aScore =
            aHeadAlignment * (1f - velocityInfluence)
            + aVelocityBonus;

        float bScore =
            bHeadAlignment * (1f - velocityInfluence)
            + bVelocityBonus;

        CollisionDetector dealer;
        CollisionDetector receiver;
        float damage;

        if (aScore >= bScore)
        {
            dealer = ev.Sender;
            receiver = ev.Collided;
            damage = aScore;
        }
        else
        {
            dealer = ev.Collided;
            receiver = ev.Sender;
            damage = bScore;
        }

        if (damage <= 0.01f)
        {
            Debug.Log(
                $"Collision with no attacker | A:{aScore:F2} B:{bScore:F2}");
            return;
        }

        receiver.ReceiveHit();

        hitFeedback?.PlayFeedbacks();
        uiFeedback?.PlayFeedbacks();
        ObjectPoolManager.SpawnObject(particlePrefab, contact.point, Quaternion.identity);

        Debug.Log(
            $"Dealer: {dealer.name} | " +
            $"Damage: {damage:F2} | " +
            $"A Score: {aScore:F2} | " +
            $"B Score: {bScore:F2} | " +
            $"A Alignment: {aHeadAlignment:F2} | " +
            $"B Alignment: {bHeadAlignment:F2}");
    }
}