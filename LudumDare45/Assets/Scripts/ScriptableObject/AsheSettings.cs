using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "LD45/Ashes Settings")]
public class AsheSettings :ScriptableObject
{
    [Header("Réglage Spawn")]
    [Range(0.0001f, 0.2f)]
    public float minSizeOnSpawn = 0.01f;
    [Range(0.0001f, 0.2f)]
    public float maxSizeOnSpawn = 0.02f;

    [Header("Autres")]
    [Range(0.01f, 0.3f)]
    public float maxSizeAshes = 0.03f;
}
