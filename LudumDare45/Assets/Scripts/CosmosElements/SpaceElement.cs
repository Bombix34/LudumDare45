using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceElement : MonoBehaviour
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

    public abstract void CheckNextStep();

    protected void OnBecameInvisible()
    {
        Vector3 screenRange = GameManager.Instance.ScreenRange;
        if (transform.localPosition.x > screenRange.x/2)
        {
            transform.localPosition = new Vector3(-screenRange.x/2, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.x < -screenRange.x / 2)
        {
            transform.localPosition = new Vector3(screenRange.x / 2, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.y > screenRange.y / 2)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -screenRange.y / 2, transform.localPosition.z);
        }
        else if (transform.localPosition.y < -screenRange.y / 2)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, screenRange.y / 2, transform.localPosition.z);
        }
    }


}
