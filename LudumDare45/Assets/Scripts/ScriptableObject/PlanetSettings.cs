using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(menuName = "LD45/Planet Settings")]
public class PlanetSettings : ScriptableObject
{
    [SerializeField] List<Material> planetMaterials;
    public Material GetRandomMaterial()
    {
        return planetMaterials[(int)Random.Range(0f, planetMaterials.Count)];
    }

    [Header("Réglage Spawn")]
    [MinMaxRange(0.0001f, 0.2f)]
    public RangedFloat SizeOnSpawn;
    [MinMaxRange(1f, 1000f)]
    public RangedFloat MassOnSpawn;

    [MinMaxRange(0.1f, 10f)]
    public RangedFloat SpeedOnSpawn;

    [Header("Réglages Passage en étoile")]
    [Range(0f, 100f)]
    public float chanceToTransform = 50f;
    [Range(1f, 1000f)]
    public float massToTransform = 15f;


    [Header("Autres")]
    [Range(0.01f, 1f)]
    public float maxSizePlanet = 0.3f;
}
