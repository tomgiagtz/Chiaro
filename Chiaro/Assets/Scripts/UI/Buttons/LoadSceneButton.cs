using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    Button button;
    public string sceneName;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadGameScene);
    }

    // Update is called once per frame
    void LoadGameScene()
    {
        Debug.Log("ahwa");
        SceneManager.LoadScene(sceneName);
    }
}
