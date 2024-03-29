﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : SpaceElement
{
    [SerializeField] StarSettings settings;

    [SerializeField] Light starLight;
    float lightIntensity;

    public StarState curState;

    [SerializeField] GameObject particleLittle, lightLittle;
    [SerializeField] GameObject particleMedium, lightMedium;
    [SerializeField] GameObject particleBig, lightBig;
    [SerializeField] Material mediumMaterial;
    [SerializeField] Material bigMaterial;

    [SerializeField] PointEffector2D asheArea;
    [SerializeField] PointEffector2D planetArea;
    [SerializeField] PointEffector2D starArea;
    
    protected override void Awake()
    {
        float size = Random.Range(settings.SizeOnSpawn.minValue, settings.SizeOnSpawn.maxValue);
        curState = StarState.little;
        transform.localScale = new Vector3(size, size, size);
        UpdateParticlesSize();
        body.mass = Random.Range(settings.MassOnSpawn.minValue, settings.MassOnSpawn.maxValue);
        body.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(settings.SpeedOnSpawn.minValue, settings.SpeedOnSpawn.maxValue);
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        body.MoveRotation(Random.Range(0f, 360f));
        //lightIntensity = Random.Range(settings.lightIntensity.minValue, settings.lightIntensity.maxValue);

        particleMedium.SetActive(false);
        lightMedium.SetActive(false);
        particleBig.SetActive(false);
        lightBig.SetActive(false);

        absorption = AnimationAbsorbtion.Initialize();
    }

    private void Start()
    {
        AddNewMaterial();
        UpdateGravity();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (this.body.mass > settings.massMax)
        {
            this.body.mass = settings.massMax;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(this.transform.position,settings.gravityRange * transform.localScale.x);
    }

    public void AsheInteraction(GameObject dust)
    {
        float newSize = transform.localScale.x + (0.005f * settings.AddSizeMultiplicator);
        if (newSize > settings.maxSizeStar)
            newSize = settings.maxSizeStar;
        transform.localScale = new Vector3(newSize, newSize, newSize);
        UpdateParticlesSize();
        UpdateGravity();
        body.mass += (dust.GetComponent<Rigidbody2D>().mass * settings.AddMassMultiplicator);
        GameManager.Instance.RemoveAshe(dust.gameObject);
        CheckNextStep();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Planet")
        {
            float newSize = transform.localScale.x + (0.01f * settings.AddSizeMultiplicator);
            if (newSize > settings.maxSizeStar)
                newSize = settings.maxSizeStar;
            transform.localScale = new Vector3(newSize, newSize, newSize);
            UpdateParticlesSize();
            UpdateGravity();
            body.mass += (other.GetComponent<Rigidbody2D>().mass * settings.AddMassMultiplicator);
            //GameManager.Instance.RemovePlanet(other.gameObject);
            CheckNextStep();
            SpawnSubEmitters(collision, settings.DustsCreatedOnCollidingWithPlanet);
            ScreenShake.instance.StartScreenShake(other.GetComponent<Rigidbody2D>().mass);
            animationAbsorption(other.GetComponent<Planet>());
        }
        else if (other.tag == "Star")
        {
            Rigidbody2D otherbody = other.GetComponent<Rigidbody2D>();
            if (otherbody.mass + other.transform.localScale.magnitude < body.mass + transform.localScale.magnitude)
            {

                other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                float newSize = transform.localScale.x + (other.transform.localScale.x * settings.AddSizeMultiplicator);
                if (newSize > settings.maxSizeStar)
                    newSize = settings.maxSizeStar;
                transform.localScale = new Vector3(newSize, newSize, newSize);
                UpdateParticlesSize();
                UpdateGravity();
                body.mass += (other.GetComponent<Rigidbody2D>().mass*settings.AddMassMultiplicator);
                //GameManager.Instance.RemoveStar(other.gameObject);
                CheckNextStep();
                SpawnSubEmitters(collision, settings.DustsCreatedOnCollidingWithStar);
                ScreenShake.instance.StartScreenShake(other.GetComponent<Rigidbody2D>().mass);
                animationAbsorption(other.GetComponent<Star>());
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
                StartCoroutine(Explosion());
                manager.AddHole(nextStepObject = Instantiate(manager.HolePrefab, this.transform.position, Quaternion.identity));
                SoundManager.Instance.PlaySound(7);
                manager.RemoveStar(this.gameObject);
                willTransformForNextStep = true;
            }
        }
    }

    void UpdateParticlesSize()
    {
        var shape = particleLittle.GetComponent<ParticleSystem>().shape;
        shape.radius = 4f*transform.localScale.x;
        shape = particleMedium.GetComponent<ParticleSystem>().shape;
        shape.radius = 4.75f * transform.localScale.x;
        shape = particleBig.GetComponent<ParticleSystem>().shape;
        shape.radius = 5.5f * transform.localScale.x;
    }

    void UpdateGravity()
        //updte gravity et scale
    {
        asheArea.forceMagnitude = settings.gravityOnAshe;
        planetArea.forceMagnitude = settings.gravityOnPlanet;
        starArea.forceMagnitude = settings.gravityOnStar;

        asheArea.GetComponent<CircleCollider2D>().radius = settings.gravityRange*transform.localScale.x;
        planetArea.GetComponent<CircleCollider2D>().radius = settings.gravityRange * transform.localScale.x;
        starArea.GetComponent<CircleCollider2D>().radius = settings.gravityRange * transform.localScale.x;

        //GetComponent<CircleCollider2D>().radius =3.2f * transform.localScale.x;
    }

     IEnumerator Explosion()
    {
        GameObject ashe;
        for(int i = 0; i<settings.ashesNumber; i++)
        {
            ashe = Instantiate(GameManager.Instance.AshePrefab, this.transform.position + new Vector3(Random.Range(-1f, 1f),Random.Range(-1f, 1f),0), Quaternion.identity);
            GameManager.Instance.AddAshes(ashe);
            ashe.GetComponent<Ashe>().ChangeVelocity(Vector3.Normalize(ashe.transform.position - this.transform.position), settings.ashesSpeed);
        }

        yield return new WaitForSeconds(0.5f);
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

    private void SpawnSubEmitters(Collision2D collision, int numberOfDustToCreate)
    {
        Vector2 p = collision.GetContact(0).point;
        Vector3 point = new Vector3(p.x, p.y, this.transform.position.z);
        for (int i = 0; i < numberOfDustToCreate; i++)
        {
            GameManager.Instance.AddAshes(Instantiate(GameManager.Instance.AshePrefab, point, Quaternion.identity));
        }
    }
}