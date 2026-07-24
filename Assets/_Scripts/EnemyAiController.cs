using UnityEngine;

public class EnemyAiController : CharacterController
{
    [SerializeField] private Transform target;
    private Transform _target;

    protected override void Awake()
    {
        base.Awake();
        if(target == null)
        {
            EventBus.Subscribe<GameStartedEvent>(OnGameStarted);
        }
        else
        {
            _target = target;
        }
    }

    void OnDestroy()
    {
        if(target == null)
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
