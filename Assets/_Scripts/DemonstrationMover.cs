using UnityEngine;
using UnityEngine.InputSystem;

public class DemonstrationMover : MonoBehaviour
{
    [SerializeField] private float forwardForce;
    [SerializeField] private float rotationForce;
    [SerializeField] private float maxSpeed;

    private Camera _camera;
    private Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        MoveForward();
        RotateTowardsMouse();
    }

    private void MoveForward()
    {
        _rb.AddForce(transform.forward * forwardForce);
        
        Vector2 linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.z);
        Debug.Log($"[{name}] {linearVelocity.magnitude}");
        if(linearVelocity.magnitude > maxSpeed)
        {
            Vector3 limitVelocity = _rb.linearVelocity.normalized * maxSpeed;
                _rb.linearVelocity = new Vector3(limitVelocity.x, _rb.linearVelocity.y, limitVelocity.z);
        }
    }

    private void RotateTowardsMouse()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mouseScreenPos);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Vector3 mousePosition = new Vector3();
        if (groundPlane.Raycast(ray, out float distance))
        {
            mousePosition = ray.GetPoint(distance);
        }

        Vector3 targetDirection = (mousePosition - transform.position).normalized;

        Vector3 newDirection = Vector3.RotateTowards(
    transform.forward,
    targetDirection.normalized,
    rotationForce * Mathf.Deg2Rad * Time.fixedDeltaTime,
    0f);

    Vector3 eulers = Quaternion.LookRotation(newDirection).eulerAngles;
    eulers.x = 0;
    eulers.z = 0;

_rb.MoveRotation(Quaternion.Euler(eulers));
    }

}
