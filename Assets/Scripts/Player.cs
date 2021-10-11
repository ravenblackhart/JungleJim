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
  [SerializeField] private float minY;

  private Animator Ani;
  //adding reference to UI Manager
  private UIManager uiManager;
  private TextMeshProUGUI scoreText;
  
  private Rigidbody2D rb;
  private bool isJumping = false;
  private bool facingRight = true;
  private bool isGrounded;
  private float horizontalMovment;
  
  private Vector3 cameraviewright;
  private Vector3 cameraviewleft;

  private Camera _camera;
  
  
  //for Scoring 
  [Header("Scoring")]
  [SerializeField] private float scoreMultiplier = 1f;

  private Vector2 startPosition;
  private float distanceMoved;

  private TextMeshProUGUI finalScore; 



  private void Awake()
  {
    _camera = Camera.main ;
    
    rb = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    
    uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    scoreText = uiManager.DistanceText; 
    
    Ani = transform.GetChild(0).GetComponent<Animator>();
    startPosition = this.transform.position;
    finalScore = uiManager.FinalScoreText;
  }

  private void Update()
  {
    horizontalMovment = Input.GetAxis("Horizontal");
    if (horizontalMovment > 0 || horizontalMovment < 0) Ani.SetBool("isMoving", true);
    else if (horizontalMovment == 0 ) Ani.SetBool("isMoving", false);
 
    
    if (Input.GetButtonDown("Jump") && isGrounded)
    {
      Ani.SetBool("isJumping", true);
      isJumping = true;
      Debug.Log(isJumping);
      FindObjectOfType<AudioManager>().Play("Jump");
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
      distanceMoved = Mathf.Round(transform.position.x - startPosition.x) * scoreMultiplier; 
    }

    scoreText.text = $"{distanceMoved} m";

    if (rb.position.y < minY)
    {
      OnDead();
      Debug.Log("dead");
    }
    if (rb.position.x - 1f < cameraviewleft.x) // dead if player get hit by the camera in the back
    {
      OnDead();
    }
  }

  private void FixedUpdate()
  {
    
    cameraviewright = _camera.ViewportToWorldPoint(new Vector3(1f, 1f, _camera.transform.position.y));
    cameraviewleft = _camera.ViewportToWorldPoint(new Vector3(0f, 0f, _camera.transform.position.y));
    
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObject);
    
    rb.velocity = new Vector2(horizontalMovment * movmentSpeed, rb.velocity.y);
    if (isJumping)
    {
      rb.AddForce(new Vector2(0f,jumpForce * 5));
    }
    isJumping = false;
    Ani.SetBool("isJumping", false);

    
  }

  private void FlipCharacter()
  {
    facingRight = !facingRight;
    transform.Rotate(0,180,0);
  }
  

  

  private void OnDead()
  {
    finalScore.text = scoreText.text;

    uiManager.GameOver();
  }

 
}
