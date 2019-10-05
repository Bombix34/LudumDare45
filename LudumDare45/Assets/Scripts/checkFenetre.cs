using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkFenetre : MonoBehaviour
{

    public Vector3 range;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 StartPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 EndPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        range = EndPoint - StartPoint;

        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20f)));
        print(range);
    }
}
