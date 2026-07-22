using UnityEngine;

public class CharacterBrain : MonoBehaviour
{
    private CharacterController _controller;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();   
    }

    void Start()
    {
        EventBus.Publish(new GameStartedEvent(this));
    }

    void Update()
    {
        _controller.CalculateInputs();
    }

    void FixedUpdate()
    {
        _controller.Move();
    }
}
