using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "LD45/Ashes Settings")]
public class AsheSettings :ScriptableObject
{
    [Header("Réglage Spawn")]
    [MinMaxRange(0.0001f, 0.2f)]
    public RangedFloat SizeOnSpawn;
    [MinMaxRange(0.1f, 10f)]
    public RangedFloat MassOnSpawn;

    [MinMaxRange(0.1f, 10f)]
    public RangedFloat SpeedOnSpawn;


    [Header("Autres")]
    [Range(0.01f, 0.3f)]
    public float maxSizeAshes = 0.03f;
}
