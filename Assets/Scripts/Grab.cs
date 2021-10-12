using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEditor;
using UnityEngine;

public class Grab : MonoBehaviour
{
  [SerializeField] private LayerMask whatICangrab;
  [SerializeField] private Camera mainCamera;
  [SerializeField] private LineRenderer lineRenderer;
  [SerializeField] private DistanceJoint2D distanceJoint;
  
  [SerializeField] private float grablength;
  

  void Start()
  {
    distanceJoint.enabled = false;
    lineRenderer.enabled = false;
  }

  // Update is called once per frame
  void Update()
  {
    
    if (Input.GetKeyDown(KeyCode.Mouse0))
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
          FindObjectOfType<AudioManager>().PlaySFX("Snake"); 
          lineRenderer.SetPosition(0, hitPoint);
          lineRenderer.SetPosition(1, transform.position);

          
          distanceJoint.connectedAnchor = hit.point; // set the anchorpoint to hit.point
          distanceJoint.enabled = true;
          lineRenderer.enabled = true;
        }
      }
      else
      {
        Debug.Log("you missed");
      }
    }
    else if (Input.GetKeyUp(KeyCode.Mouse0))
    {
      distanceJoint.enabled = false;
      lineRenderer.enabled = false;
    }

    if (distanceJoint.enabled)
    {
      lineRenderer.SetPosition(1, transform.position); // so the line follows the character
    }
  }
}


