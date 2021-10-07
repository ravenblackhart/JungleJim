using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UIManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private Canvas homeScreen;
    [SerializeField] private Canvas gameScreen;

    [Header("Main Menu - Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button closePanelButton;
    [SerializeField] private Button quitButton;
    
    [Header("Main Menu - Canvases")]
    [SerializeField] private Canvas settingsPanel;
    [SerializeField] private Canvas creditsPanel;
    [SerializeField] private Canvas leaderboardPanel;

    [Header("In-game UI - HUD")] 
    [SerializeField] private Canvas HUD;
    [SerializeField] private TextMeshProUGUI DistanceText;
    
    [Header("In-game UI - Buttons")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    
    [Header("In-game UI - Canvases")]
    [SerializeField] private Canvas pausePanel;
    [SerializeField] private Canvas gameOverPanel;
    [SerializeField] private TextMeshProUGUI FinalScoreText;

    #endregion

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "0_MainMenu")
        {
            settingsPanel.enabled = false;
            creditsPanel.enabled = false;
            leaderboardPanel.enabled = false;
            gameScreen.enabled = false;
        }
        
        else if (SceneManager.GetActiveScene().name == "1_GameLevel")
        {
            gameScreen.enabled = true;
            homeScreen.enabled = false; 
        }
    }

    private void Start()
    {
       
    }

    #region Main Menu UI Functions
    
    public void StartLevel() => SceneManager.LoadScene("1_GameLevel"); //Use this script for playButton & restartButton
    public void OpenSettings() => settingsPanel.enabled = true;
    public void OpenLeader() => leaderboardPanel.enabled = true;

    public void OpenCredits()
    {
        settingsPanel.enabled = false;
        creditsPanel.enabled = true;
    }

    public void ClosePanel() => this.transform.parent.gameObject.SetActive(false); 

    #endregion
    
    #region In-Game UI Functions
    
    public void PauseGame()
    {
        if (!pausePanel.enabled && !gameOverPanel.enabled)
        {
            pausePanel.enabled = true;
            Time.timeScale = 0.0f;
        }

        else if (pausePanel.enabled) ResumeGame();
       
    }
    
    public void ResumeGame()
    {
        pausePanel.enabled = false;
        Time.timeScale = 1.0f;
    }
    
   public void MainMenu() => SceneManager.LoadScene("0_MainMenu");

   public void GameOver()
   {
       
       gameOverPanel.enabled = true; 
       
   }

    #endregion
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
}