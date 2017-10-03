using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Planet : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler{
    public float mass;
    public float minDistance;
    public float maxDistance;
    public float gravityForce;

    private GameObject gravityField;
    private CameraScript mainCamera;
    private bool isDragged;
    private Vector2 currentPos;

    void Start()
    {
        mainCamera = Camera.main.GetComponent<CameraScript>();

        maxDistance = mass * 5;
        transform.localScale *= mass;

        gravityField = GameObject.Find(gameObject.name + "/gravity");
        gravityField.transform.localScale = Vector3.one;
        gravityField.transform.localScale = new Vector3(maxDistance/gravityField.transform.lossyScale.x,
                                                        maxDistance/ gravityField.transform.lossyScale.y, 1.0f);
        minDistance = GetComponent<SpriteRenderer>().size.magnitude;
    }

    public Vector2 Attract(Mover m)
    {
        Vector2 force = transform.position - m.transform.position;

        float distance = Mathf.Clamp(force.magnitude, minDistance, maxDistance);

        force = force.normalized;
        float strength = (gravityForce * mass * m.mass) / (distance * distance);
        force *= strength;

        return force;
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = 0.3f;
        mainCamera.StopFollowing();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Time.timeScale = 1.0f;
        mainCamera.StartFollowing();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentPos = Camera.main.ScreenToWorldPoint(eventData.position);
        isDragged = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 previousPos = currentPos;
        currentPos = Camera.main.ScreenToWorldPoint(eventData.position);

        Vector2 deltaPos = currentPos - previousPos;
        transform.position += (Vector3)deltaPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
