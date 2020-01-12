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

    public IEnumerator PrewarmRend(Renderer rend)
    {
        rend.material = nonActivableMaterial;
        yield return new WaitForEndOfFrame();
        rend.material = activableUnderMaterialNoMovables;
        yield return new WaitForEndOfFrame();
        rend.material = activableUnderMaterial;
        yield return new WaitForEndOfFrame();
        rend.material = activableOverMaterial;
        yield return new WaitForEndOfFrame();
        rend.material = activableLinkedMaterial;
        yield return new WaitForEndOfFrame();
        rend.material = activeMaterial;
    }
}
