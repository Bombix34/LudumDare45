using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ashe : SpaceElement
{
    [SerializeField] AsheSettings settings;

    protected override void Awake()
    {
        float size = Random.Range(settings.minSizeOnSpawn, settings.maxSizeOnSpawn);
        transform.localScale = new Vector3(size,size,size);
        body.mass *= size;
        body.velocity = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-1f, 1f)) * Random.Range(1f, 8f);
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        //transform.LookAt
        body.MoveRotation(Random.Range(0f, 360f));
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
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
                GameManager.Instance.RemoveAshe(other.gameObject);
            }
            else
            {
                //other.GetComponent<Rigidbody2D>().velocity+=(body.velocity*0.5f);
                float newSize = transform.localScale.x + other.transform.localScale.x;
                if (newSize > settings.maxSizeAshes)
                    newSize = settings.maxSizeAshes;
                other.transform.localScale = new Vector3(newSize, newSize, newSize);
                GameManager.Instance.RemoveAshe(this.gameObject);
            }
        }
    }

}
