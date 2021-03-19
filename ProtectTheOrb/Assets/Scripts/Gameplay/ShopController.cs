using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SelectionMode {
        Buy,
        Upgrade,
        Destroy

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


}
