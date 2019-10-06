using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ashe : SpaceElement
{
    [SerializeField] AsheSettings settings;

    UTimer timerAsheInvulnerable;

    float timeInvulnerable = 0.5f;

    bool IsInvulnerable = true;

    protected override void Awake()
    {
        float size = Random.Range(settings.SizeOnSpawn.minValue, settings.SizeOnSpawn.maxValue);
        transform.localScale = new Vector3(size,size,size);
        body.mass = Random.Range(settings.MassOnSpawn.minValue,settings.MassOnSpawn.maxValue);
        body.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(settings.SpeedOnSpawn.minValue, settings.SpeedOnSpawn.maxValue);
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        //transform.LookAt
        body.MoveRotation(Random.Range(0f, 360f));
    }

    protected void Start()
    {
        timerAsheInvulnerable = UTimer.Initialize(timeInvulnerable, this, becomeNotInvulnerable);
        timerAsheInvulnerable.start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {

        if (IsInvulnerable)
        {
            return;
        }

        GameObject other = collision.gameObject;
        if(other.tag=="Ashe")
        {
            Vector2 otherVelocity = other.GetComponent<Rigidbody2D>().velocity;
            if (otherVelocity.magnitude + (5 * other.GetComponent<Rigidbody2D>().mass) < body.velocity.magnitude + (5 * body.mass)) 
            {
               // body.velocity += (otherVelocity * 0.5f);
                float newSize = transform.localScale.x + other.transform.localScale.x;
                if (newSize > settings.maxSizeAshes)
                    newSize = settings.maxSizeAshes;
                transform.localScale = new Vector3(newSize, newSize, newSize);
                body.mass += other.GetComponent<Rigidbody2D>().mass;
                GameManager.Instance.RemoveAshe(other.gameObject);
                CheckNextStep();
            }
        }
    }

    public override void CheckNextStep()
    {
        if(body.mass>=settings.massToTransform)
        {
            float rand = Random.Range(0f, 1f);
            if(rand<(settings.chanceToTransform/100))
            {
                GameManager manager = GameManager.Instance;
                manager.AddPlanet(Instantiate(manager.PlanetPrefab, this.transform.position, Quaternion.identity));
                manager.RemoveAshe(this.gameObject);
            }
        }
    }

    public override void AddNewMaterial()
    {
        GetComponent<MeshRenderer>().material = settings.GetRandomMaterial();
    }

    private void becomeNotInvulnerable()
    {
        IsInvulnerable = false;
    }

}
