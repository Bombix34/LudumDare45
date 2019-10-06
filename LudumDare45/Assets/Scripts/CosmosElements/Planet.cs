﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : SpaceElement
{
    [SerializeField] PlanetSettings settings;

    protected override void Awake()
    {
        float size = Random.Range(settings.SizeOnSpawn.minValue, settings.SizeOnSpawn.maxValue);
        transform.localScale = new Vector3(size, size, size);
        body.mass = Random.Range(settings.MassOnSpawn.minValue, settings.MassOnSpawn.maxValue);
        body.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(settings.SpeedOnSpawn.minValue, settings.SpeedOnSpawn.maxValue);
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        //transform.LookAt
        body.MoveRotation(Random.Range(0f, 360f));
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
            if (newSize > settings.maxSizePlanet)
              newSize = settings.maxSizePlanet;
            transform.localScale = new Vector3(newSize, newSize, newSize);
            body.mass += other.GetComponent<Rigidbody2D>().mass;
            GameManager.Instance.RemoveAshe(other.gameObject);
            CheckNextStep();
        }
        else if(other.tag=="Planet")
        {
            Rigidbody2D otherbody = other.GetComponent<Rigidbody2D>();
            if(otherbody.mass+other.transform.localScale.magnitude<body.mass+transform.localScale.magnitude)
            {
                float newSize = transform.localScale.x + other.transform.localScale.x;
                if (newSize > settings.maxSizePlanet)
                    newSize = settings.maxSizePlanet;
                transform.localScale = new Vector3(newSize, newSize, newSize);
                body.mass += other.GetComponent<Rigidbody2D>().mass;
                GameManager.Instance.RemovePlanet(other.gameObject);
                CheckNextStep();
            }
        }
    }

    public override void CheckNextStep()
    {
        if (body.mass >= settings.massToTransform)
        {
            float rand = Random.Range(0f, 1f);
            if (rand < (settings.chanceToTransform / 100))
            {
                GameManager manager = GameManager.Instance;
                manager.AddStar(Instantiate(manager.StarPrefab, this.transform.position, Quaternion.identity));
                manager.RemovePlanet(this.gameObject);
            }
        }
    }

    public override void AddNewMaterial()
    {
        GetComponent<MeshRenderer>().material = settings.GetRandomMaterial();
    }
}