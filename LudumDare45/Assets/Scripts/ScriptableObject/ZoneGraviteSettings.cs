using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZoneGraviteSettings : ScriptableObject
{


    /*[MinMaxRange(-1000f, 1000f)]
    public RangedFloat RangeOfGravityModification;*/

    [Header("Refraction")]
    [Range(0, 1000)]
    public float RefractionForceDust = 500;
    [Range(0, 1000)]
    public float RefractionForcePlanets = 500;
    [Range(0, 1000)]
    public float RefractionForceStars = 500;

    [Range(1, 500)]
    public float RefractionRange = 10;

    [Header("Attraction")]
    [Range(0, 1000)]
    public float AttractionForceDust = 500;
    [Range(0, 1000)]
    public float AttractionForcePlanets = 500;
    [Range(0, 1000)]
    public float AttractionForceStars = 500;


    [Range(1, 500)]
    public float AttractionRange = 10;





}
