using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MovableSound : MonoBehaviour
{
    public StudioEventEmitter enterAreaEvent;
    public StudioEventEmitter exitAreaEvent;

    [ContextMenu("Play Sound Enter Area")]
    public void PlaySoundEnterArea()
    {
        enterAreaEvent.Play();
    }

    [ContextMenu("Play Sound Exit Area")]
    public void PlaySoundExitArea()
    {
        //exitAreaEvent.Play();
    }
}
