using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "LD45/Ashes Settings")]
public class AsheSettings :ScriptableObject
{
    [SerializeField] List<Material> asheMaterials;
    public Material GetRandomMaterial()
    {
        return asheMaterials[(int)Random.Range(0f, asheMaterials.Count)];
    }

    [Header("Réglages Spawn")]
    [MinMaxRange(0.0001f, 0.2f)]
    public RangedFloat SizeOnSpawn;
    [MinMaxRange(0.1f, 10f)]
    public RangedFloat MassOnSpawn;

    [MinMaxRange(0.1f, 10f)]
    public RangedFloat SpeedOnSpawn;


    [Header("Réglages Passage en planète")]
    [Range(0f,100f)]
    public float chanceToTransform = 50f;
    [Range(1f, 100f)]
    public float massToTransform = 15f;

    [Header("Autres")]
    [Range(0.01f, 0.3f)]
    public float maxSizeAshes = 0.03f;


}
