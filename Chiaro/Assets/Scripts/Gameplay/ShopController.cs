using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SelectionMode {
        Buy,
        Upgrade,
        Sell

    };
public class ShopController : MonoSingleton<ShopController>
{
    // Start is called before the first frame update
    public TowerValues selectedTower;


    


    public SelectionMode mode = SelectionMode.Buy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetModeBuy() {
        mode = SelectionMode.Buy;
    }

    void SetModeUpgrade() {
        mode = SelectionMode.Upgrade;
    }

    public void ToggleMode() {
        int numModes = SelectionMode.GetValues(typeof(SelectionMode)).Length;
        mode = (SelectionMode) (((int) mode == numModes -1) ? 0 : (int) mode + 1);
    }

    public void ChangeSelectedTower(TowerValues tower) {
        selectedTower = tower;
    }

    public bool AttemptBuy(GameObject selection) {
        Node node = selection.GetComponent<Node>();
        if (!node.towerGO && selectedTower.CanAfford()) {
            selectedTower.Buy();
            Instantiate(selectedTower.prefab, selection.transform.position + node.offset, selection.transform.rotation);
        }
        return false;

        
    }


}
