using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{ 
    [SerializeField] List<GameObject> Ashes;
    [SerializeField] List<GameObject> Planets;
    [SerializeField] List<GameObject> Trous;

    void Awake()
    {
        Ashes = new List<GameObject>();
        Planets = new List<GameObject>();
        Trous = new List<GameObject>();
    }

    public void AddAshes(GameObject ash)
    {
        Ashes.Add(ash);
        DebugUI.Instance.UpdateAshes(Ashes.Count);
    }
    
}
