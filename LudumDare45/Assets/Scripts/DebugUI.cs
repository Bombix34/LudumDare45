using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : Singleton<DebugUI>
{
    [SerializeField] Text ashesArea;
    [SerializeField] Text planetsArea;
    [SerializeField] Text starsArea;

    public void UpdateAshes(int nb)
    {
        ashesArea.text = nb.ToString();
    }

    public void UpdatePlanets(int nb)
    {
        planetsArea.text = nb.ToString();
    }

    public void UpdateStars(int nb)
    {
        starsArea.text = nb.ToString();
    }
}
