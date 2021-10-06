using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UIManager : MonoBehaviour
{
    #region Inspector

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
    [SerializeField] private TextMeshProUGUI distance;
    
    [Header("In-game UI - Buttons")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    
    [Header("In-game UI - Canvases")]
    [SerializeField] private Canvas pausePanel;
    [SerializeField] private Canvas gameOverPanel;

    #endregion

    #region Main Menu UI Functions
    
    void StartLevel() => SceneManager.LoadScene("1_GameLevel"); //Use this script for playButton & restartButton

    #endregion
    
    #region In-Game UI Functions
    
    void PauseGame()
    {
        if (!pausePanel.enabled && !gameOverPanel.enabled)
        {
            pausePanel.enabled = true;
            Time.timeScale = 0.0f;
        }

        else if (pausePanel.enabled) ResumeGame();
       
    }
    
    void ResumeGame()
    {
        pausePanel.enabled = false;
        Time.timeScale = 1.0f;
    }
    
    void MainMenu() => SceneManager.LoadScene("0_MainMenu");

    #endregion
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
}