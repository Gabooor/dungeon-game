using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isGamePaused;
    public GameObject pauseMenu;

    void Start(){
        isGamePaused = false;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!isGamePaused) PauseGame();
            else ResumeGame();
        }
    }

    public void PauseGame(){
        pauseMenu.SetActive(true);
        isGamePaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame(){
        pauseMenu.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1;
    }

    public void ExitToMenu(){
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void ExitToDesktop(){
        Application.Quit();
    }
}
