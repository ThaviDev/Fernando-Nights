using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeDoor : MonoBehaviour
{
    [SerializeField]
    private Collider2D myCollider;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private FloatSCOB Batery;
    public bool isClosedOrOpen; // Conrolled by 'SecurityGuard'

    public bool isForced; // If a type 2 is forcing the door open
    public float forcedTime; // Tiempo que la puerta estara forzada abierta

    private void Start()
    {
        Batery.Value = 100;
    }

    void Update()
    {
        if (isClosedOrOpen && Batery.Value > 0)
        {
            anim.SetBool("IsClosedOrOpen", isClosedOrOpen);
            myCollider.enabled = true;
            Batery.Value = Batery.Value - Time.deltaTime/10;
        }
        else if (!isClosedOrOpen || Batery.Value <= 0)
        {
            anim.SetBool("IsClosedOrOpen", false);
            myCollider.enabled = false;
        }

        if (isForced)
        {
            forcedTime = forcedTime - Time.deltaTime;
            ForcingTheDoor();
        }
        if (forcedTime < 0)
        {
            isForced = false;
        }
    }

    public void ForcingTheDoor()
    {
        isClosedOrOpen = false;
    }
}
