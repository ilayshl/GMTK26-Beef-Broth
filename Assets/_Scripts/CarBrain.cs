using System;
using UnityEngine;

public class CarBrain : MonoBehaviour
{
    public event Action<CarBrain, int, int> HealthChanged;
    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;
    private CarController _controller;
    private CollisionDetector _collisionDetector;
    private int _maxHealth = 10;
    private int _currentHealth;

    void Awake()
    {
        _controller = GetComponent<CarController>();
        _collisionDetector = GetComponent<CollisionDetector>();
        //_healthDisplay = GetComponentInChildren<HealthDisplay>();
        _collisionDetector.HitReceived += OnHitReceived;
    }

    void Start()
    {
        _currentHealth = _maxHealth;
        EventBus.Publish(new CarStartedEvent(this));
    }

    void Update()
    {
        _controller.CalculateInputs();
    }

    void FixedUpdate()
    {
        _controller.Move();
    }

    void OnDestroy()
    {
        EventBus.Publish(new CarDestroyedEvent(this));
    }

    private void OnHitReceived()
    {
        _currentHealth -= 1;
        HealthChanged?.Invoke(this, _currentHealth, _maxHealth);
    }
}
