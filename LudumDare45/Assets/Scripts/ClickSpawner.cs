using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickSpawner : MonoBehaviour
{
    public GameObject particlePrefab;
    [SerializeField] Image tutoClick;
    [SerializeField] Image tutoMolette;

    [SerializeField] SpawnSettings settings;
    private float chronoSpawn = 0f;

    private float chronoNoteSound=0f;
    public float maxChrono=0.2f;

    private UTimer timerSpwanMoletteTuto;
    private UTimer timerSpwanClickTuto;
    public float tempsAvantSpawnMoletteTuto = 20f;
    public float tempsAvantSpawnClickTuto = 5f;
    public float speedFade = 1f;
    private bool flagFadeInMolette = false;
    private bool flagFadeInClick = false;
    private Coroutine FadeInMoletteCoroutine;
    private Coroutine FadeInClickCoroutine;

    private void Start()
    {
        tutoMolette.color = new Color(tutoMolette.color.r, tutoMolette.color.g, tutoMolette.color.b, 0);
        tutoClick.color = new Color(tutoClick.color.r, tutoClick.color.g, tutoClick.color.b, 0);
        timerSpwanMoletteTuto = UTimer.Initialize(tempsAvantSpawnMoletteTuto, this, startFadeInMoletteTuto);
        timerSpwanMoletteTuto.start();
        timerSpwanClickTuto = UTimer.Initialize(tempsAvantSpawnClickTuto, this, startFadeInClickTuto);
        timerSpwanClickTuto.start();
    }

    void Update()
    {
        chronoSpawn -= Time.fixedDeltaTime;
        if(Input.GetMouseButton(0)&&chronoSpawn<=0)
        {
            startFadeOutClickTuto();

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
            startFadeOutMoletteTuto();
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
            startFadeOutMoletteTuto();
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

    private void startFadeInMoletteTuto()
    {
        FadeInMoletteCoroutine = StartCoroutine(FadeInMolette());
    }

    private void startFadeOutMoletteTuto()
    {
        timerSpwanMoletteTuto.Stop();
        if (flagFadeInMolette)
        {
            StopCoroutine(FadeInMoletteCoroutine);
        }
        StartCoroutine(FadeOutMolette());
    }

    private void startFadeInClickTuto()
    {
        FadeInClickCoroutine = StartCoroutine(FadeInClick());
    }

    private void startFadeOutClickTuto()
    {
        timerSpwanClickTuto.Stop();
        if (flagFadeInClick)
        {
            StopCoroutine(FadeInClickCoroutine);
            StartCoroutine(FadeOutClick());
        }
        
    }



    private IEnumerator FadeInMolette()
    {
        flagFadeInMolette = true;
        float alpha = 0;

        while(alpha < 1)
        {
            alpha += Time.deltaTime * speedFade;
            tutoMolette.color = new Color(tutoMolette.color.r, tutoMolette.color.g, tutoMolette.color.b, alpha);
            yield return null;
        }

        flagFadeInMolette = false;
    }

    private IEnumerator FadeOutMolette()
    {
        float alpha = tutoMolette.color.a;
        while(alpha > 0)
        {
            alpha -= Time.deltaTime * speedFade;
            tutoMolette.color = new Color(tutoMolette.color.r, tutoMolette.color.g, tutoMolette.color.b, alpha);
            yield return null;
        }
    }

    private IEnumerator FadeInClick()
    {
        flagFadeInClick = true;
        float alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * speedFade;
            tutoClick.color = new Color(tutoClick.color.r, tutoClick.color.g, tutoClick.color.b, alpha);
            yield return null;
        }

       // flagFadeInClick = false;
    }

    private IEnumerator FadeOutClick()
    {
        flagFadeInClick = false;
        float alpha = tutoClick.color.a;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * speedFade;
            tutoClick.color = new Color(tutoClick.color.r, tutoClick.color.g, tutoClick.color.b, alpha);
            yield return null;
        }
    }
}
