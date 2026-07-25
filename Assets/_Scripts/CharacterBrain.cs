using UnityEngine;

public class CharacterBrain : MonoBehaviour
{
    private CarController _controller;

    void Awake()
    {
        _controller = GetComponent<CarController>();   
    }

    void Update()
    {
        _controller.CalculateInputs();
    }

    void FixedUpdate()
    {
        _controller.Move();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<CharacterBrain>(out var characterBrain))
        {
        float mag = collision.thisRigidbody.linearVelocity.magnitude;
        float collidedMag = collision.rigidbody.linearVelocity.magnitude;
        float higherMag = mag > collidedMag ? mag : collidedMag;
        Vector3 pointOfImpact = collision.contacts[0].point;
        EventBus.Publish(new CollisionEvent(higherMag, collision.thisRigidbody, collision.rigidbody, pointOfImpact));
        }
    }
}
