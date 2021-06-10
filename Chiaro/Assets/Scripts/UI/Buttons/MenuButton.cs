using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {
    Button button;

    public bool hasKeyBoardInput = false;
    public KeyCode pauseKey = KeyCode.Escape;
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(Pause);
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(hasKeyBoardInput);
        if (Input.GetKeyDown(pauseKey) && hasKeyBoardInput) {
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
