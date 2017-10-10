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
    private Vector2 lastPos;
    private Vector2 currentPos;
    private bool goBack;

    //Ellipse movement
    public Vector2 systemCenter = Vector2.zero;
    public float semimajor;
    public float semiminor;
    public float planetVelocity = 0.1f;
    private float ellipsePos = 0.0f;

    //Ellipse renderer
    private LineRenderer ellipseRenderer;

    void Start()
    {
        mainCamera = Camera.main.GetComponent<CameraScript>();
    }

    public void UpdateEllipse()
    {
        //Ellipse points;
        float imax = 10;
        Vector3[] points = new Vector3[(int)imax + 1];
        for (float i = 1; i <= imax + 1; ++i)
        {
            float x = systemCenter.x + (semimajor * Mathf.Cos(Mathf.PI * (i / (imax / 2.0f))));
            float y = systemCenter.y + (semiminor * Mathf.Sin(Mathf.PI * (i / (imax / 2.0f))));
            points[(int)i - 1] = new Vector3(x, y, transform.position.z);
        }


        Vector3[] smoothPoints = LineSmoother.SmoothLine(points, 0.1f);

        ellipseRenderer = GetComponent<LineRenderer>();
        ellipseRenderer.positionCount = smoothPoints.Length;
        ellipseRenderer.SetPositions(smoothPoints);

        transform.position = points[0];
    }

    public void UpdatePlanetRenderer()
    {
        maxDistance = mass * 5;
        transform.localScale = new Vector3(mass * 1.5f, mass * 1.5f, 0.0f);

        gravityField = GameObject.Find(gameObject.name + "/gravity");
        gravityField.transform.localScale = Vector3.one;
        gravityField.transform.localScale = new Vector3(maxDistance / gravityField.transform.lossyScale.x,
                                                        maxDistance / gravityField.transform.lossyScale.y, 1.0f);
        minDistance = GetComponent<SpriteRenderer>().size.magnitude;
    }

    void Update()
    {        
        if(transform.position != EllipseNextPosition())
        {
            lastPos = transform.position;
        }

        transform.position = EllipseNextPosition();

        if (!isDragged)
        {
            ellipsePos += planetVelocity;
        }
    }

    Vector3 EllipseNextPosition()
    {
        ellipsePos %= 2 * Mathf.PI;

        float x = systemCenter.x + (semimajor * Mathf.Cos(ellipsePos));
        float y = systemCenter.y + (semiminor * Mathf.Sin(ellipsePos));

        return new Vector3(x, y, transform.position.z);
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
        isDragged = true;
        Time.timeScale = 0.3f;
        mainCamera.StopFollowing();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragged = false;
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
        Vector2 nextPos = (Vector2)EllipseNextPosition() - lastPos;

        float angle = Vector2.Angle(nextPos, deltaPos);
        int dragSpeed = 5;
        float velocity = 1 - (angle / 90);
        float ellipsePosTemp = ellipsePos;

        if (goBack)
        {
            ellipsePos -= planetVelocity * velocity * dragSpeed;
        }
        else
        {
            ellipsePos += planetVelocity * velocity * dragSpeed;
        }

        if (ellipsePos < ellipsePosTemp && !goBack)
        {
            goBack = true;
        }
        else if(ellipsePos > ellipsePosTemp && goBack)
        {
            goBack = false;
        }


    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
