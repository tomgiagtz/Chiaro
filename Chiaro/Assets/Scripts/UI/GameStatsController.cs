using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatsController : MonoBehaviour
{
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI coinText;

    // Update is called once per frame
    void FixedUpdate()
    {
        lifeText.SetText(GameController.Instance.LifeCount().ToString());
        coinText.SetText(GameController.Instance.CoinCount().ToString());

    }
}
