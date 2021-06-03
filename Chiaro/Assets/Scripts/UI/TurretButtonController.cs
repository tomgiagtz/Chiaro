using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public TowerValues towerType;
    public TextMeshProUGUI priceLabel;
    public Image displayImage;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        priceLabel.SetText(towerType.cost.ToString());
        displayImage.sprite = towerType.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.CoinCount() < towerType.cost) {
            button.interactable = false;
        } else {
            button.interactable = true;
        }
    }
}
