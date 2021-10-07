using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] private float movmentSpeed;
  [SerializeField] private float jumpForce;
  [SerializeField] private float checkRadius;
  [SerializeField] private LayerMask groundObject;
  [SerializeField] private Transform groundCheck;
  
  
  private Rigidbody2D rb;
  private bool isJumping = false;
  private bool facingRight = true;
  private bool isGrounded;
  private float horizontalMovment;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    
  }

  private void Update()
  {
    horizontalMovment = Input.GetAxis("Horizontal");
    if (Input.GetButtonDown("Jump") && isGrounded)
    {
      isJumping = true;
      Debug.Log(isJumping);
      //FindObjectOfType<AudioManager>().Play("Jump");
    }

    if (horizontalMovment > 0 && !facingRight)
    {
      FlipCharacter();
    }
    else if (horizontalMovment < 0 && facingRight)
    {
      FlipCharacter();
    }
    
  }

  private void FixedUpdate()
  {
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObject);
    
    rb.velocity = new Vector2(horizontalMovment * movmentSpeed, rb.velocity.y);
    if (isJumping)
    {
      rb.AddForce(new Vector2(0f,jumpForce * 5));
    }
    isJumping = false;
  }

  private void FlipCharacter()
  {
    facingRight = !facingRight;
    transform.Rotate(0,180,0);
  }
}
