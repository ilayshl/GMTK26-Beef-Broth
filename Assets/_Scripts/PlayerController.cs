using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CarController
{
    private Camera _camera;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    void Start()
    {
        EventBus.Publish(new GameStartedEvent(rb));
    }

    public override void CalculateInputs()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mouseScreenPos);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(ray, out float distance))
        {
            targetPosition = ray.GetPoint(distance);
        }
    }
}
