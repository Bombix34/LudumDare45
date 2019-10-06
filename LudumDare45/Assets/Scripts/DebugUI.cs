using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : Singleton<DebugUI>
{
    [SerializeField] Text ashesArea;
    [SerializeField] Text planetsArea;
    [SerializeField] Text starsArea;
    [SerializeField] Text holesArea;

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

    public void UpdateHoles(int nb)
    {
        holesArea.text = nb.ToString();
    }
}
