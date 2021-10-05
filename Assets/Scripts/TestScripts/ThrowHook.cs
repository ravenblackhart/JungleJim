using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHook : MonoBehaviour
{
   public GameObject hook;
   private GameObject curHook;

   private void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         Vector2 destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         curHook = (GameObject)Instantiate(hook, transform.position, Quaternion.identity);
         curHook.GetComponent<RopeScript>().destiny = destiny;

      }
   }
}
