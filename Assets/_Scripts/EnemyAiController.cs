using UnityEngine;

public class EnemyAiController : CharacterController
{
    private Transform _target;

    protected override void Awake()
    {
        base.Awake();
        EventBus.Subscribe<GameStartedEvent>(OnGameStarted);
    }

    void OnDestroy()
    {
        EventBus.Unsubscribe<GameStartedEvent>(OnGameStarted);
    }

    private void OnGameStarted(GameStartedEvent ev)
    {
        _target = ev.Player.transform;
    }

    public override void CalculateInputs()
    {
        targetPosition = _target.position;
    }
}
