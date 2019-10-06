﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{ 
    [SerializeField] List<GameObject> Ashes;
    [SerializeField] List<GameObject> Planets;
    [SerializeField] List<GameObject> Stars;
    [SerializeField] List<GameObject> Holes;

    public GameObject AshePrefab => ashePrefab;
    [SerializeField] GameObject ashePrefab;
    public GameObject PlanetPrefab => planetPrefab;
    [SerializeField] GameObject planetPrefab;

    public GameObject StarPrefab => starPrefab;
    [SerializeField] GameObject starPrefab;

    public GameObject HolePrefab => holePrefab;
    [SerializeField] GameObject holePrefab;

    public GameObject ZoneAttrationPrefab => zoneAttrationPrefab;
    [SerializeField] GameObject zoneAttrationPrefab;
    public GameObject ZoneRefractionPrefab => zoneRefractionPrefab;
    [SerializeField] GameObject zoneRefractionPrefab;

    public Vector3 ScreenRange => screenRange;
    Vector3 screenRange;

    void Awake()
    {
        Ashes = new List<GameObject>();
        Planets = new List<GameObject>();
        Stars = new List<GameObject>();
        Holes = new List<GameObject>();

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

    public void AddStar(GameObject star)
    {
        Stars.Add(star);
        DebugUI.Instance.UpdateStars(Stars.Count);
    }
    public void RemoveStar(GameObject toRm)
    {
        Stars.Remove(toRm);
        Destroy(toRm);
        DebugUI.Instance.UpdateStars(Stars.Count);
    }

    public void AddHole(GameObject hole)
    {
        Holes.Add(hole);
        DebugUI.Instance.UpdateHoles(Holes.Count);
    }
    public void RemoveHole(GameObject toRm)
    {
        Holes.Remove(toRm);
        Destroy(toRm);
        DebugUI.Instance.UpdateHoles(Holes.Count);
    }

    public GameObject GetPlanetPrefab()
    {
        return PlanetPrefab;
    }

}
