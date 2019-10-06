using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : SpaceElement
{
    [SerializeField] StarSettings settings;

    [SerializeField] Light starLight;
    float lightIntensity;

    StarState curState;

    [SerializeField] GameObject particleLittle, lightLittle;
    [SerializeField] GameObject particleMedium, lightMedium;
    [SerializeField] GameObject particleBig, lightBig;
    [SerializeField] Material mediumMaterial;
    [SerializeField] Material bigMaterial;
    protected override void Awake()
    {
        float size = Random.Range(settings.SizeOnSpawn.minValue, settings.SizeOnSpawn.maxValue);
        curState = StarState.little;
        transform.localScale = new Vector3(size, size, size);
        body.mass = Random.Range(settings.MassOnSpawn.minValue, settings.MassOnSpawn.maxValue);
        body.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(settings.SpeedOnSpawn.minValue, settings.SpeedOnSpawn.maxValue);
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        //transform.LookAt
        body.MoveRotation(Random.Range(0f, 360f));
        //lightIntensity = Random.Range(settings.lightIntensity.minValue, settings.lightIntensity.maxValue);

        particleMedium.SetActive(false);
        lightMedium.SetActive(false);
        particleBig.SetActive(false);
        lightBig.SetActive(false);
    }

    private void Start()
    {
        AddNewMaterial();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Ashe")
        {
            float newSize = transform.localScale.x + 0.01f;
            if (newSize > settings.maxSizeStar)
                newSize = settings.maxSizeStar;
            transform.localScale = new Vector3(newSize, newSize, newSize);
            body.mass += other.GetComponent<Rigidbody2D>().mass;
            GameManager.Instance.RemoveAshe(other.gameObject);
            CheckNextStep();
        }
        else if (other.tag == "Planet")
        {
            float newSize = transform.localScale.x + 0.01f;
            if (newSize > settings.maxSizeStar)
                newSize = settings.maxSizeStar;
            transform.localScale = new Vector3(newSize, newSize, newSize);
            body.mass += other.GetComponent<Rigidbody2D>().mass;
            GameManager.Instance.RemovePlanet(other.gameObject);
            CheckNextStep();
        }
        else if (other.tag == "Star")
        {
            Rigidbody2D otherbody = other.GetComponent<Rigidbody2D>();
            if (otherbody.mass + other.transform.localScale.magnitude < body.mass + transform.localScale.magnitude)
            {
                float newSize = transform.localScale.x + other.transform.localScale.x;
                if (newSize > settings.maxSizeStar)
                    newSize = settings.maxSizeStar;
                transform.localScale = new Vector3(newSize, newSize, newSize);
                body.mass += other.GetComponent<Rigidbody2D>().mass;
                GameManager.Instance.RemoveStar(other.gameObject);
                CheckNextStep();
            }
        }
    }

    private void UpdateLightIntensity()
    {
       // starLight.intensity = lightIntensity * (transform.localScale.magnitude * 10f);
    }

    public override void CheckNextStep()
    {
        if(transform.localScale.x>settings.sizeToBeBigRedStar&&curState!=StarState.big)
        {
            curState = StarState.big;
            particleBig.SetActive(true);
            lightBig.SetActive(true);
            particleMedium.SetActive(false);
            lightMedium.SetActive(false);
            particleLittle.SetActive(false);
            lightLittle.SetActive(false);
            GetComponent<MeshRenderer>().material = bigMaterial;
        }
        else if(transform.localScale.x>settings.sizeToBeMediumStar && curState ==StarState.little )
        {
            curState = StarState.medium;
            particleMedium.SetActive(true);
            lightMedium.SetActive(true);
            particleLittle.SetActive(false);
            lightLittle.SetActive(false);
            GetComponent<MeshRenderer>().material = mediumMaterial;
        }
        if (body.mass >= settings.massToTransform)
        {
            float rand = Random.Range(0f, 1f);
            if (rand < (settings.chanceToTransform / 100))
            {
                GameManager manager = GameManager.Instance;
                manager.AddHole(Instantiate(manager.HolePrefab, this.transform.position, Quaternion.identity));
                manager.RemoveStar(this.gameObject);
            }
        }
    }

    public override void AddNewMaterial()
    {
        GetComponent<MeshRenderer>().material = settings.GetRandomMaterial();
    }

    public enum StarState
    {
        little,
        medium,
        big
    }
}