using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveList", menuName="Values/WaveList")]
public class WaveList : ScriptableObject
{
    public List<Wave> waves = new List<Wave>();
}
