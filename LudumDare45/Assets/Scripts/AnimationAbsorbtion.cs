using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class AnimationAbsorbtion
{

    MonoBehaviour mono;
    UnityAction callbackFunction;


    public static AnimationAbsorbtion Initialize()
    {
        return new AnimationAbsorbtion();
    }



    public void startAnimationAbsorbtion(GameObject p, GameObject g, MonoBehaviour mono, UnityAction callbackFunction)
    {

        p.GetComponent<CircleCollider2D>().enabled = false;
        p.gameObject.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, g.transform.position.z + 0.1f);
        this.callbackFunction = callbackFunction;
        mono.StartCoroutine(LerpToTheOtherAstre(p, g));
    }



    private IEnumerator LerpToTheOtherAstre(GameObject p, GameObject g)
    {
        float speed = 0.2f;
        float avancee = 0;
        float distance = 1;
        bool error = false;

        while (!UnityEngine.Object.ReferenceEquals(g, null) && error == false && distance > 0.05f && distance < 10)
        {
            try
            {
                distance = Vector2.Distance(p.transform.position, g.transform.position);
                Vector2 newPos = Vector2.Lerp(p.transform.position, g.transform.position, avancee);
                p.transform.position = new Vector3(newPos.x, newPos.y, p.transform.position.z);

                avancee += Time.deltaTime * speed;

            }
            catch (Exception e)
            {
                error = true;
                Debug.LogWarning("ERREUR TRY CATCH");
            }

            yield return null;
        }

        /*if(Vector3.Distance(p.transform.position, g.transform.position) < 0.05f)
        {
            Debug.Log("MANGE");
        }

        if(UnityEngine.Object.ReferenceEquals(g, null))
        {
            Debug.LogWarning("ERREUR");
        }*/

        callbackFunction();

    }

    /*
     *                 CheckNextStep();
                body.mass += other.GetComponent<Rigidbody2D>().mass;
                //GameManager.Instance.RemovePlanet(other.gameObject);
                
                absorbtion.startAnimationAbsorbtion(other.gameObject, gameObject, () =>
                {
                    GameManager.Instance.RemovePlanet(other.gameObject);
                });*/

/*
            else if(other.tag=="Planet")
        {
            Rigidbody2D otherbody = other.GetComponent<Rigidbody2D>();
            if(otherbody.mass+other.transform.localScale.magnitude<body.mass+transform.localScale.magnitude)
            {

                other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                float newSize = transform.localScale.x + (other.transform.localScale.x * settings.AddSizeMultiplicator);
                if (newSize > settings.maxSizePlanet)
                    newSize = settings.maxSizePlanet;
                transform.localScale = new Vector3(newSize, newSize, newSize);
    UpdateScale();
    body.mass +=(other.GetComponent<Rigidbody2D>().mass* settings.AddMassMultiplicator);
    //GameManager.Instance.RemovePlanet(other.gameObject);
    CheckNextStep();
    animationAbsorption(other.GetComponent<Planet>());
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
            manager.AddStar(nextStepObject = Instantiate(manager.StarPrefab, this.transform.position, Quaternion.identity));
            manager.RemovePlanet(this.gameObject);
            willTransformForNextStep = true;
        }
    }
}*/

/*    protected void animationAbsorption(Planet other)
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
}*/
}
