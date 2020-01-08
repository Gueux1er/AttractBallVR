using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using EventInstance = FMOD.Studio.EventInstance;
using RuntimeManager = FMODUnity.RuntimeManager;

public class SoundManager : MonoBehaviour
{
    [EventRef]
    public string mainMenuMusic;
    [EventRef]
    public string lofiMusic;

    [EventRef]
    public string attraction;
    [EventRef]
    public string repulsion;

    [EventRef]
    public string enterSnowBall;
    [EventRef]
    public string ballEnterArea;
    [EventRef]
    public string ballExitArea;
    [EventRef]
    public string successArea;

    [EventRef]
    public string ambientCaveCricket;
    [EventRef]
    public string ambientWater;
    [EventRef]
    public string ambientNature;
    [EventRef]
    public string ambientWind;

    [EventRef]
    public string bird;
    [EventRef]
    public string frog;
    [EventRef]
    public string fire;
    [EventRef]
    public string hamster;
    [EventRef]
    public string bell;
    [EventRef]
    public string fluid;
    [EventRef]
    public string squeekyToy;
    [EventRef]
    public string noise;

    private EventInstance mainMenuMusicInstance;
    private EventInstance lofiMusicInstance;
    private EventInstance attractionInstance;
    private EventInstance repulsionInstance;
    private EventInstance enterSnowBallInstance;
    private EventInstance ballEnterAreaInstance;
    private EventInstance ballExitAreaInstance;
    private EventInstance successAreaInstance;
    private EventInstance ambientCaveCricketInstance;
    private EventInstance ambientWaterInstance;
    private EventInstance ambientNatureInstance;
    private EventInstance ambientWindInstance;
    private EventInstance birdInstance;
    private EventInstance frogInstance;
    private EventInstance fireInstance;
    private EventInstance hamsterInstance;
    private EventInstance bellInstance;
    private EventInstance fluidInstance;
    private EventInstance squeekyToyInstance;
    private EventInstance noiseInstance;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this);
            return;
        }

        mainMenuMusicInstance = RuntimeManager.CreateInstance(mainMenuMusic);
        lofiMusicInstance = RuntimeManager.CreateInstance(lofiMusic);
        attractionInstance = RuntimeManager.CreateInstance(attraction);
        repulsionInstance = RuntimeManager.CreateInstance(repulsion);
        enterSnowBallInstance = RuntimeManager.CreateInstance(enterSnowBall);
        ballEnterAreaInstance = RuntimeManager.CreateInstance(ballEnterArea);
        ballExitAreaInstance = RuntimeManager.CreateInstance(ballExitArea);
        successAreaInstance = RuntimeManager.CreateInstance(successArea);
        ambientCaveCricketInstance = RuntimeManager.CreateInstance(ambientCaveCricket);
        ambientWaterInstance = RuntimeManager.CreateInstance(ambientWater);
        ambientNatureInstance = RuntimeManager.CreateInstance(ambientNature);
        ambientWindInstance = RuntimeManager.CreateInstance(ambientWind);
        birdInstance = RuntimeManager.CreateInstance(bird);
        frogInstance = RuntimeManager.CreateInstance(frog);
        fireInstance = RuntimeManager.CreateInstance(fire);
        hamsterInstance = RuntimeManager.CreateInstance(hamster);
        bellInstance = RuntimeManager.CreateInstance(bell);
        fluidInstance = RuntimeManager.CreateInstance(fluid);
        squeekyToyInstance = RuntimeManager.CreateInstance(squeekyToy);
        noiseInstance = RuntimeManager.CreateInstance(noise);
    }

    /// <summary>
    /// Play this function when the game starts
    /// </summary>
    public void StartMainMenuMusic()
    {
        lofiMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        mainMenuMusicInstance.start();
    }

    public void StartLofiMusic()
    {
        mainMenuMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        lofiMusicInstance.setParameterByName("Area", 0);
        lofiMusicInstance.setParameterByName("Rain", 0);
        lofiMusicInstance.start();
    }

    public void ChangeParameterLofiMusic(int area, bool rainning)
    {
        lofiMusicInstance.setParameterByName("Area", area);
        lofiMusicInstance.setParameterByName("Rain", rainning ? 1 : 0);
    }

    public void StartAttractionSound()
    {
        attractionInstance.start();
    }

    public void StopAttractionSound()
    {
        attractionInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StartRepulsionSound()
    {
        repulsionInstance.start();
    }

    public void StopRepulsionSound()
    {
        repulsionInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}