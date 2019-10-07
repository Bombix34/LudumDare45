using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : SpaceElement
{
    [SerializeField] PlanetSettings settings;

    [SerializeField] PointEffector2D gravitySystem;
    [SerializeField] CircleCollider2D gravityArea;

    protected override void Awake()
    {
        float size = Random.Range(settings.SizeOnSpawn.minValue, settings.SizeOnSpawn.maxValue);
        transform.localScale = new Vector3(size, size, size);
        body.mass = Random.Range(settings.MassOnSpawn.minValue, settings.MassOnSpawn.maxValue);
        body.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(settings.SpeedOnSpawn.minValue, settings.SpeedOnSpawn.maxValue);
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        body.MoveRotation(Random.Range(0f, 360f));
    }

    private void Start()
    {
        AddNewMaterial();
        gravitySystem.forceMagnitude = settings.planetGravity;
        gravityArea.radius = settings.gravityRange ;
    }

    protected override void Update()
    {
        // A SUPPRIMER
        gravitySystem.forceMagnitude = settings.planetGravity;
        gravityArea.radius = settings.gravityRange;
        //
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
        Gizmos.DrawSphere(this.transform.position, settings.gravityRange);
    }

    public void AsheInteraction(GameObject dust)
    {
        float newSize = transform.localScale.x + (0.005f * settings.AddSizeMultiplicator);
        if (newSize > settings.maxSizePlanet)
            newSize = settings.maxSizePlanet;
        transform.localScale = new Vector3(newSize, newSize, newSize);
        UpdateScale();
        body.mass += (dust.GetComponent<Rigidbody2D>().mass * settings.AddMassMultiplicator);
        GameManager.Instance.RemoveAshe(dust.gameObject);
        CheckNextStep();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag=="Planet")
        {
            Rigidbody2D otherbody = other.GetComponent<Rigidbody2D>();
            if(otherbody.mass+other.transform.localScale.magnitude<body.mass+transform.localScale.magnitude)
            {

                other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                float newSize = transform.localScale.x + (other.transform.localScale.x*settings.AddSizeMultiplicator);
                if (newSize > settings.maxSizePlanet)
                    newSize = settings.maxSizePlanet;
                transform.localScale = new Vector3(newSize, newSize, newSize);
                UpdateScale();
                body.mass +=( other.GetComponent<Rigidbody2D>().mass * settings.AddMassMultiplicator);
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

    public void UpdateScale()
    {
        //GetComponent<CircleCollider2D>().radius = 5 * transform.localScale.x;
    }

    public override void AddNewMaterial()
    {
        GetComponent<MeshRenderer>().material = settings.GetRandomMaterial();
    }

    public void OnDestroy()
    {
        ScreenShake.instance.StartScreenShake(GetComponent<Rigidbody2D>().mass);
    }
}