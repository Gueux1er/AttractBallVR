using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParameterManager : MonoBehaviour
{
    public static ParameterManager Instance;

    public enum ActionType
    {
        Translation, Force
    }
    [Header("Gameplay parameters")]
    public ActionType forceType = ActionType.Translation;
    public float handRadius = 8f;

    public float attractionForce = -10f;
    public float attractionSpeed = 1.0f;

    public float repulsionForce = 8f;
    public float repulsionSpeed = 1.0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick) || Input.GetKeyDown(KeyCode.S)) // suivant B
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            Debug.Log(SceneManager.sceneCount);
            if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)) // precedent Y
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
            if (nextSceneIndex >= 0)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
    }
}
