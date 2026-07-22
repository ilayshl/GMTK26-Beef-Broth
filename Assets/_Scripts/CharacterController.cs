using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class CharacterController : MonoBehaviour
{
    [SerializeField] protected float forwardForce = 3;
    [SerializeField] protected float rotationForce = 30;
    [SerializeField] protected float maxSpeed = 6;
    [SerializeField] protected float rotationDamping = 8f;
    protected Vector3 targetPosition;
    private Rigidbody _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public abstract void CalculateInputs(); //Each inheritance will use their own inputs.


    //The movement logic is technically the same for every inheritance ->
    //move forward and rotate towards target.
    //The target may change.
    public void Move()
    {
        MoveForward();
        RotateTowardsTarget();
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

    private void RotateTowardsTarget()
    {
        Vector3 targetDirection = (targetPosition - _rb.transform.position).normalized;

        float angle = Vector3.SignedAngle(
    transform.forward,
    targetDirection.normalized,
    Vector3.up);

    float steering = Mathf.Clamp(angle / 90f, -1f, 1f);

float torque =
    steering * rotationForce
    - _rb.angularVelocity.y * rotationDamping;

_rb.AddTorque(Vector3.up * torque, ForceMode.Acceleration);
    }
}
