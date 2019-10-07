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

    public Phase currentPhase = Phase.nothing;



    public void PlayNextPhase()
    {
        if((int)currentPhase == (int)Phase.nothing)
        {
            ResetMusic();
            currentPhase = Phase.firstDust;
            StartCoroutine(FadeInEffect(Scene1, musicInitVolume, 2f));
            StartCoroutine(FadeOutEffect(Scene12, 0f, 5f));
        }
        else if((int)currentPhase <= (int)Phase.firstDust)
        {
            currentPhase = Phase.manyDust;
            StartCoroutine(FadeInEffect(Scene2, musicInitVolume, 1.5f));
            StartCoroutine(FadeOutEffect(Scene1, musicInitVolume*0.75f, 2f));
        }
        else if((int)currentPhase <= (int)Phase.manyDust)
        {
            currentPhase = Phase.firstPlanet;
            StartCoroutine(FadeInEffect(Scene3, musicInitVolume, 1.5f));
            StartCoroutine(FadeOutEffect(Scene2, musicInitVolume * 0.75f, 2f));
            StartCoroutine(FadeOutEffect(Scene1, 0f, 2f));
        }
        else if((int)currentPhase <= (int)Phase.firstPlanet)
        {
            currentPhase = Phase.manyPlanet;
            StartCoroutine(FadeInEffect(Scene4, musicInitVolume, 1.5f));
            StartCoroutine(FadeOutEffect(Scene3, musicInitVolume * 0.75f, 1f));
            StartCoroutine(FadeOutEffect(Scene2, 0f, 1f));

            Scene1.volume = 0f;
        }
        else if((int)currentPhase <= (int)Phase.manyPlanet)
        {
            currentPhase = Phase.firstSun;
            StartCoroutine(FadeInEffect(Scene5, musicInitVolume, 1.5f));
            StartCoroutine(FadeOutEffect(Scene4, musicInitVolume * 0.75f, 3f));
            StartCoroutine(FadeOutEffect(Scene3, 0f, 3f));

            Scene2.volume = 0f;
            Scene1.volume = 0f;
        }
        else if((int)currentPhase <= (int)Phase.firstSun)
        {
            currentPhase = Phase.manySun;
            StartCoroutine(FadeInEffect(Scene6, musicInitVolume, 1.5f));
            StartCoroutine(FadeInEffect(Scene7, musicInitVolume, 2f));
            StartCoroutine(FadeOutEffect(Scene5, 0f, 3f));
            StartCoroutine(FadeOutEffect(Scene4, 0f, 3f));

            Scene3.volume = 0f;
            Scene2.volume = 0f;
            Scene1.volume = 0f;
        }
        else if((int)currentPhase <= (int)Phase.manySun)
        {
            currentPhase = Phase.firstBlackHole;
            StartCoroutine(FadeInEffect(Scene8, musicInitVolume, 1.5f));
            StartCoroutine(FadeInEffect(Scene9, musicInitVolume, 2f));
            StartCoroutine(FadeOutEffect(Scene6, 0f, 1f));
            StartCoroutine(FadeOutEffect(Scene7, 0f, 1f));


            Scene3.volume = 0f;
            Scene2.volume = 0f;
            Scene1.volume = 0f;
            Scene4.volume = 0f;
            Scene5.volume = 0f;
        }
        else if((int)currentPhase <= (int)Phase.firstBlackHole)
        {
            currentPhase = Phase.end;
            Scene12.Play();
            StartCoroutine(FadeInEffect(Scene12, musicInitVolume, 0.5f));
            StartCoroutine(FadeOutEffect(Scene8, 0f, 5f));
            StartCoroutine(FadeOutEffect(Scene9, 0f, 5f));

            Scene3.volume = 0f;
            Scene2.volume = 0f;
            Scene1.volume = 0f;
            Scene4.volume = 0f;
            Scene5.volume = 0f;
            Scene6.volume = 0f;
            Scene7.volume = 0f;
        }
    }

    public void ForceHolePhase()
    {
        currentPhase = Phase.firstBlackHole;
        StartCoroutine(FadeInEffect(Scene8, musicInitVolume, 1.5f));
        StartCoroutine(FadeInEffect(Scene9, musicInitVolume, 2f));
        StartCoroutine(FadeOutEffect(Scene6, 0f, 1f));
        StartCoroutine(FadeOutEffect(Scene7, 0f, 1f));


        Scene3.volume = 0f;
        Scene2.volume = 0f;
        Scene1.volume = 0f;
        Scene4.volume = 0f;
        Scene5.volume = 0f;
    }


    public bool MatchPhase(Phase toTest)
    {
        return (int)currentPhase<=(int)toTest;
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

    public Phase GetCurrentPhase()
    {
        return currentPhase;
    }

    public void ResetMusic()
    {
        currentPhase = Phase.nothing;
        Scene1.volume = 0f;
        Scene2.volume = 0f;
        Scene3.volume = 0f;
        Scene4.volume = 0f;
        Scene5.volume = 0f;
        Scene6.volume = 0f;
        Scene7.volume = 0f;
        Scene8.volume = 0f;
        Scene9.volume = 0f;
        Scene10.volume = 0f;
        Scene11.volume = 0f;
        Scene1.Stop();
        Scene2.Stop();
        Scene3.Stop();
        Scene4.Stop();
        Scene5.Stop();
        Scene6.Stop();
        Scene7.Stop();
        Scene8.Stop();
        Scene9.Stop();
        Scene10.Stop();
        Scene11.Stop();
        Scene1.Play();
        Scene2.Play();
        Scene3.Play();
        Scene4.Play();
        Scene5.Play();
        Scene6.Play();
        Scene7.Play();
        Scene8.Play();
        Scene9.Play();
        Scene10.Play();
        Scene11.Play();
        Scene12.Play();
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
        end
    }
}
