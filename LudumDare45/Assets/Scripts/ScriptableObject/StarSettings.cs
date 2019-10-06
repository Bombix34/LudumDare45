﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(menuName = "LD45/Star Settings")]
public class StarSettings : ScriptableObject
{
    [SerializeField] List<Material> starMaterials;
    public Material GetRandomMaterial()
    {
        return starMaterials[(int)Random.Range(0f, starMaterials.Count)];
    }

    [Header("Réglages Spawn")]
    [MinMaxRange(0.0001f, 1f)]
    public RangedFloat SizeOnSpawn;
    [MinMaxRange(1f, 1000f)]
    public RangedFloat MassOnSpawn;

    [MinMaxRange(0.1f, 10f)]
    public RangedFloat SpeedOnSpawn;


    [Header("Réglages Passage en trou noir")]
    [Range(0f, 100f)]
    public float chanceToTransform = 50f;
    [Range(10f, 1000f)]
    public float massToTransform = 15f;

    [Header("Autres")]
    [Range(0.01f, 5f)]
    public float maxSizeStar = 1f;

    [MinMaxRange(1f, 100f)]
    public RangedFloat lightIntensity;


}