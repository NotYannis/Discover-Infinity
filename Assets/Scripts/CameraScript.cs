using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraScript : MonoBehaviour {
    public GameObject toFollow;
    public Vector3 baseOffset;
    private Vector3 offset;
    public float lerpForce = 0.5f;
    public bool isFollowing;



	// Use this for initialization
	void Start () {
        toFollow = GameObject.Find("Discover");
        offset = baseOffset;
    }

    void Update()
    {
        if (isFollowing)
        {
            Vector3 rot = toFollow.transform.eulerAngles;
            rot.z = Mathf.LerpAngle(transform.eulerAngles.z, rot.z, lerpForce);
            transform.eulerAngles = rot;

            float angle = rot.z * Mathf.Rad2Deg;
            offset.x = baseOffset.x * Mathf.Cos(angle) - baseOffset.y * Mathf.Sin(angle);
            offset.y = baseOffset.x * Mathf.Sin(angle) + baseOffset.y * Mathf.Cos(angle);

            Vector2 pos = toFollow.transform.position;
            pos.x = Mathf.Lerp(transform.position.x, pos.x, lerpForce);
            pos.y = Mathf.Lerp(transform.position.y, pos.y, lerpForce);

            transform.position = pos;
            Vector3 dir = transform.TransformDirection(Vector2.right);

            transform.position += dir * 0.5f;
        }
    }

    public void StopFollowing()
    {
        isFollowing = false;
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }
}
