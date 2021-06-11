using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerValues", menuName="Values/TowerValues")]
public class TowerValues : ScriptableObject
{
    public int cost;
    public int sellPrice;
    public float range;
    public float damage;
    //shots per second
    public float rateOfFire;
    public GameObject prefab;
    // public TowerValues upgrade;
    public GameObject upgrade;

    public Sprite sprite;

    public bool CanAfford() {
        return cost > GameController.Instance.CoinCount();
    }

    public void Buy() {
        GameController.Instance.RemoveCoins(cost);
    }

    public void Sell() {
        GameController.Instance.AddCoins(sellPrice);
    }
}


