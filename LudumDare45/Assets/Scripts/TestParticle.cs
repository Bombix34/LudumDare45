using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticle : MonoBehaviour
{

    Vector2 baseVelocity;

    void Awake()
    {
        baseVelocity = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-1f, 1f));
    }

    void Update()
    {
        transform.Translate(baseVelocity*0.1f);
    }
}
