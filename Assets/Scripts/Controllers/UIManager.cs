using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

[System.Serializable]
public class MyEvent : UnityEvent<GameObject>
{
}

public class UIManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] [CanBeNull] private Canvas homeScreen;
    [SerializeField] [CanBeNull] private Canvas gameScreen;
    
    [Header("Main Menu - Canvases")] 
    [SerializeField] [CanBeNull] private Canvas settingsPanel;
    [SerializeField] [CanBeNull] private Canvas creditsPanel;
    [SerializeField] [CanBeNull] private Canvas leaderboardPanel;

    [Header("Main Menu - UI Elements")] 
    [SerializeField] [CanBeNull] private TMP_InputField setUID;
    [SerializeField] [CanBeNull] private TextMeshProUGUI saveMessage;

    [Header("In-game UI - HUD")] 
    [SerializeField] [CanBeNull] private Canvas HUD;
    [SerializeField] [CanBeNull] public TextMeshProUGUI DistanceText;

    [Header("In-game UI - Canvases")] 
    [SerializeField] [CanBeNull] private Canvas pausePanel;
    [SerializeField] [CanBeNull] private Canvas gameOverPanel;
    [SerializeField] [CanBeNull] public TextMeshProUGUI FinalScoreText;

    [Header("UI Animation Settings")] 
    [SerializeField] private float animationDuration = 5f;

    #endregion

    private float posXIn = 0f;
    private float posYIn = 120f;

    private float posXOut = 0f;
    private float posYOut = 1175f;

    private float elapsedAnimDuration = 0;
    private float percentAnim;
    private Vector2 startPosition;
    private Vector2 targetPosition;

    private bool animatePanel = false;
    private RectTransform animTarget;

    private PlayFabManager playFab;

    void Awake()
    {
        Time.timeScale = 1.0f;
        
        if (SceneManager.GetActiveScene().name == "0_MainMenu")
        {
            homeScreen.enabled = true;
            settingsPanel.enabled = false;
            saveMessage.enabled = false;
            creditsPanel.enabled = false;
            leaderboardPanel.enabled = false;
        }

        else if (SceneManager.GetActiveScene().name != "0_MainMenu")
        {
            gameScreen.enabled = true;
            HUD.enabled = true;
            pausePanel.enabled = false;
            gameOverPanel.enabled = false;
        }

        playFab = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayFabManager>();

    }

    public void Update()
    {
        if (animatePanel && elapsedAnimDuration < animationDuration)
        {
            startPosition = animTarget.transform.localPosition;
            percentAnim = (elapsedAnimDuration / animationDuration);
            elapsedAnimDuration += Time.unscaledDeltaTime;

            animTarget.transform.localPosition =
                Vector2.Lerp(startPosition, targetPosition, percentAnim);
            
            if (startPosition == targetPosition || elapsedAnimDuration >= animationDuration || percentAnim >= 0.95f)
            {
                animatePanel = false;
                elapsedAnimDuration = 0f;

                if (targetPosition.y == posYOut)
                {
                    animTarget.transform.GetComponent<Canvas>().enabled = false;
                }
            }

        }
    }
    

    #region Main Menu UI Functions

    public void StartLevel()
    {
        if (PlayerPrefs.GetString("Username") == String.Empty)
        {
            settingsPanel.enabled = true;
            targetPosition.Set(posXIn, posYIn);
            SlidePanel(settingsPanel);
            setUID.Select();
            saveMessage.enabled = true; 
            saveMessage.text = $"Set your User ID!"; 
            saveMessage.color = new Color32(221, 210, 189, 255);
            saveMessage.fontWeight = FontWeight.Bold;
            saveMessage.fontSize = 42; 
        }
        else SceneManager.LoadScene("1_GameLevel"); //Use this script for playButton & restartButton
    } 
    
    public void OpenPanel(Canvas panel)
    {
        panel.enabled = true;
        targetPosition.Set(posXIn, posYIn);
        SlidePanel(panel);

        if (panel == settingsPanel)
        {
            setUID.text = PlayerPrefs.GetString("Username");
        }
    }

    public void ClosePanel(Canvas panel)
    {
        targetPosition.Set(posXOut, posYOut);
        SlidePanel(panel);

        saveMessage.enabled = false;

    }

    void SlidePanel(Canvas targetPanel)
    {
        animTarget = targetPanel.GetComponent<RectTransform>();
        animatePanel = true;
    }

    public void SaveUID()
    {
        saveMessage.enabled = true;
        

        if (setUID.text.Length < 3 || setUID.text.Length > 12)
        {
            saveMessage.color = new Color32(255, 136, 110, 255);
            saveMessage.fontWeight = FontWeight.Bold;
            saveMessage.fontSize = 42;
            
            if (setUID.text.Length < 3) saveMessage.text = $"Please Enter At least 3 characters!";
            if (setUID.text.Length > 12) saveMessage.text = $"Username should be maximum 12 characters long";
        }

        else
        {
            saveMessage.color = new Color32(99, 65, 60, 255);
            saveMessage.fontWeight = FontWeight.Regular;
            saveMessage.fontSize = 24;
            
            if (setUID.text.Length > 3 && setUID.text != PlayerPrefs.GetString("Username"))
            {
                PlayerPrefs.SetString("Username", setUID.text);
                playFab.SavePlayerID();
                saveMessage.text = $"Your User ID has been saved!";

            }
        
            else if (setUID.text == PlayerPrefs.GetString("Username"))
            {
                saveMessage.text = $"No changes have been made";
            }
        }
        
        
        
    }
    #endregion

    #region Leaderboard

    public void LeaderboardLoad()
    {
        playFab.GetLeaderboard();
        leaderboardPanel.enabled = true; 
        targetPosition.Set(posXIn, posYIn);
        SlidePanel(leaderboardPanel);
    }
    
    #endregion

    #region In-Game UI Functions

    public void PauseGame()
    {
        if (!pausePanel.enabled && !gameOverPanel.enabled)
        {
            pausePanel.enabled = true;
            targetPosition.Set(posXIn, posYIn);
            SlidePanel(pausePanel);
            Time.timeScale = 0.0f;
        }

        else if (pausePanel.enabled) ResumeGame();
    }

    public void ResumeGame()
    {
        targetPosition.Set(posXOut, posYOut);
        SlidePanel(pausePanel);
        Time.timeScale = 1.0f;
    }

    public void MainMenu() => SceneManager.LoadScene("0_MainMenu");

    public void GameOver()
    {
        targetPosition.Set(posXIn, posYIn);
        gameOverPanel.enabled = true;
        Time.timeScale = 0.0f; 
        SlidePanel(gameOverPanel);
    }

    #endregion

    public void QuitGame()
    {
        Application.Quit();
    }
}