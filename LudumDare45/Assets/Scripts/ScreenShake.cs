using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    static public ScreenShake instance;

    public float powerScreenShake = 1;
    public float timeScreenShake = 1;

    private bool flagCoroutine;
    private Coroutine screenShakeCo;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

  /*  public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartScreenShake(1, 2);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartScreenShake(0.2f, 5);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartEndlessScreenShake(2);
        }
    }*/


    public void StartScreenShake(float power, float time)
    {
        // there already is an instance of screenShake running, we check if the power of the last coroutine is lower than the new one
        if (flagCoroutine)
        {
            if(power >= this.powerScreenShake)
            {
                // if so, we start a new screenshake wich is more powerful than the older one
                StopCoroutine(screenShakeCo);
                Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
                initializeCoroutine(power, time);
            }
        }
        else
        {
            initializeCoroutine(power, time);
        }
    }

    public void StartEndlessScreenShake(float power)
    {
        StopScreenShake();
        float time = 3600;
        initializeCoroutine(power, time);
    }

    public void StopScreenShake()
    {
        if (flagCoroutine)
        {
            StopCoroutine(screenShakeCo);
            Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
        }
    }

    private void initializeCoroutine(float power, float time)
    {
        this.powerScreenShake = power;
        this.timeScreenShake = time;
        screenShakeCo = StartCoroutine(ScreenShakeCoroutine());
    }

    private IEnumerator ScreenShakeCoroutine()
    {
        flagCoroutine = true;
        float countdown = 0;
        while (countdown < timeScreenShake)
        {
            float x = Random.Range(-powerScreenShake, powerScreenShake);
            float y = Random.Range(-powerScreenShake, powerScreenShake);

            Camera.main.transform.position = new Vector3(x, y, Camera.main.transform.position.z);

            countdown += Time.deltaTime;
            yield return null;
        }

        // we reset the camera position at the end of the animation 
        Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);

        flagCoroutine = false;

    }

}
