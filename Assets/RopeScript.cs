using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
   public Vector2 destiny;
   public float speed = 1f;
   public float nodeDistance = 1f;

   public GameObject nodePrefab;
   public GameObject player;
   public GameObject lastNode;

   private bool done = false; 


   private void Start()
   {
      player = GameObject.FindGameObjectWithTag("Player");
      lastNode = transform.gameObject;
   }

   void Update()
   {
      transform.position = Vector2.MoveTowards(transform.position, destiny, speed);

      if ((Vector2)transform.position != destiny)
      {
         if (Vector2.Distance(player.transform.position, lastNode.transform.position) > nodeDistance)
         {
            CreateNode();
         }
         
      }
      else if (!done)
      {
         done = true; 
         lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
      }
   }

   void CreateNode()
   {
      Vector2 pos2Create = player.transform.position - lastNode.transform.position; 
      
      pos2Create.Normalize();
      pos2Create *= nodeDistance;
      pos2Create += (Vector2)lastNode.transform.position;

      GameObject go = (GameObject) Instantiate((nodePrefab), pos2Create, Quaternion.identity);
      go.transform.SetParent(transform);
      lastNode.GetComponent<HingeJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();

      lastNode = go;
   }
}
