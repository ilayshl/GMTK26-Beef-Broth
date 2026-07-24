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
        if(lastCollisionTime == Time.time) return;
        lastCollisionTime = Time.time;
        uiFeedback.PlayFeedbacks();
        Debug.Log($"[{name}] Collision between characters!");
    }
}
