using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    
    [Header("Controller Scripts")] 
    [SerializeField] private protected AudioManager audioManager;
    private protected UIManager uiManager;

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

        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        #endregion

        // #region PlayerController
        //
        // if (SceneManager.GetActiveScene().buildIndex != 0)
        // {
        //     GameObject[] mpc = GameObject.FindGameObjectsWithTag("Player");
        //     if (mpc.Length > 1)
        //     {
        //         for (int i = 1; i < mpc.Length; i++)
        //         {
        //             Destroy(mpc[i]);
        //         }
        //     }
        //     else if (mpc.Length < 1)
        //     {
        //         Instantiate(player);
        //     }
        // }
        // #endregion

    }
}
