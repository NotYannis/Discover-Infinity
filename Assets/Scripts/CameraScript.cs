using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraScript : MonoBehaviour {
    public GameObject toFollow;
    public Vector3 offset;
    public float lerpForce = 0.5f;
    public bool isFollowing;



	// Use this for initialization
	void Start () {
        toFollow = GameObject.Find("Discover");
    }

    void Update()
    {
        if (isFollowing)
        {
            Vector2 pos = toFollow.transform.position + offset;
            pos.x = Mathf.Lerp(transform.position.x, pos.x, lerpForce);
            pos.y = Mathf.Lerp(transform.position.y, pos.y, lerpForce);

            transform.position = pos;

            Quaternion rot = toFollow.transform.rotation;
            rot.z = Mathf.Lerp(transform.rotation.z, rot.z, lerpForce);

            transform.rotation = rot;
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
