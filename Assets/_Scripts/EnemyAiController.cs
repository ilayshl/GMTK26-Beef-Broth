using UnityEngine;

public class EnemyAiController : CarController
{
    [SerializeField] private Rigidbody target;
    [SerializeField] private float maxPredictionTime = 1.5f;
    private Rigidbody _target;

    protected override void Awake()
    {
        base.Awake();
        if (target == null)
        {
            EventBus.Subscribe<GameStartedEvent>(OnGameStarted);
        }
        else
        {
            _target = target;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (target == null)
            EventBus.Unsubscribe<GameStartedEvent>(OnGameStarted);
    }

    private void OnGameStarted(GameStartedEvent ev)
    {
        _target = ev.Player;
    }

    public override void CalculateInputs()
    {
        float distance = Vector3.Distance(transform.position, _target.position);

        float targetSpeed = _target.linearVelocity.magnitude;

        float predictionTime = targetSpeed > 0.1f
            ? Mathf.Min(distance / targetSpeed, maxPredictionTime)
            : 0f;

        targetPosition = _target.position +
                         _target.linearVelocity * predictionTime;

        if(showTargetAnchor)
        {
            
        }
    }
}
