using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEditor;
using UnityEngine;

public class Grab : MonoBehaviour
{

// [SerializeField] private LayerMask whatICangrab;
// [SerializeField] private Camera mainCamera;
// [SerializeField] private LineRenderer lineRenderer;
// [SerializeField] private DistanceJoint2D distanceJoint;
// [SerializeField] private float movespeed;
// [SerializeField] private float grablength;
//
// public int maxPoints = 10; //no i need?
//
// 
// private Rigidbody2D rb;
// 
// private List<Vector2> points = new List<Vector2>();
//
//
//
// // Start is called before the first frame update
// void Start()
// {
//  rb = GetComponent<Rigidbody2D>();
//
//  lineRenderer.positionCount = 0; // need?
//  //distanceJoint.enabled = false;
// }
//
// // Update is called once per frame
// void Update()
// {
//  if (Input.GetKeyDown(KeyCode.Mouse0))
//  {
//   Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
//   Vector2 direction = (mousePos - (Vector2) transform.position).normalized;
//
//   RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, grablength, whatICangrab);
//   if (hit.collider != null)
//   {
//    Vector2 hitPont = hit.point;
//    points.Add(hitPont);
//    if (points.Count > maxPoints)
//    {
//     points.RemoveAt(0);
//    }
//   }
//  }
//
//  if (points.Count > 0)
//  {
//   Vector2 moveTo = centroid(points.ToArray());
//   
//   rb.MovePosition(Vector2.MoveTowards(transform.position,moveTo,Time.deltaTime * movespeed)); //move the player to that direction
//
//   lineRenderer.positionCount = 0;
//   lineRenderer.positionCount = points.Count * 2;
//   for(int n=0,j=0; n < points.Count * 2; n+=2,j++) // one linerenderer everytime
//   {
//    lineRenderer.SetPosition(n,transform.position);
//    lineRenderer.SetPosition(n+1,points[j]);
//   }
//  }
//
//  else if ((Input.GetKeyUp(KeyCode.Mouse0)))
//  {
//   Detatch();
//  }
// }
//
// void Detatch()
// {
//  lineRenderer.positionCount = 0;
//  points.Clear();
// }
//
// Vector2 centroid(Vector2[] points)
// {
//  Vector2 center = Vector2.zero;
//  foreach (Vector2 point in points)
//  {
//   center += point;
//  }
//
//  center /= points.Length;
//  return center;
// }
//}

//else if (Input.GetKeyUp(KeyCode.Mouse0))
  //{
  //  distanceJoint.enabled = false;
  //  lineRenderer.enabled = false;
  //}
  //if (distanceJoint.enabled) 
  //{
  //  lineRenderer.SetPosition(1, transform.position);
  //}





  [SerializeField] private LayerMask whatICangrab;
  [SerializeField] private Camera mainCamera;
  [SerializeField] private LineRenderer lineRenderer;
  [SerializeField] private DistanceJoint2D distanceJoint;
  //[SerializeField] private float movespeed;
  [SerializeField] private float grablength;

 // private int max;
  private Rigidbody2D rb;
  private List<Vector2> points = new List<Vector2>();



  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();


    distanceJoint.enabled = false;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
      Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
      Vector2 direction = (mousePos - (Vector2) transform.position).normalized;

      RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, grablength, whatICangrab);
      
      
      
      if (hit.collider != null)
      {
       Vector2 hitPont = hit.point;
       points.Add(hitPont);
       
       lineRenderer.SetPosition(0, hit.point);
       lineRenderer.SetPosition(1, transform.position);
       
       // rb.MovePosition(Vector2.MoveTowards(transform.position,hit.point,Time.deltaTime * 1)); //move the player to that direction
       distanceJoint.connectedAnchor = hit.point;

       distanceJoint.enabled = true;
       lineRenderer.enabled = true;
      }
        
      //lineRenderer.enabled = true;
        
        


    }
    else if (Input.GetKeyUp(KeyCode.Mouse0))
    {
      distanceJoint.enabled = false;
      lineRenderer.enabled = false;
    }

    if (distanceJoint.enabled)
    {
      lineRenderer.SetPosition(1, transform.position);
    }

    }
  }

