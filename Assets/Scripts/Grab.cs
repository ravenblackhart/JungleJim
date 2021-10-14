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
  
  private Vector2 tailposition;
  private float offsettail = 0.6f;
  private Player player;
  void Start()
  {
    player = GetComponent<Player>();
    distanceJoint.enabled = false;
    lineRenderer.enabled = false;
  }

  // Update is called once per frame
  void Update()
  {
    
    if (player.facingRight) //So the tail/grab is on the right place when changing left & right
    {
      tailposition = new Vector2(transform.localPosition.x - offsettail, transform.position.y);
    }

    if (!player.facingRight)
    {
      tailposition = new Vector2(transform.localPosition.x + offsettail, transform.position.y);
    }
    
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
       


        if (difference.magnitude <= grablength) //if we are in length to grab
        {
          FindObjectOfType<AudioManager>().PlaySFX("Snake"); 
          lineRenderer.SetPosition(0, hitPoint);
          lineRenderer.SetPosition(1, tailposition);
          
          
          distanceJoint.connectedAnchor = hit.point; // set the anchorpoint to hit.point
          distanceJoint.enabled = true;
          lineRenderer.enabled = true;
          lineRenderer.startWidth = 0.01f;
          lineRenderer.endWidth = 0.3f;
        }
      }
    }
    else if (Input.GetKeyUp(KeyCode.Mouse0))
    {
      distanceJoint.enabled = false;
      lineRenderer.enabled = false;
    }

    if (distanceJoint.enabled)
    {
      lineRenderer.SetPosition(1, tailposition); // so the line follows the character
    }
  }
}


