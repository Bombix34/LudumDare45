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

    [Header("Réglages gravité")]
    [Range(0.1f, 30f)]
    public float gravityRange = 16f;
    [Range(-1000f, -1f)]
    public float gravityOnAshe = -150f;
    [Range(-1000f, -1f)]
    public float gravityOnPlanet = -500f;
    [Range(-1500f, -1f)]
    public float gravityOnStar = -1000f;
    [Range(-1500f, -1f)]
    public float gravityOnHoles = -2000f;

    [Header("Autres")]
    [Range(1f, 50f)]
    public float maxSizeHole = 1f;
    [Range(0.1f, 2f)]
    public float AddMassMultiplicator = 1f;

    [Range(0.01f, 2f)]
    public float AddSizeMultiplicator = 1f;

}
