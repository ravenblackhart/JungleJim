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
  [SerializeField] private float minY;
  [SerializeField] private LayerMask groundObject;
  [SerializeField] private Transform groundCheck;
  

  public Animator animator;
  //adding reference to UI Manager
  private UIManager uiManager;
  private TextMeshProUGUI scoreText;
  
  private Rigidbody2D rb;
  private bool isJumping = false;
  private bool facingRight = true;
  private bool isGrounded;
  private float horizontalMovment;
  
  
  private Vector3 cameraviewleft;
  private Camera _camera;
  
  
  
  
  //for Scoring 
  [Header("Scoring")]
  [SerializeField] private float scoreMultiplier = 1f;

  private Vector2 startPosition;
  private float distanceMoved;

  private TextMeshProUGUI finalScore;
  private PlayFabManager playFab;
  
  private void Awake()
  {
    _camera = Camera.main ;
    rb = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    
    uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    playFab = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayFabManager>();
    scoreText = uiManager.DistanceText; 
    
    startPosition = this.transform.position;
    finalScore = uiManager.FinalScoreText;
  }

  private void Update()
  {
    horizontalMovment = Input.GetAxis("Horizontal");
    
    if (horizontalMovment > 0 || horizontalMovment <0)
    {
      if (isGrounded)//only runs if he is on the ground
      {
        animator.SetBool("isRunning", true); //mathf to make sure its always positive
      }
    }
    else
    {
      animator.SetBool("isRunning", false);
    }


    if (Input.GetButtonDown("Jump") && isGrounded)
    {
      isJumping = true;
      FindObjectOfType<AudioManager>().PlaySFX("Jump");
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

    
    //Dead Check
    cameraviewleft = _camera.ViewportToWorldPoint(new Vector3(0f, 0f, _camera.transform.position.y));
    if (rb.position.y < minY || rb.position.x < cameraviewleft.x) // dead if player get hit by the camera in the back or falls off the map
    {
      OnDead();
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
  
  private void OnDead()
  {
    finalScore.text = scoreText.text;
    var prevHighscore = PlayerPrefs.GetFloat("High Score");
    var currentScore = distanceMoved; 

    if (currentScore > prevHighscore)
    {
      PlayerPrefs.SetFloat("High Score", currentScore);
      playFab.SendLeaderboard((int) distanceMoved);
    }
    
    uiManager.GameOver();
  }
  

}
