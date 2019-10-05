using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ashe : SpaceElement
{

    protected override void Awake()
    {
        float size = Random.Range(0.2f, 0.8f);
        transform.localScale = new Vector3(size,size,size);
        body.mass *= size;
        body.velocity = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-1f, 1f)) * Random.Range(1f, 8f);
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
                body.velocity += (otherVelocity * 0.5f);
                float newSize = transform.localScale.x + other.transform.localScale.x;
                if (newSize > 3f)
                    newSize = 3f;
                transform.localScale = new Vector3(newSize, newSize, newSize);
                GameManager.Instance.RemoveAshe(other.gameObject);
                print(body.velocity.magnitude);
            }
            else
            {
                other.GetComponent<Rigidbody2D>().velocity+=(body.velocity*0.5f);
                float newSize = transform.localScale.x + other.transform.localScale.x;
                if (newSize > 3f)
                    newSize = 3f;
                other.transform.localScale = new Vector3(newSize, newSize, newSize);
                GameManager.Instance.RemoveAshe(this.gameObject);
            }
        }
    }

}
