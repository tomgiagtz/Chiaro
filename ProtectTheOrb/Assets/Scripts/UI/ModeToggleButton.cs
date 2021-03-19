using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModeToggleButton : MonoBehaviour
{
    TextMeshProUGUI label;

    private void Start() {
        label = GetComponent<TextMeshProUGUI>();
        label.SetText(ShopController.Instance.mode.ToString());
    }
    private void Update() {
        label.SetText(ShopController.Instance.mode.ToString());
    }


}
