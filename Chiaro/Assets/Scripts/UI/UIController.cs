using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoSingleton<UIController> {

    [Header("Pause Menu")]
    public GameObject pauseMenu;
    UISlide pauseMenuSlide;

    private void Start() {
        pauseMenuSlide = pauseMenu.GetComponent<UISlide>();
    }

    public void ShowPauseMenu() {
        Debug.Log("ShowPauseMenu");
        GameController.Instance.isGamePaused = true;
        Time.timeScale = 0;
        pauseMenuSlide.ShowElement();
    
        
    }

    public void HidePauseMenu() {
        Debug.Log("hide");
        GameController.Instance.isGamePaused = false;
        Time.timeScale = 1;
        pauseMenuSlide.HideElement();
    }
 }
