using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour {
    Button button;
    public KeyCode pauseKey = KeyCode.Escape;
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(Pause);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(pauseKey)) {
            Pause();
        }
    }

    void Pause() {
        if (GameController.Instance.isGamePaused) {
            UIController.Instance.HidePauseMenu();
        } else {
            UIController.Instance.ShowPauseMenu();
        }
    }
}
