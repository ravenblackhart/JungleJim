using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectionscript : MonoBehaviour
{
    //[SerializeField] private Grab grabScript;
    //[SerializeField] private LineRenderer lineRenderer;

    private Grab grab;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private int nrOfPoints = 50;
    [SerializeField] private float timeBetweenPoints = 0.1f;

    [SerializeField]
    private LayerMask whatICangrab; // the physics layers that will cause the line to stop being drawn or start?

    [SerializeField] private float grablength = 15f;

    void Start()
    {
        grab = GetComponent<Grab>();
        //lineRenderer = GetComponent<LineRenderer>();
    }


    void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition); // takes mouspos from screentowoldpoint
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, whatICangrab);
        // vector.zero to decide the direction so the only thing we click get "hit". Decide grablenght and what we can grab



        if (hit.collider != null) // if we hit a collider and it have the right layermask etc do this
        {
            Vector2 hitPoint = hit.point;
            Vector2 characterPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 difference = hitPoint - characterPos;


            if (difference.magnitude <= grablength)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, mousePos2D);
                //distanceJoint.connectedAnchor = hit.point;

                lineRenderer.enabled = true;
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
    }
}
