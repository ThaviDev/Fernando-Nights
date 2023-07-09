using System;
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

    /*
    public static UnlockAreas current;

    private void Awake()
    {
        current = this;
    }

    public event Action onUnlockArea2;
    public void UnlockArea2()
    {
        if (onUnlockArea2 != null) {
            onUnlockArea2();
        }
    }
    public event Action onUnlockArea3;
    public void UnlockArea3()
    {
        if (onUnlockArea3 != null)
        {
            onUnlockArea3();
        }
    }
    public event Action onUnlockArea4;
    public void UnlockArea4()
    {
        if (onUnlockArea4 != null)
        {
            onUnlockArea4();
        }
    }
    public event Action onUnlockArea5;
    public void UnlockArea5()
    {
        if (onUnlockArea5 != null)
        {
            onUnlockArea5();
        }
    }
    public event Action onUnlockArea6;
    public void UnlockArea6()
    {
        if (onUnlockArea6 != null)
        {
            onUnlockArea6();
        }
    }
    */

    void Update()
    {
        
    }

    public void UnlockArea(int ID)
    {
        _TileAreas[ID - 2].SetActive(true);
    }
}
