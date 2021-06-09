using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveUIElement : MonoBehaviour
{
    Wave waveData;
    TextMeshProUGUI waveLabel;
    Image waveBacker;

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //should be called whenever gameobject is made
    void Init(){
        waveLabel.SetText(waveData.name);
        waveBacker.color = waveData.GetWaveUIColor();
    }
}
