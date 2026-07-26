using UnityEngine;

public class CarBrain : MonoBehaviour
{
    public bool IsPlayer;
    private CarController _controller;
    private CollisionDetector _collisionDetector;

    void Awake()
    {
        _controller = GetComponent<CarController>();
        _collisionDetector = GetComponent<CollisionDetector>();
        _collisionDetector.HitReceived += OnHitReceived;
    }

    void Update()
    {
        _controller.CalculateInputs();
    }

    void FixedUpdate()
    {
        _controller.Move();
    }

    private void OnHitReceived()
    {
        if(!IsPlayer)
        {
            TimerManager.Instance.AddTime(3);
        }
        else
        {
            TimerManager.Instance.AddTime(-5);
        }
    }
}
