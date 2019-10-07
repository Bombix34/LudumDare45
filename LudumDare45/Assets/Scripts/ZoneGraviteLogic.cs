using System.Collections;
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
            SoundManager.Instance.PlaySound(4);
            Destroy(attractionZone);
        }
        SoundManager.Instance.PlaySound(3);
        go = Instantiate(GameManager.Instance.ZoneAttrationPrefab, position, Quaternion.Euler(-90, 0, 0));

        PointEffector2D[] listPointEmitter = go.GetComponentsInChildren<PointEffector2D>();
        listPointEmitter[0].forceMagnitude = -go.GetComponent<ZoneGraviteLogic>().settings.AttractionForceDust;
        listPointEmitter[1].forceMagnitude = -go.GetComponent<ZoneGraviteLogic>().settings.AttractionForcePlanets;
        listPointEmitter[2].forceMagnitude = -go.GetComponent<ZoneGraviteLogic>().settings.AttractionForceStars;

        foreach(CircleCollider2D circleCollider in go.GetComponentsInChildren<CircleCollider2D>())
        {
            circleCollider.radius = go.GetComponent<ZoneGraviteLogic>().settings.AttractionRange;
        }
        attractionZone = go;

        return go;
    }

    static public GameObject createZoneRefraction(Vector3 position)
    {
        GameObject go = null;

        if (refractionZone != null)
        {
            SoundManager.Instance.PlaySound(2);
            Destroy(refractionZone);
        }
        SoundManager.Instance.PlaySound(1);
        go = Instantiate(GameManager.Instance.ZoneRefractionPrefab, position, Quaternion.Euler(-90, 0, 0));
        PointEffector2D[] listPointEmitter = go.GetComponentsInChildren<PointEffector2D>();
        listPointEmitter[0].forceMagnitude = go.GetComponent<ZoneGraviteLogic>().settings.RefractionForceDust;
        listPointEmitter[1].forceMagnitude = go.GetComponent<ZoneGraviteLogic>().settings.RefractionForcePlanets;
        listPointEmitter[2].forceMagnitude = go.GetComponent<ZoneGraviteLogic>().settings.RefractionForceStars;

        foreach (CircleCollider2D circleCollider in go.GetComponentsInChildren<CircleCollider2D>())
        {
            circleCollider.radius = go.GetComponent<ZoneGraviteLogic>().settings.RefractionRange;
        }
        refractionZone = go;

        return go;
    }

    static public void destroyZone(GameObject zone)
    {
        if(zone == refractionZone)
        {
            SoundManager.Instance.PlaySound(2);
            Destroy(refractionZone);
        }

        if(zone == attractionZone)
        {
            SoundManager.Instance.PlaySound(4);
            Destroy(attractionZone);
        }

    }


}
