using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class CarController : MonoBehaviour
{
    [SerializeField] protected float forwardForce = 3;
    [SerializeField] protected float rotationForce = 30;
    [SerializeField] protected float maxSpeed = 6;
    [SerializeField] protected float rotationDamping = 8f;
    [SerializeField] protected bool showTargetAnchor = false;
    protected Vector3 targetPosition;
    protected Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        rb.AddForce(transform.forward * forwardForce);

        Vector2 linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z);
        if (linearVelocity.magnitude > maxSpeed)
        {
            Vector3 limitVelocity = rb.linearVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limitVelocity.x, rb.linearVelocity.y, limitVelocity.z);
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 targetDirection = (targetPosition - rb.transform.position).normalized;

        float angle = Vector3.SignedAngle(
        transform.forward,
        targetDirection.normalized,
        Vector3.up);

        float steering = Mathf.Clamp(angle / 90f, -1f, 1f);

        float torque =
            steering * rotationForce
            - rb.angularVelocity.y * rotationDamping;

        rb.AddTorque(Vector3.up * torque, ForceMode.Acceleration);
    }

    private void OnDrawGizmos()
    {
        if (!showTargetAnchor)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, 0.5f);
        Gizmos.DrawLine(transform.position, targetPosition);
    }
}
