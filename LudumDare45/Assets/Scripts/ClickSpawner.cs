using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickSpawner : MonoBehaviour
{
    public GameObject particlePrefab;
    [SerializeField] Image tuto;

    [SerializeField] SpawnSettings settings;
    private float chronoSpawn = 0f;

    private float chronoNoteSound=0f;
    public float maxChrono=0.2f;

    private UTimer timerSpwanTuto;
    public float tempsAvantSpawnTuto = 5f;
    public float speedFade = 1f;
    private bool flagFadeIn = false;
    private Coroutine FadeInCoroutine;

    private void Start()
    {
        timerSpwanTuto = UTimer.Initialize(tempsAvantSpawnTuto, this, startFadeInTuto);
        tuto.color = new Color(tuto.color.r, tuto.color.g, tuto.color.b, 0);
        timerSpwanTuto.start();
    }

    void Update()
    {
        chronoSpawn -= Time.fixedDeltaTime;
        if(Input.GetMouseButton(0)&&chronoSpawn<=0)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,20f));
            for(int i=0; i < settings.instancePerClick;i++)
            {
                GameManager.Instance.AddAshes(Instantiate(GameManager.Instance.AshePrefab, mousePosition, Quaternion.identity));
            }
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
            startFadeOutTuto();
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
            startFadeOutTuto();
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

    private void startFadeInTuto()
    {
        FadeInCoroutine = StartCoroutine(FadeIn());
    }

    private void startFadeOutTuto()
    {
        timerSpwanTuto.Stop();
        if (flagFadeIn)
        {
            StopCoroutine(FadeInCoroutine);
        }
        StartCoroutine(FadeOut());
    }
    

    private IEnumerator FadeIn()
    {
        flagFadeIn = true;
        float alpha = 0;

        while(alpha < 1)
        {
            alpha += Time.deltaTime * speedFade;
            tuto.color = new Color(tuto.color.r, tuto.color.g, tuto.color.b, alpha);
            yield return null;
        }

        flagFadeIn = false;
    }

    private IEnumerator FadeOut()
    {
        float alpha = tuto.color.a;
        while(alpha > 0)
        {
            alpha -= Time.deltaTime * speedFade;
            tuto.color = new Color(tuto.color.r, tuto.color.g, tuto.color.b, alpha);
            yield return null;
        }
    }
}
