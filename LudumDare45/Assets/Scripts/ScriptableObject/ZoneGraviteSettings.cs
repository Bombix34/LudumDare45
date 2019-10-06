using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZoneGraviteSettings : ScriptableObject
{


    /*[MinMaxRange(-1000f, 1000f)]
    public RangedFloat RangeOfGravityModification;*/

    [Header("Refraction")]
    [Range(0, 1000)]
    public float RefractionForce = 500;
    [Range(1, 500)]
    public float RefractionRange = 10;

    [Header("Attraction")]
    [Range(0, 1000)]
    public float AttractionForce = 500;

    [Range(1, 500)]
    public float AttractionRange = 10;





}
