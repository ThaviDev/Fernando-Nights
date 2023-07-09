using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject soulPrefab;
    private GameObject[] mySoulPresent = new GameObject[3];
    [SerializeField]
    private int AliveTime;
    [SerializeField]
    private int DeadTime;
    private float currentTimer;
    private bool isAlive;

    [SerializeField]
    private bool isVerticalOrHorizontal;

    private void Start()
    {
        isAlive = false;
    }
    bool a;
    private void Update()
    {
        currentTimer = currentTimer - Time.deltaTime;
        if (isAlive && a)
        {
            currentTimer = AliveTime + Random.Range(-3,3);
            a = false;
            Spawn();
        }
        else if (!isAlive && a)
        {
            currentTimer = DeadTime + Random.Range(-3, 3);
            a = false;
        }

        if (currentTimer <= 0)
        {
            isAlive = !isAlive;
            a = true;
        }

    }

    void Spawn()
    {
        mySoulPresent[0] = Instantiate(soulPrefab, transform.position, transform.rotation);
        if (!isVerticalOrHorizontal)
        {
            mySoulPresent[1] = Instantiate(soulPrefab, transform.position + new Vector3(1.5f, 0, 0), transform.rotation);
            mySoulPresent[2] = Instantiate(soulPrefab, transform.position + new Vector3(-1.5f, 0, 0), transform.rotation);
        } else if (isVerticalOrHorizontal)
        {
            mySoulPresent[1] = Instantiate(soulPrefab, transform.position + new Vector3(0f, 1.5f, 0), transform.rotation);
            mySoulPresent[2] = Instantiate(soulPrefab, transform.position + new Vector3(0f, -1.5f, 0), transform.rotation);
        }


        for (int i = 0; i < 3; i++)
        {
            mySoulPresent[i].GetComponent<Soul>().timeAlive = currentTimer;
        }
    }
}
