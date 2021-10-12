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
    [SerializeField] private OldAudioManager oldAudioManagerPrefab;
    [SerializeField] private UIManager uiManagerPrefab;

    void Awake()
    {
        GameObject[] mgm = GameObject.FindGameObjectsWithTag("GameController");
        if (mgm.Length > 1) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        
        //Event System
        if (FindObjectOfType<EventSystem>() == null)
        {
            var eventSystem = 
                new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }

        #region UI Manager

        GameObject[] mui = GameObject.FindGameObjectsWithTag("UIManager");
        if (mui.Length > 1)
        {
            for (int i = 1; i < mui.Length; i++)
            {
                Destroy(mui[i]);
            }
        }
        
        else if (mui.Length < 1)
        {
            Instantiate(uiManagerPrefab);
        }

        #endregion

        #region AudioManager

        GameObject[] mam = GameObject.FindGameObjectsWithTag("AudioManager");
        if (mam.Length > 1)
        {
            for (int i = 1; i < mam.Length; i++)
            {
                Destroy(mam[i]);
            }
        }
        
        else if (mam.Length < 1)
        {
            Instantiate(oldAudioManagerPrefab);
        }

        #endregion
    }

}
