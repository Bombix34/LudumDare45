using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
	[SerializeField] AudioSource Scene1;
    [SerializeField] AudioSource Scene2;
    [SerializeField] AudioSource Scene3;
    [SerializeField] AudioSource Scene4;
    [SerializeField] AudioSource Scene5;
    [SerializeField] AudioSource Scene6;
    [SerializeField] AudioSource Scene7;
    [SerializeField] AudioSource Scene8;
    [SerializeField] AudioSource Scene9;
    [SerializeField] AudioSource Scene10;
    [SerializeField] AudioSource Scene11;
    [SerializeField] AudioSource Scene12;

    [SerializeField] AudioSource loopAmbiant;
    [SerializeField] AudioSource loopFX;


    public float musicInitVolume = 0.8f;

    Phase currentPhase = Phase.nothing;


    public void PlayNextPhase()
    {
        if(currentPhase==Phase.nothing)
        {
            currentPhase = Phase.firstDust;
            StartCoroutine(FadeInEffect(Scene1, musicInitVolume, 2f));
        }
        else if(currentPhase==Phase.firstDust)
        {
            currentPhase = Phase.manyDust;
            StartCoroutine(FadeInEffect(Scene2, musicInitVolume, 1.5f));
            StartCoroutine(FadeOutEffect(Scene1, musicInitVolume*0.75f, 2f));
        }
        else if(currentPhase==Phase.manyDust)
        {
            currentPhase = Phase.firstPlanet;
            StartCoroutine(FadeInEffect(Scene3, musicInitVolume, 1.5f));
            StartCoroutine(FadeOutEffect(Scene2, musicInitVolume * 0.75f, 2f));
            StartCoroutine(FadeOutEffect(Scene1, 0f, 2f));
        }
        else if(currentPhase==Phase.firstPlanet)
        {
            currentPhase = Phase.manyPlanet;
            StartCoroutine(FadeInEffect(Scene4, musicInitVolume, 1.5f));
            StartCoroutine(FadeOutEffect(Scene3, musicInitVolume * 0.75f, 1f));
            StartCoroutine(FadeOutEffect(Scene2, 0f, 1f));
        }
        else if( currentPhase==Phase.manyPlanet)
        {
            currentPhase = Phase.firstSun;
            StartCoroutine(FadeInEffect(Scene5, musicInitVolume, 1.5f));
            StartCoroutine(FadeOutEffect(Scene4, musicInitVolume * 0.75f, 3f));
            StartCoroutine(FadeOutEffect(Scene3, 0f, 3f));
        }
        else if( currentPhase==Phase.firstSun)
        {
            currentPhase = Phase.manySun;
            StartCoroutine(FadeInEffect(Scene6, musicInitVolume, 1.5f));
            StartCoroutine(FadeInEffect(Scene7, musicInitVolume, 2f));
            StartCoroutine(FadeOutEffect(Scene5, 0f, 3f));
            StartCoroutine(FadeOutEffect(Scene4, 0f, 3f));
        }
        else if(currentPhase==Phase.manySun)
        {
            currentPhase = Phase.firstBlackHole;
            StartCoroutine(FadeInEffect(Scene8, musicInitVolume, 1.5f));
            StartCoroutine(FadeInEffect(Scene9, musicInitVolume, 2f));
            StartCoroutine(FadeOutEffect(Scene6, 0f, 1f));
            StartCoroutine(FadeOutEffect(Scene7, 0f, 1f));
        }
        else if(currentPhase==Phase.firstBlackHole)
        {
            currentPhase = Phase.manyBlackHole;
        }
        else if(currentPhase==Phase.manyBlackHole)
        {
            currentPhase = Phase.end;
        }
    }

    public bool MatchPhase(Phase toTest)
    {
        return toTest == currentPhase;
    }

    public IEnumerator FadeInEffect(AudioSource piste, float volume, float timeModif)
    {
        while (piste.volume < volume)
        {
            piste.volume += Time.deltaTime / timeModif;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator FadeOutEffect(AudioSource piste, float volumeToGo, float timeModif)
    {
        while (piste.volume > volumeToGo)
        {
            piste.volume -= Time.deltaTime / timeModif;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public enum Phase
    {
        nothing,
        firstDust,
        manyDust,
        firstPlanet,
        manyPlanet,
        firstSun,
        manySun,
        firstBlackHole,
        manyBlackHole,
        end
    }
}
