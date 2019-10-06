using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : Singleton<DebugUI>
{
    [SerializeField] Text ashesArea;
    [SerializeField] Text planetsArea;

    public void UpdateAshes(int nb)
    {
        ashesArea.text = nb.ToString();
    }

    public void UpdatePlanets(int nb)
    {
        planetsArea.text = nb.ToString();
    }
}
