using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    Vector2 basePos;

    [System.NonSerialized]
    public Vector2 velocity;
    [System.NonSerialized]
    public Vector2 acceleration;

    public float mass;
    public bool limitVelocity;
    public float maxVelocity = 1.0f;

	// Use this for initialization
	void Start () {
        basePos = transform.position;
	}

    public void ApplyForce(Vector2 force)
    {
        force /= mass;
        acceleration += force;
    }
    
    public void Move()
    {
        velocity += acceleration;
        if (limitVelocity)
        {
            float mag = velocity.magnitude;
            if (mag < -maxVelocity)
            {
                velocity = velocity.normalized;
                velocity *= -maxVelocity;
            }
            else if (mag > maxVelocity)
            {
                velocity = velocity.normalized;
                velocity *= maxVelocity;
            }
        }

        acceleration.Scale(Vector3.zero);
        transform.position += (Vector3)velocity * Time.deltaTime;

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Reset()
    {
        transform.position = basePos;
        velocity = Vector2.zero;
        acceleration = Vector2.zero;
    }
}
