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

    private void Start()
    {
        Batery.Value = 100;
    }

    public bool isClosedOrOpen; // Conrolled by 'SecurityGuard'
    void Update()
    {
        if (isClosedOrOpen && Batery.Value > 0)
        {
            anim.SetBool("IsClosedOrOpen", isClosedOrOpen);
            myCollider.enabled = true;
            Batery.Value = Batery.Value - Time.deltaTime/50;
        }
        else if (!isClosedOrOpen || Batery.Value <= 0)
        {
            anim.SetBool("IsClosedOrOpen", isClosedOrOpen);
            myCollider.enabled = false;
        }
    }
}
