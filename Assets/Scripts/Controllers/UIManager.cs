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
    
    void Awake()
    {
        Time.timeScale = 1.0f;
        
        if (SceneManager.GetActiveScene().name == "0_MainMenu")
        {
            homeScreen.enabled = true;
            settingsPanel.enabled = false;
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

    public void StartLevel() => SceneManager.LoadScene("1_GameLevel"); //Use this script for playButton & restartButton
    
    public void OpenPanel(Canvas panel)
    {
        panel.enabled = true;
        targetPosition.Set(posXIn, posYIn);
        SlidePanel(panel);
    }

    public void ClosePanel(Canvas panel)
    {
        targetPosition.Set(posXOut, posYOut);

        SlidePanel(panel);

    }

    void SlidePanel(Canvas targetPanel)
    {
        animTarget = targetPanel.GetComponent<RectTransform>();
        animatePanel = true;
    }

    #endregion
    
    #region Leaderboard 
    
    
    
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