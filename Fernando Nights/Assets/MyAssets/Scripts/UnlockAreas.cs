using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAreas : MonoBehaviour
{
    public GameObject[] _TileAreas = new GameObject[5];
    void Start()
    {
        for(int i = 0; i < _TileAreas.Length; i++)
        {
            _TileAreas[i].SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
