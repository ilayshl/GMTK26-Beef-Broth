using UnityEngine;

public class EnemyAiController : CarController
{
    [SerializeField] private Rigidbody target; //For when it's a scene object
    [SerializeField] private float maxPredictionTime = 1.5f;
    private Rigidbody _target;

    void Start()
    {
        if(target == null) _target = CharacterSpawner.Instance.SpawnedPlayer;
        else _target = target;
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
    }
}
