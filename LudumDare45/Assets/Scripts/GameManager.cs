using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    [SerializeField] List<GameObject> Ashes;
    [SerializeField] List<GameObject> Planets;
    [SerializeField] List<GameObject> Trous;

    public Vector3 ScreenRange => screenRange;
    public Vector3 screenRange;

    void Awake()
    {
        Ashes = new List<GameObject>();
        Planets = new List<GameObject>();
        Trous = new List<GameObject>();
    }

    void Update()
    {
        /*
        Vector3 StartPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 EndPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        screenRange = EndPoint - StartPoint;
        */
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

    //SINGLETON________________________________________________________________________________________________

    private static GameManager s_Instance = null;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static GameManager Instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                Debug.Log("error");
                GameObject obj = new GameObject("Error");
                s_Instance = obj.AddComponent(typeof(GameManager)) as GameManager;
            }

            return s_Instance;
        }
    }
}
