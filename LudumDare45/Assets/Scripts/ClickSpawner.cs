using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSpawner : MonoBehaviour
{
    public GameObject particlePrefab;

    void Start()
    {
        
    }

    void Update()
    {
       // print(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,20f));
            GameManager.Instance.AddAshes(Instantiate(particlePrefab, mousePosition, Quaternion.identity));
        }
    }
}
