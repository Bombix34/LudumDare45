using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityModifierSettings : ScriptableObject
{


    [MinMaxRange(-1000f, 1000f)]
    public RangedFloat RangeOfGravityModification;

    [Range(0, 100)]
    public float SpeedAddRemoveForce = 20;



}
