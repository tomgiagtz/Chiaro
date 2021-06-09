using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WaveController))]
public class WaveDisplayController : MonoSingleton<WaveDisplayController>
{

    //set in inspector
    public GameObject waveUIPrefab;
    public Scrollbar waveScroll;
    public Transform waveDisplayContainer;


    [SerializeField]
    List<GameObject> waveUIElements = new List<GameObject>();
    List<Wave> waves;

    
    WaveController waveController;
    void Start()
    {
        waveController = GetComponent<WaveController>();
        waves = new List<Wave>(waveController.waveListData.waves);

        InitWaveData();
        

        waveScroll.value = 0f;
    }

    void InitWaveData() {
        //could be optimized by only loading enough wave ui gos for as many fit in scroll view (8 max at a time)
        foreach( Wave wave in waves ) {
            Debug.Log(wave.name);
            //create new prefab
            GameObject newGo = Instantiate(waveUIPrefab, waveDisplayContainer);
            // initialize new prefab with wave data singleton
            newGo.GetComponent<WaveUIElement>().InitWaveUI(wave);
            waveUIElements.Add(newGo);
        }
    }

    // Update is called once per frame
    void Update()
    {       
        float waveProgress = waveController.waveProgress;
        UpdateScrollPosition(waveProgress);
    }

    float currScrollValue, targetScrollValue, prevScrollValue;

    void UpdateScrollPosition(float waveProgress) {

        if (waveUIElements.Count == 0 || !waveController.currWave) {
            //dont attempt to update if there are no wave ui elements
            return;
        }

        //wave scroll value at zero when currwave progress == 0
        //wave scroll value at 0.5 when currwave progess == 1


        // Debug.Log("source: " + waveProgress);
        // Debug.Log("target: " + targetScrollValue);
        // Debug.Log("curr: " + currScrollValue);


        if (targetScrollValue == waveProgress) {
            //currently scrolling to target
            currScrollValue = Mathf.Lerp(currScrollValue, targetScrollValue, Time.deltaTime * waveController.currWave.spawnRate);
        } else if (targetScrollValue < waveProgress) {
            //waveProgress was updated
            targetScrollValue = waveProgress;
        } 

        waveScroll.value = currScrollValue / 2f;
    }


    //when wave manager reaches end of wave, it triggers this function
    public void OnWaveComplete() {
        waves.RemoveAt(0);
        GameObject.Destroy(waveUIElements[0]);
        waveScroll.value = 0f;
        currScrollValue = 0f;
        targetScrollValue = 0f;
        waveUIElements.RemoveAt(0);
    }
}
