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
    private EventInstance ambienceWaterInstance;
    private EventInstance ambienceNatureInstance;
    private EventInstance ambienceWindInstance;
    private EventInstance birdInstance;
    private EventInstance frogInstance;
    private EventInstance fireInstance;
    private EventInstance hamsterInstance;
    private EventInstance bellInstance;
    private EventInstance fluidInstance;
    private EventInstance squeekyToyInstance;
    private EventInstance noiseInstance;

    private List<StudioEventEmitter> birdSoundList;
    private List<StudioEventEmitter> frogSoundList;

    public static SoundManager instance;

    public Transform playerTransform;

    private void Awake()
    {
        mainMenuMusicInstance = RuntimeManager.CreateInstance(mainMenuMusic);
        lofiMusicInstance = RuntimeManager.CreateInstance(lofiMusic);
        attractionInstance = RuntimeManager.CreateInstance(attraction);
        repulsionInstance = RuntimeManager.CreateInstance(repulsion);
        enterSnowBallInstance = RuntimeManager.CreateInstance(enterSnowBall);
        ballEnterAreaInstance = RuntimeManager.CreateInstance(ballEnterArea);
        ballExitAreaInstance = RuntimeManager.CreateInstance(ballExitArea);
        successAreaInstance = RuntimeManager.CreateInstance(successArea);
        ambientCaveCricketInstance = RuntimeManager.CreateInstance(ambientCaveCricket);
        ambienceWaterInstance = RuntimeManager.CreateInstance(ambientWater);
        ambienceNatureInstance = RuntimeManager.CreateInstance(ambientNature);
        ambienceWindInstance = RuntimeManager.CreateInstance(ambientWind);
        birdInstance = RuntimeManager.CreateInstance(bird);
        frogInstance = RuntimeManager.CreateInstance(frog);
        fireInstance = RuntimeManager.CreateInstance(fire);
        hamsterInstance = RuntimeManager.CreateInstance(hamster);
        bellInstance = RuntimeManager.CreateInstance(bell);
        fluidInstance = RuntimeManager.CreateInstance(fluid);
        squeekyToyInstance = RuntimeManager.CreateInstance(squeekyToy);
        noiseInstance = RuntimeManager.CreateInstance(noise);

        birdSoundList = new List<StudioEventEmitter>();
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("BirdSound").Length; ++i)
        {
            birdSoundList.Add(GameObject.FindGameObjectsWithTag("BirdSound")[i].GetComponent<StudioEventEmitter>());
        }

        frogSoundList = new List<StudioEventEmitter>();
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("FrogSound").Length; ++i)
        {
            frogSoundList.Add(GameObject.FindGameObjectsWithTag("FrogSound")[i].GetComponent<StudioEventEmitter>());
        }
    }

    private void Update()
    {
        if(playerTransform != null)
        {
            ambienceNatureInstance.setParameterByName("Height", Mathf.Clamp(playerTransform.position.y, 0, 2) / 2);
            ambienceWaterInstance.setParameterByName("Height", Mathf.Clamp(playerTransform.position.y, 0, 2) / 2);
            ambienceWindInstance.setParameterByName("Height", Mathf.Clamp(playerTransform.position.y, 0, 2) / 2);
        }
    }

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

    public void SetParameterLofiMusic(int area, bool rainning)
    {
        lofiMusicInstance.setParameterByName("Area", area);
        lofiMusicInstance.setParameterByName("Rain", rainning ? 1 : 0);
    }

    // Base
    public void StartWindAmbience()
    {
        ambienceWindInstance.start();
    }

    // River
    public void StartWaterAmbience()
    {
        ambienceWaterInstance.start();
    }

    // Grass
    public void StartNatureAmbience()
    {
        ambienceNatureInstance.start();
    }

    // Rain
    public void StopNatureAmbience()
    {
        ambienceNatureInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    // Trees
    public void StartBirdSounds()
    {
        for (int i = 0; i < birdSoundList.Count; ++i)
        {
            birdSoundList[i].Play();
        }
    }

    // Rain
    public void StopBirdSounds()
    {
        for (int i = 0; i < birdSoundList.Count; ++i)
        {
            birdSoundList[i].Play();
        }
    }
    
    // Bridge
    public void StartFrogSounds()
    {
        for (int i = 0; i < frogSoundList.Count; ++i)
        {
            frogSoundList[i].Play();
        }
    }

    public void StopFrogSounds()
    {
        for (int i = 0; i < frogSoundList.Count; ++i)
        {
            frogSoundList[i].Play();
        }
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