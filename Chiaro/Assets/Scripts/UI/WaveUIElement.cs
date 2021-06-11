using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveUIElement : MonoBehaviour
{

    //set in inspector
    public TextMeshProUGUI waveLabel;
    public Image waveBacker;

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //should be called whenever gameobject is made
    public void InitWaveUI(Wave waveData){
        Debug.Log("HEre" + waveData.GetWaveUIColor());
        waveLabel.SetText(waveData.waveName);
        waveBacker.color = waveData.GetWaveUIColor();
    }
}
