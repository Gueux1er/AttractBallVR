using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;

    [Header("Area Materials")]
    public MaterialInstance nonActivableMaterial;
    public MaterialInstance activableUnderMaterialNoMovables;
    public MaterialInstance activableUnderMaterial;
    public MaterialInstance activableOverMaterial;
    public MaterialInstance activeMaterial;

    [Header("Hand Materials")]
    public Material attractMaterial;
    public Material repulseMaterial;

    [Serializable]
    public struct MaterialInstance
    {
        public Color innerColor;
        public Color rimColor;
        public float rimWidth;
        public float rimGlow;
    }

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

    public void SetMaterial(Area.ActiveState areaState, Material mat, bool specialCond = false)
    {
        
        switch (areaState)
        {           
            case Area.ActiveState.NON_ACTIVABLE:
                mat.SetColor("_InnerColor", nonActivableMaterial.innerColor);
                mat.SetColor("_RimColor", nonActivableMaterial.rimColor);
                mat.SetFloat("_RimWidth", nonActivableMaterial.rimWidth);
                mat.SetFloat("_RimGlow", nonActivableMaterial.rimGlow);
                break;
            case Area.ActiveState.ACTIVABLE_UNDER:
                if (specialCond)
                {
                    mat.SetColor("_InnerColor", activableUnderMaterialNoMovables.innerColor);
                    mat.SetColor("_RimColor", activableUnderMaterialNoMovables.rimColor);
                    mat.SetFloat("_RimWidth", activableUnderMaterialNoMovables.rimWidth);
                    mat.SetFloat("_RimGlow", activableUnderMaterialNoMovables.rimGlow);
                }
                else
                {
                    mat.SetColor("_InnerColor", activableUnderMaterial.innerColor);
                    mat.SetColor("_RimColor", activableUnderMaterial.rimColor);
                    mat.SetFloat("_RimWidth", activableUnderMaterial.rimWidth);
                    mat.SetFloat("_RimGlow", activableUnderMaterial.rimGlow);
                }
                break;
            case Area.ActiveState.ACTIVABLE_OVER:
                mat.SetColor("_InnerColor", activableOverMaterial.innerColor);
                mat.SetColor("_RimColor", activableOverMaterial.rimColor);
                mat.SetFloat("_RimWidth", activableOverMaterial.rimWidth);
                mat.SetFloat("_RimGlow", activableOverMaterial.rimGlow);
                break;
            case Area.ActiveState.ACTIVE:
                mat.SetColor("_InnerColor", activeMaterial.innerColor);
                mat.SetColor("_RimColor", activeMaterial.rimColor);
                mat.SetFloat("_RimWidth", activeMaterial.rimWidth);
                mat.SetFloat("_RimGlow", activeMaterial.rimGlow);
                break;
        }
    }

    //public IEnumerator PrewarmRend(Renderer rend)
    //{
    //    rend.material = nonActivableMaterial;
    //    yield return new WaitForEndOfFrame();
    //    rend.material = activableUnderMaterialNoMovables;
    //    yield return new WaitForEndOfFrame();
    //    rend.material = activableUnderMaterial;
    //    yield return new WaitForEndOfFrame();
    //    rend.material = activableOverMaterial;
    //    yield return new WaitForEndOfFrame();
    //    rend.material = activableLinkedMaterial;
    //    yield return new WaitForEndOfFrame();
    //    rend.material = activeMaterial;
    //}
}