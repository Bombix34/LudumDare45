using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : SpaceElement
{
    [SerializeField] HoleSettings settings;

    [SerializeField] PointEffector2D asheArea;
    [SerializeField] PointEffector2D planetArea;
    [SerializeField] PointEffector2D starArea;
    [SerializeField] PointEffector2D holeArea;

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
        UpdateGravity();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        body.velocity = new Vector2(0f, 0f);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
        Gizmos.DrawSphere(this.transform.position, settings.gravityRange * transform.localScale.x);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Ashe")
        {
            float newSize = transform.localScale.x + 0.05f;
            if (newSize > settings.maxSizeHole)
                newSize = settings.maxSizeHole;
            transform.localScale = new Vector3(newSize, transform.localScale.y, newSize);
            UpdateGravity();
            body.mass += (other.GetComponent<Rigidbody2D>().mass * settings.AddMassMultiplicator);
            GameManager.Instance.RemoveAshe(other.gameObject);
            CheckNextStep();
        }
        else if (other.tag == "Planet")
        {
            float newSize = transform.localScale.x + 0.1f;
            if (newSize > settings.maxSizeHole)
                newSize = settings.maxSizeHole;
            transform.localScale = new Vector3(newSize, transform.localScale.y, newSize);
            UpdateGravity();
            body.mass += (other.GetComponent<Rigidbody2D>().mass * settings.AddMassMultiplicator);
            GameManager.Instance.RemovePlanet(other.gameObject);
            CheckNextStep();
        }
        else if (other.tag == "Star")
        {
            float newSize = transform.localScale.x + 0.5f;
            if (newSize > settings.maxSizeHole)
                newSize = settings.maxSizeHole;
            transform.localScale = new Vector3(newSize, transform.localScale.y, newSize);
            UpdateGravity();
            body.mass += (other.GetComponent<Rigidbody2D>().mass * settings.AddMassMultiplicator);
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
                UpdateGravity();
                body.mass += (other.GetComponent<Rigidbody2D>().mass * settings.AddMassMultiplicator);
                GameManager.Instance.RemoveHole(other.gameObject);
                CheckNextStep();
            }
        }
    }

    void UpdateGravity()
    {
        asheArea.forceMagnitude = settings.gravityOnAshe;
        planetArea.forceMagnitude = settings.gravityOnPlanet;
        starArea.forceMagnitude = settings.gravityOnStar;
        holeArea.forceMagnitude = settings.gravityOnHoles;

        asheArea.GetComponent<CircleCollider2D>().radius = (settings.gravityRange * transform.localScale.x) / 3.5433f;
        planetArea.GetComponent<CircleCollider2D>().radius = (settings.gravityRange * transform.localScale.x) / 3.5433f;
        starArea.GetComponent<CircleCollider2D>().radius = (settings.gravityRange * transform.localScale.x) / 3.5433f;
        starArea.GetComponent<CircleCollider2D>().radius = (settings.gravityRange * transform.localScale.x) / 3.5433f;

       // GetComponent<CircleCollider2D>().radius = 0.15f * transform.localScale.x;
    }

    public override void CheckNextStep()
    {
    }
    public override void AddNewMaterial()
    {
    }


    public void OnDestroy()
    {
        ScreenShake.instance.StartScreenShake(GetComponent<Rigidbody2D>().mass);
    }
}