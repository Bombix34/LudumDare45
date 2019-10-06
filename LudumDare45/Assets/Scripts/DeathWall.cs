using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag =="Ashe")
        {
            GameManager.Instance.RemoveAshe(other);
        }
        else if(other.tag =="Planet")
        {
            GameManager.Instance.RemovePlanet(other);
        }
        else if (other.tag == "Star")
        {
            GameManager.Instance.RemoveStar(other);
        }
        else if (other.tag == "Hole")
        {
            GameManager.Instance.RemoveHole(other);
        }
        Destroy(collision.gameObject);
    }
}
