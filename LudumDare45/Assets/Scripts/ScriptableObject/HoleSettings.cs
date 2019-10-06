using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//[CreateAssetMenu(menuName = "LD45/Hole Settings")]
public class HoleSettings : ScriptableObject
{
    [Header("Réglages Spawn")]
    [MinMaxRange(0.1f, 6f)]
    public RangedFloat SizeOnSpawn;
    [MinMaxRange(1f, 10000f)]
    public RangedFloat MassOnSpawn;

    [MinMaxRange(0f, 10f)]
    public RangedFloat SpeedOnSpawn;

    [Header("Autres")]
    [Range(1f, 50f)]
    public float maxSizeHole = 1f;

}
