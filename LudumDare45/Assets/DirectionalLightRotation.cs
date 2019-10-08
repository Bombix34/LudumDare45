using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightRotation : MonoBehaviour
{
    Color32[] colors;
    Color32 newColor;
    Light dirLight;

    void Start()
    {
        colors = new Color32[6];
        colors[0] = new Color32(0xFF,0x00,0x8c,0xFF);
        colors[1] = new Color32(0x64,0x00,0xFF,0xFF);
        colors[2] = new Color32(0xF0,0x0D,0xFF,0xFF);
        colors[3] = new Color32(0x00,0xF9,0xFF,0xFF);
        colors[4] = new Color32(0x00,0xFF,0xB9,0xFF);
        colors[5] = new Color(1, 1, 1, 1);
        dirLight = GetComponent<Light>();
        newColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
            dirLight.color = Color.Lerp(dirLight.color, newColor, 0.005f);
            if (dirLight.color == newColor)
            {
                int randColor = Random.Range(0, 5);
                newColor = colors[randColor];
            }
        
    }
}
