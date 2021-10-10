using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] private float movmentSpeed;
  [SerializeField] private float jumpForce;
  [SerializeField] private float checkRadius;
  [SerializeField] private LayerMask groundObject;
  [SerializeField] private Transform groundCheck;
  
  //adding reference to UI Manager
  private UIManager uiManager;
  private TextMeshProUGUI scoreText;
  
  private Rigidbody2D rb;
  private bool isJumping = false;
  private bool facingRight = true;
  private bool isGrounded;
  private float horizontalMovment;
  
  //for Scoring 
  [Header("Scoring")]
  [SerializeField] private float scoreMultiplier;

  private Vector2 startPosition;
  private float distanceMoved; 




  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    scoreText = 
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
    
    //scoring script 

    if (Mathf.Round(transform.position.x - startPosition.x) > distanceMoved)
    {
      distanceMoved = Mathf.Round(transform.position.x - startPosition.x); 
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

  #region Vivienne

  // Added script components for : 
  // - Death State

  private void OnBecameInvisible()
  {
    uiManager.GameOver();
  }

  #endregion
}
