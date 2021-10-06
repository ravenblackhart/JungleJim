using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [Header("Controller Scripts")] 
    [SerializeField] private protected AudioManager audioManager;
    [SerializeField] private protected UIManager uiManager;

    void Awake()
    {
        GameObject[] mgm = GameObject.FindGameObjectsWithTag("GameController");
        if (mgm.Length > 1) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        
        #region UIManager

        if (FindObjectOfType<EventSystem>() == null)
        {
            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            GameObject[] mui = GameObject.FindGameObjectsWithTag("UIController");
            if (mui.Length > 1)
            {
                for (int i = 1; i < mui.Length; i++)
                {
                    Destroy(mui[i]);
                }
            }
        
            else if (mui.Length < 1)
            {
                Instantiate(uiManager);
            }
        }
       
        #endregion

        #region PlayerController

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            GameObject[] mpc = GameObject.FindGameObjectsWithTag("Player");
            if (mpc.Length > 1)
            {
                for (int i = 1; i < mpc.Length; i++)
                {
                    Destroy(mpc[i]);
                }
            }
            else if (mpc.Length < 1)
            {
                Instantiate(player);
            }
        }
        #endregion

    }
}
