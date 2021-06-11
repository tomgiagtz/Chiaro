using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretButton : MonoBehaviour
{
    // Start is called before the first frame update
    public TowerValues towerType;
    public TextMeshProUGUI priceLabel;
    public Color affordableColor, unaffordableColor;
    public Image displayImage;

    public Sprite selectedSprite;
    public Sprite unselectedSprite;
    Image backer;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        backer = GetComponent<Image>();
        button.onClick.AddListener(OnBlueprintSelected);
        priceLabel.SetText(towerType.cost.ToString());
        displayImage.sprite = towerType.sprite;
        priceLabel.color = unaffordableColor;
    }

    //hacky selection vars, should just be an event
    void Update()
    {
        

        if (!towerType.CanAfford()) {
            button.interactable = false;
            priceLabel.color = unaffordableColor;
        } else {
            button.interactable = true;
            priceLabel.color = affordableColor;
        }

        

        backer.sprite = towerType.IsSelected() ? selectedSprite : unselectedSprite;
    }

    void OnBlueprintSelected() {
        Debug.Log(towerType.IsSelected());
        ShopController.Instance.ChangeSelectedTower(towerType);
        Debug.Log(towerType.IsSelected());
    }
}
