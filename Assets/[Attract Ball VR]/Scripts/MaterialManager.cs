using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;

    [Header("Materials")]
    public Material nonActivableMaterial;
    public Material activableUnderMaterialNoMovables;
    public Material activableUnderMaterial;
    public Material activableOverMaterial;
    public Material activableLinkedMaterial;
    public Material activeMaterial;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
