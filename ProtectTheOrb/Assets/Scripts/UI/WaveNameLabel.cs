using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveNameLabel : MonoBehaviour
{
    TextMeshProUGUI label;

    private void Start() {
        label = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        if (WaveController.Instance?.currWave?.name != null){
            label.SetText(WaveController.Instance.currWave.name);
        } else {
            label.SetText("None");
        }
    }
}
