using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSouris : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        Debug.Log(position);

    }
}
