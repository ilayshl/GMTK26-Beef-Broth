using UnityEngine;

public class CharacterBrain : MonoBehaviour
{
    private CarController _controller;
    private CollisionDetector _collisionDetector;
    private int _health = 100;

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

    private void OnHitReceived(int value)
    {
        _health -= value;
        Debug.Log($"[{name}] Took a hit of {value}, current health: {_health}");
    }
}
