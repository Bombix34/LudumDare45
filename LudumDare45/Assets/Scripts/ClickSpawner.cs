using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSpawner : MonoBehaviour
{
    public GameObject particlePrefab;

    [SerializeField] SpawnSettings settings;
    private float chronoSpawn = 0f;

    private float chronoNoteSound=0f;
    public float maxChrono=0.2f;

    private void Start()
    {
        
    }

    void Update()
    {
        chronoSpawn -= Time.fixedDeltaTime;
        if(Input.GetMouseButton(0)&&chronoSpawn<=0)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,20f));
            GameManager.Instance.AddAshes(Instantiate(GameManager.Instance.AshePrefab, mousePosition, Quaternion.identity));
            chronoSpawn = settings.timeBetweenDustSpawn;
            if(chronoNoteSound<=0)
            {
                SoundManager.Instance.PlaySound(0);
                chronoNoteSound = maxChrono;
            }
            else
            {
                chronoNoteSound -= Time.fixedDeltaTime;
            }
        }


        // mouse wheel up
        // add attraction force on gravity modifier
        if(Input.mouseScrollDelta.y > 0)
        {
            GameObject gravityModifier = getGravityModifierOverMouse();
            if (gravityModifier == null)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20f));
                gravityModifier = ZoneGraviteLogic.createZoneRefraction(mousePosition);
            }
            //gravityModifier.GetComponent<GravityModifier>().AddForce();
        }

        // mouse wheel down
        // add refraction force on gravity modifier
        if(Input.mouseScrollDelta.y < 0)
        {
            GameObject gravityModifier = getGravityModifierOverMouse();
            if (gravityModifier == null)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20f));
                gravityModifier = ZoneGraviteLogic.createZoneAttraction(mousePosition);
            }
            //gravityModifier.GetComponent<GravityModifier>().RemoveForce();
        }

        // mouse middle button
        // gravity modifier destruction
        if (Input.GetMouseButtonDown(2))
        {

            GameObject gravityModifier = getGravityModifierOverMouse();
            if(gravityModifier != null)
            {
                ZoneGraviteLogic.destroyZone(gravityModifier);
            }

        }
    }

    private GameObject getGravityModifierOverMouse()
    {
        int layerMask = LayerMask.GetMask("GravityModifier");

        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, layerMask))
        {
            return hit.collider.gameObject.transform.parent.gameObject;
        }

        return null;
    }
}
