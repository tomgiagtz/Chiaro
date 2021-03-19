using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer mesh;
    public Color highlightColor;
    public Vector3 offset = new Vector3 (0f, 0.25f, 0f);
    public GameObject towerGO;
    Color defaultColor;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        defaultColor = mesh.materials[1].color;
        Physics.queriesHitTriggers = false;
    }

    private void OnMouseEnter() {
        
        mesh.materials[1].color = highlightColor;
    }

    private void OnMouseExit() {
        mesh.materials[1].color = defaultColor;
    }

    private void OnMouseDown() {
        if (towerGO != null) {
            Debug.Log("hallo");
        }
    }
}
