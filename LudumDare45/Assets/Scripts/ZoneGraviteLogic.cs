﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGraviteLogic : MonoBehaviour
{

    public ZoneGraviteSettings settings;


    static private GameObject attractionZone;
    static private GameObject refractionZone;
    
    private PointEffector2D pointEffector;


    private void Awake()
    {
        pointEffector = GetComponent<PointEffector2D>();

    }

    static public GameObject createZoneAttraction(Vector3 position)
    {
        GameObject go = null;

        if(attractionZone != null)
        {
            Destroy(attractionZone);
        }

        go = Instantiate(GameManager.Instance.ZoneAttrationPrefab, position, Quaternion.Euler(-90, 0, 0));
        go.GetComponent<PointEffector2D>().forceMagnitude = -go.GetComponent<ZoneGraviteLogic>().settings.AttractionForce;
        go.GetComponent<CircleCollider2D>().radius = go.GetComponent<ZoneGraviteLogic>().settings.AttractionRange;
        attractionZone = go;

        return go;
    }

    static public GameObject createZoneRefraction(Vector3 position)
    {
        GameObject go = null;

        if (refractionZone != null)
        {
            Destroy(refractionZone);
        }

        go = Instantiate(GameManager.Instance.ZoneRefractionPrefab, position, Quaternion.Euler(-90, 0, 0));
        go.GetComponent<PointEffector2D>().forceMagnitude = go.GetComponent<ZoneGraviteLogic>().settings.RefractionForce;
        go.GetComponent<CircleCollider2D>().radius = go.GetComponent<ZoneGraviteLogic>().settings.RefractionRange;
        refractionZone = go;

        return go;
    }

    static public void destroyZone(GameObject zone)
    {
        if(zone == refractionZone)
        {
            Destroy(refractionZone);
        }

        if(zone == attractionZone)
        {
            Destroy(attractionZone);
        }

    }

    /*public void AddForce()
    {
        if(pointEffector.forceMagnitude + settings.Force <= settings.RangeOfGravityModification.maxValue)
        {
            pointEffector.forceMagnitude += settings.Force;
        }
    }

    public void RemoveForce()
    {
        if (pointEffector.forceMagnitude - settings.Force >= settings.RangeOfGravityModification.minValue)
        {
            pointEffector.forceMagnitude -= settings.Force;
        }
    }*/




}