using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : Singleton<DebugUI>
{
    [SerializeField] Text ashesArea;

    public void UpdateAshes(int nb)
    {
        ashesArea.text = nb.ToString();
    }
}
