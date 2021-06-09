using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoSingleton<UIController> {
    public GameObject pauseMenu;

    private void Start() {
        pauseMenu.SetActive(false);
    }

    public void ShowPauseMenu() {
        Debug.Log("ShowPauseMenu");
        GameController.Instance.isGamePaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    
        
    }

    public void HidePauseMenu() {
        Debug.Log("hide");
        GameController.Instance.isGamePaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
 }
