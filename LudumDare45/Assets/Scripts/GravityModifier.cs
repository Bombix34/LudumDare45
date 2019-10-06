using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityModifier : MonoBehaviour
{

    public GravityModifierSettings settings;


    private PointEffector2D pointEffector;


    private void Awake()
    {
        pointEffector = GetComponent<PointEffector2D>();

    }


    public void AddForce()
    {
        if(pointEffector.forceMagnitude + settings.SpeedAddRemoveForce <= settings.RangeOfGravityModification.maxValue)
        {
            pointEffector.forceMagnitude += settings.SpeedAddRemoveForce;
        }
    }

    public void RemoveForce()
    {
        if (pointEffector.forceMagnitude - settings.SpeedAddRemoveForce >= settings.RangeOfGravityModification.minValue)
        {
            pointEffector.forceMagnitude -= settings.SpeedAddRemoveForce;
        }
    }




}
