using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : SpaceElement
{
    [SerializeField] HoleSettings settings;

    protected override void Awake()
    {
        float size = Random.Range(settings.SizeOnSpawn.minValue, settings.SizeOnSpawn.maxValue);
        transform.localScale = new Vector3(size, size, size);
        body.mass = Random.Range(settings.MassOnSpawn.minValue, settings.MassOnSpawn.maxValue);
        body.velocity = new Vector2(0f,0f);
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
        body.velocity = new Vector2(0f, 0f);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Ashe")
        {
            float newSize = transform.localScale.x + 0.05f;
            if (newSize > settings.maxSizeHole)
                newSize = settings.maxSizeHole;
            transform.localScale = new Vector3(newSize, newSize, newSize);
            body.mass += other.GetComponent<Rigidbody2D>().mass;
            GameManager.Instance.RemoveAshe(other.gameObject);
            CheckNextStep();
        }
        else if (other.tag == "Planet")
        {
            float newSize = transform.localScale.x + 0.1f;
            if (newSize > settings.maxSizeHole)
                newSize = settings.maxSizeHole;
            transform.localScale = new Vector3(newSize, newSize, newSize);
            body.mass += other.GetComponent<Rigidbody2D>().mass;
            GameManager.Instance.RemovePlanet(other.gameObject);
            CheckNextStep();
        }
        else if (other.tag == "Star")
        {
            float newSize = transform.localScale.x + 0.5f;
            if (newSize > settings.maxSizeHole)
                newSize = settings.maxSizeHole;
            transform.localScale = new Vector3(newSize, newSize, newSize);
            body.mass += other.GetComponent<Rigidbody2D>().mass;
            GameManager.Instance.RemoveStar(other.gameObject);
            CheckNextStep();
        }
        else if(other.tag=="Hole")
        {
            Rigidbody2D otherbody = other.GetComponent<Rigidbody2D>();
            if (otherbody.mass  < body.mass )
            {
                float newSize = transform.localScale.x + other.transform.localScale.x;
                if (newSize > settings.maxSizeHole)
                    newSize = settings.maxSizeHole;
                transform.localScale = new Vector3(newSize, newSize, newSize);
                body.mass += other.GetComponent<Rigidbody2D>().mass;
                GameManager.Instance.RemoveHole(other.gameObject);
                CheckNextStep();
            }
        }
    }

    public override void CheckNextStep()
    {
    }
    public override void AddNewMaterial()
    {
    }

}