using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceElement : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D body;
    protected AnimationAbsorbtion absorption;
    protected GameObject nextStepObject;
    protected bool willTransformForNextStep;

    protected virtual void Awake()
    {
    }

    protected virtual void Update()
    {
    }
    protected virtual void FixedUpdate()
    {
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
    }

    public abstract void CheckNextStep();
    public abstract void AddNewMaterial();

    protected void OnBecameInvisible()
    {
        Vector3 screenRange = GameManager.Instance.ScreenRange;
        Vector3 resultPos = Vector3.zero;
        if (transform.localPosition.x > screenRange.x/2)
        {
            //Droite
            resultPos = new Vector3(-screenRange.x / 2, transform.localPosition.y, transform.localPosition.z);
            if (this.gameObject.tag != "Ashe")
            {
                float radius = GetComponent<Renderer>().bounds.extents.magnitude;
                resultPos = new Vector3(resultPos.x - radius, resultPos.y, resultPos.z);
            }
            transform.localPosition = resultPos;
        }
        else if (transform.localPosition.x < -screenRange.x / 2)
        {
            resultPos = new Vector3(screenRange.x / 2, transform.localPosition.y, transform.localPosition.z);
            if (this.gameObject.tag != "Ashe")
            {
                float radius = GetComponent<Renderer>().bounds.extents.magnitude;
                resultPos = new Vector3(resultPos.x + radius, resultPos.y, resultPos.z);
            }
            transform.localPosition = resultPos;
        }
        if (transform.localPosition.y > screenRange.y / 2)
        {
            resultPos = new Vector3(transform.localPosition.x, -screenRange.y / 2, transform.localPosition.z);
            if (this.gameObject.tag != "Ashe")
            {
                float radius = GetComponent<Renderer>().bounds.extents.magnitude;
                resultPos = new Vector3(resultPos.x, resultPos.y - radius, resultPos.z);
            }
            transform.localPosition = resultPos;
        }
        else if (transform.localPosition.y < -screenRange.y / 2)
        {
            resultPos = new Vector3(transform.localPosition.x, screenRange.y / 2, transform.localPosition.z);
            if (this.gameObject.tag != "Ashe")
            {
                float radius = GetComponent<Renderer>().bounds.extents.magnitude;
                resultPos = new Vector3(resultPos.x + radius, resultPos.y + radius, resultPos.z);
            }
            transform.localPosition = resultPos;
        }
    }

    protected void animationAbsorption(Planet other)
    {
        // if the current element will transform at the next frame
        if (willTransformForNextStep)
        {
            // we set the animation not on the current element, but on the next one
            other.absorption.startAnimationAbsorbtion(other.gameObject, nextStepObject, other, () =>
            {
                GameManager.Instance.RemovePlanet(other.gameObject);
            });
            willTransformForNextStep = false;
        }
        else
        {
            other.absorption.startAnimationAbsorbtion(other.gameObject, gameObject, other, () =>
            {
                GameManager.Instance.RemovePlanet(other.gameObject);
            });
        }
    }

    protected void animationAbsorption(Star other)
    {
        // if the current element will transform at the next frame
        if (willTransformForNextStep)
        {
            // we set the animation not on the current element, but on the next one
            other.absorption.startAnimationAbsorbtion(other.gameObject, nextStepObject, this, () =>
            {
                GameManager.Instance.RemoveStar(other.gameObject);
            });
            willTransformForNextStep = false;
        }
        else
        {
            other.absorption.startAnimationAbsorbtion(other.gameObject, gameObject, this, () =>
            {
                GameManager.Instance.RemoveStar(other.gameObject);
            });
        }
    }

    protected void animationAbsorption(Hole other)
    {
        // if the current element will transform at the next frame
        if (willTransformForNextStep)
        {
            // we set the animation not on the current element, but on the next one
            other.absorption.startAnimationAbsorbtion(other.gameObject, nextStepObject, this, () =>
            {
                GameManager.Instance.RemoveHole(other.gameObject);
            });
            willTransformForNextStep = false;
        }
        else
        {
            other.absorption.startAnimationAbsorbtion(other.gameObject, gameObject, this, () =>
            {
                GameManager.Instance.RemoveHole(other.gameObject);
            });
        }
    }
}
