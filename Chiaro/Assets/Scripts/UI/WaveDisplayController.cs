using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveDisplayController : MonoSingleton<WaveDisplayController>
{
    [SerializeField]
    List<GameObject> waveUiElements = new List<GameObject>();
    public List<Wave> waves;

    public Scrollbar waveScroll;
    WaveController waveController;
    void Start()
    {
        waveController = GetComponent<WaveController>();
        foreach( Wave wave in waves ) {
            Debug.Log(wave.name);
            //create waveUIelements for each wave
        }
        

        waveScroll.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        float waveProgress = waveController.waveProgress;
        Debug.Log(waveController.waveProgress);
        UpdateScrollPosition(waveProgress);
        if (waveProgress == 1f) {
            // Debug.Log("Delete curr and load next");
            
        } else {

            
            //wave scroll value at zero when currwave progress == 0
            //wave scroll value at 0.5 when currwave progess == 1
        }
    }

    public void NextWave() {

    }
    float currScrollValue, targetScrollValue, prevScrollValue;

    void UpdateScrollPosition(float waveProgress) {
        Debug.Log("source: " + waveProgress);
        Debug.Log("target: " + targetScrollValue);
        Debug.Log("curr: " + currScrollValue);


        if (targetScrollValue == waveProgress) {
            //currently scrolling to target

            currScrollValue = Mathf.Lerp(currScrollValue, targetScrollValue, Time.deltaTime * 2);
        } else if (targetScrollValue < waveProgress) {
            //waveProgress was updated
            targetScrollValue = waveProgress;
        } else {
            //next wave began
        }

        waveScroll.value = currScrollValue / 2f;
    }
}
