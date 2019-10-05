using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceElement : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D body;


    protected virtual void Awake()
    {
    }

    protected void Update()
    {
    }
    protected virtual void FixedUpdate()
    {
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
    }

    protected void OnBecameInvisible()
    {
        if (transform.localPosition.x > 14f)
        {
            transform.localPosition = new Vector3(-1* transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.x < -14f)
        {
            transform.localPosition = new Vector3(-1f *transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.y > 6f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -1f *transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.y < -6f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -1f* transform.localPosition.y, transform.localPosition.z);
        }
    }


}
