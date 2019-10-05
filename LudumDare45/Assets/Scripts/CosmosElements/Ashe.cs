using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ashe : MonoBehaviour
{
    Vector2 baseVelocity;

    [SerializeField] Rigidbody2D body;

    void Awake()
    {
        baseVelocity = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-1f, 1f)) * Random.Range(1f, 8f);
        float size = Random.Range(0.2f, 0.8f);
        transform.localScale = new Vector3(size,size,size);
        body.velocity = baseVelocity;
    }

    void FixedUpdate()
    {
        foreach(var item in this.GetComponentsInChildren<Rigidbody2D>())
        {
            item.velocity = body.velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag=="Ashe")
        {
            Vector2 otherVelocity = other.GetComponent<Rigidbody2D>().velocity;
            if (otherVelocity.magnitude < body.velocity.magnitude)
            {
                other.transform.parent = this.transform;
                other.GetComponent<Rigidbody2D>().velocity = body.velocity;
            }
            else
            {
                this.transform.parent = other.transform;
                body.velocity = other.GetComponent<Rigidbody2D>().velocity;
            }
        }
    }
}
