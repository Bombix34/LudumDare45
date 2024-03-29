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
    [MinMaxRange(1f, 10000f)]
    public RangedFloat MassOnSpawn;

    [MinMaxRange(0.1f, 10f)]
    public RangedFloat SpeedOnSpawn;


    [Header("Réglages transition type d'étoile")]
    [Range(0.1f, 5f)]
    public float sizeToBeMediumStar;
    [Range(0.1f, 5f)]
    public float sizeToBeBigRedStar;
    [Range(150f, 10000f)]
    public float massMax;


    [Header("Réglages Passage en trou noir")]
    [Range(0f, 100f)]
    public float chanceToTransform = 50f;
    [Range(10f, 10000f)]
    public float massToTransform = 15f;

    [Header("Réglages gravité")]
    [Range(1f, 100f)]
    public float gravityRange = 16f;
    [Range(-10000f, -1f)]
    public  float gravityOnAshe = -40f;
    [Range(-10000f, -1f)]
    public  float gravityOnPlanet = -300f;
    [Range(-15000f, -1f)]
    public float gravityOnStar = -750f;

    [Header("Explosion")]
    [Range(0,1000)]
    public int ashesNumber;
    [Range(0f,1000f)]
    public float ashesSpeed;

    [Header("Autres")]
    [Range(0.01f, 5f)]
    public  float maxSizeStar = 1f;

    [Range(0.1f, 2f)]
    public float AddMassMultiplicator = 1f;

    [MinMaxRange(1f, 100f)]
    public RangedFloat lightIntensity;


    [Range(0.01f, 2f)]
    public float AddSizeMultiplicator = 1f;

    [Range(0, 100)]
    public int DustsCreatedOnCollidingWithPlanet = 10;
    [Range(0, 300)]
    public int DustsCreatedOnCollidingWithStar = 10;



}
