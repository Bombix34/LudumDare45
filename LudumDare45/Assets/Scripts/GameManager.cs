using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{ 
    [SerializeField] List<GameObject> Ashes;
    [SerializeField] List<GameObject> Planets;
    [SerializeField] List<GameObject> Trous;

    public GameObject AshePrefab => ashePrefab;
    [SerializeField] GameObject ashePrefab;
    public GameObject PlanetPrefab => planetPrefab;
    [SerializeField] GameObject planetPrefab;

    public Vector3 ScreenRange => screenRange;
    Vector3 screenRange;

    void Awake()
    {
        Ashes = new List<GameObject>();
        Planets = new List<GameObject>();
        Trous = new List<GameObject>();

        Vector3 StartPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 20f));
        Vector3 EndPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 20f));
        screenRange = EndPoint - StartPoint;
    }

    public void AddPlanet(GameObject planet)
    {
        Planets.Add(planet);
        DebugUI.Instance.UpdatePlanets(Planets.Count);
    }

    public void RemovePlanet(GameObject toRm)
    {
        Planets.Remove(toRm);
        Destroy(toRm);
        DebugUI.Instance.UpdatePlanets(Planets.Count);
    }

    public void AddAshes(GameObject ash)
    {
        Ashes.Add(ash);
        DebugUI.Instance.UpdateAshes(Ashes.Count);
    }

    public void RemoveAshe(GameObject toRm)
    {
        Ashes.Remove(toRm);
        Destroy(toRm);
        DebugUI.Instance.UpdateAshes(Ashes.Count);
    }

    public GameObject GetPlanetPrefab()
    {
        return PlanetPrefab;
    }

}
