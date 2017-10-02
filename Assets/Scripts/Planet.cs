using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Planet : MonoBehaviour, IBeginDragHandler, IDragHandler{
    public float mass;
    public float minDistance = 5.0f;
    public float maxDistance = 25.0f;

    private bool isDragged;
    private Vector2 currentPos;

    public Vector2 Attract(Mover m)
    {
        Vector2 force = transform.position - m.transform.position;
        float distance = Mathf.Clamp(force.magnitude, minDistance, maxDistance);

        force = force.normalized;
        float strength = (1 * mass * m.mass) / (distance * distance);
        force *= strength;

        return force;
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
        Debug.print(deltaPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
