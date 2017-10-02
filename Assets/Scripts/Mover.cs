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
        transform.position += (Vector3)velocity;
        acceleration.Scale(Vector3.zero);
    }

    public void Reset()
    {
        transform.position = basePos;
        velocity = Vector2.zero;
        acceleration = Vector2.zero;
    }
}
