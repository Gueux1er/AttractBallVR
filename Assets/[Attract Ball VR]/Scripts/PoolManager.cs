using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{    
    public GameObject prefabContainer;
    private List<GameObject> poolList = new List<GameObject>();

    public int poolMax;

    void Start()
    {
        for (int i = 0; i < prefabContainer.transform.childCount; i++)
        {
            poolList.Add(prefabContainer.transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        
    }
}
