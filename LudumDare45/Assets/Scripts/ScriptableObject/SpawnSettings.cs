using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "LD45/Spawn Settings")]
public class SpawnSettings : ScriptableObject
{
    [Range(0f, 1f)]
    public float timeBetweenDustSpawn = 0.1f;

}
