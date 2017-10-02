using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {
    public float mass;
    public float minDistance = 5.0f;
    public float maxDistance = 25.0f;

    public Vector2 Attract(Mover m)
    {
        Vector2 force = transform.position - m.transform.position;
        float distance = Mathf.Clamp(force.magnitude, minDistance, maxDistance);

        force = force.normalized;
        float strength = (1 * mass * m.mass) / (distance * distance);
        force *= strength;

        return force;
    }
	// Use this for initialization
	void Start () {
        transform.localScale *= mass / 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
