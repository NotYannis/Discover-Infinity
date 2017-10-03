using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Debug : MonoBehaviour {
    Mover m;
    Vector2 maxVel = Vector2.zero;
    Vector2 maxAcc = Vector2.zero;

    Text acc;
    Text vel;
    Text accmax;
    Text velmax;
    // Use this for initialization
    void Start () {
        vel = GameObject.Find("vel").GetComponent<Text>();
        acc = GameObject.Find("acc").GetComponent<Text>();
        velmax = GameObject.Find("velmax").GetComponent<Text>();
        accmax = GameObject.Find("accmax").GetComponent<Text>();
        m = GameObject.Find("Discover").GetComponent<Mover>();
	}
	
	// Update is called once per frame
	void Update () {
        if(m.velocity.magnitude > maxVel.magnitude)
        {
            maxVel = m.velocity;
        }
        if(m.acceleration.magnitude > maxAcc.magnitude)
        {
            maxAcc = m.acceleration;
        }

        vel.text = "Vel : " + m.velocity.ToString();
        velmax.text = "Max : " + maxVel.ToString();
        acc.text = "Acc : " + m.acceleration.ToString();
        accmax.text = "Max : " + maxAcc.ToString();

    }
}
