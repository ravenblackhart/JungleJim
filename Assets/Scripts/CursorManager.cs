using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    [SerializeField] private LayerMask whatICangrab;
    private Camera mainCamera;

    [SerializeField] private float grablength;
    [SerializeField] private Texture2D cursorTextureOn;
    private bool mouseIsOver;

    // private Player player;
    private void Awake()
    {
        mainCamera = Camera.main;
    }



    private void OnMouseOver()
    {
        mouseIsOver = true;
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(default, new Vector2(0, 0), CursorMode.Auto);
    }



    // Update is called once per frame
    void Update()
    {

        if (mouseIsOver == true)
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
                    Cursor.SetCursor(cursorTextureOn, new Vector2(10, 10), CursorMode.Auto);
                }


            }

        }
    }
}


