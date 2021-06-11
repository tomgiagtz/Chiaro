using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer mesh;
    public Color highlightColor;
    public Vector3 offset = new Vector3 (0f, 0.5f, 0f);
    public GameObject towerGO;
    Color defaultColor;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        defaultColor = mesh.materials[1].color;
        Physics.queriesHitTriggers = false;
    }

    private void OnMouseEnter() {
        if (!GameController.Instance.isGamePaused)
            mesh.materials[1].color = highlightColor;
    }

    private void OnMouseExit() {   
        mesh.materials[1].color = defaultColor;
    }

    private void OnMouseDown() {
        if (GameController.Instance.isGamePaused)
            return;
        TowerValues selectedTowerValues = ShopController.Instance.selectedTower;

       

        
        switch (ShopController.Instance.mode) {
            case SelectionMode.Buy: 
                 ShopController.Instance.AttemptBuy(gameObject);
                break;
            case SelectionMode.Upgrade: 
                Destroy(towerGO);
                // towerGO = Instantiate(selectedTowerValues.upgrade, transform.position + offset, transform.rotation);
                break;
            case SelectionMode.Sell: 
                GameController.Instance.AddCoins(selectedTowerValues.sellPrice);
                Destroy(towerGO);
                break;

            default: {
                return;
            }

        }

    }


}
