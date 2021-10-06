using UnityEngine;

public class Grab : MonoBehaviour
{
  public LayerMask whatICangrab;
  public Camera mainCamera;
  public LineRenderer lineRenderer;
  public DistanceJoint2D distanceJoint;

  

  // Start is called before the first frame update
  void Start()
  {
    distanceJoint.enabled = false;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
      Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
      lineRenderer.SetPosition(0, mousePos);
      lineRenderer.SetPosition(1, transform.position);
      distanceJoint.connectedAnchor = mousePos;
      
      distanceJoint.enabled = true;
      lineRenderer.enabled = true;

      
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
  //  private Vector3 mousePos;
  //  private Camera cam;
  //  private bool study;
  //  private DistanceJoint2D joint;
  //  private LineRenderer linRenderer;
//
  //  private Vector3 tempPos;
//
  //  private void Start()
  //  {
  //      cam = Camera.main;
  //      joint = GetComponent<DistanceJoint2D>();
//
  //      joint.enabled = false;
  //      study = true;
//
  //      linRenderer = GetComponent<LineRenderer>();
  //      linRenderer.positionCount = 0;
//
  //      DrawLine();
  //  }
//
  //  private void Update()
  //  {
  //      MousePos();
  //      if (Input.GetMouseButtonDown(0) && study)
  //      {
  //          joint.enabled = true;
  //          joint.connectedAnchor = mousePos;
  //          linRenderer.positionCount = 2;
  //          study = false;
  //          tempPos = mousePos;
  //      }
  //      else if (Input.GetMouseButtonDown(0))
  //      {
  //          joint.enabled = false;
  //          study = true;
//
  //          linRenderer.positionCount = 0;
  //      }
  //  }
//
  //  private void MousePos()
  //  {
  //      mousePos = cam.ScreenToViewportPoint(Input.mousePosition);
  //  }
//
  //  private void DrawLine()
//
  //  {
  //      if (linRenderer.positionCount <= 0) return;
  //      {
  //          linRenderer.SetPosition(0,transform.position);
  //          linRenderer.SetPosition(1,tempPos);
  //      }
  //  }
//
    //  [SerializeField]private float timer ;
    //  
    //  public Camera mainCamera;
    //  public LineRenderer _lineRenderer;
    //  public Joint2D _distanceJoint;
    // // public Transform player;
//
    //  public LayerMask whatICangrab;
    //  private RaycastHit hit;
//
    // 
    //  
    //   void Start()
    //  {
    //      _distanceJoint.enabled = false;
    //  }
    //  
    //  // Update is called once per frame
    //  void Update()
    //  {
    //      Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
    //    
    //      if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(mousePos, Vector3.back, out hit, whatICangrab))
    //      {
    //          
    //          _lineRenderer.SetPosition(0, mousePos);
    //          _lineRenderer.SetPosition(1, transform.position);
    //          _distanceJoint.connectedAnchor = mousePos;
    //          _distanceJoint.enabled = true;
    //          _lineRenderer.enabled = true;
    //  
    //          timer -= 1f;
    //  
    //      }
    //      if(timer < 0f)
    //      {
    //          _distanceJoint.enabled = false;
    //          _lineRenderer.enabled = false;
    //      }
    //      if (_distanceJoint.enabled) 
    //      {
    //          _lineRenderer.SetPosition(1, transform.position);
    //      }
    //      
    //     
    //      
    //  }
    // 

    }
}
