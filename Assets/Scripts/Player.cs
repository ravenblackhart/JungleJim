using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpforce;
    [SerializeField] private float movmentSpeed;
    private Rigidbody2D rb2d;
    private float horizontalMovment;
    private bool grab;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovment = Input.GetAxis("Horizontal");
        
        if (horizontalMovment != 0)
        {
            transform.position += new Vector3(horizontalMovment, 0, 0) * Time.deltaTime * movmentSpeed;
        }
        else
        {
            transform.position += new Vector3(0, 0, 0);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
           //jump
        }
    }

}
