using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSpawner : MonoBehaviour
{
    public GameObject particlePrefab;

    [SerializeField] SpawnSettings settings;
    private float chronoSpawn = 0f;


    void Update()
    {
        chronoSpawn -= Time.fixedDeltaTime;
        if(Input.GetMouseButton(0)&&chronoSpawn<=0)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,20f));
            GameManager.Instance.AddAshes(Instantiate(GameManager.Instance.AshePrefab, mousePosition, Quaternion.identity));
            chronoSpawn = settings.timeBetweenDustSpawn;
        }
    }
}
