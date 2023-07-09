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
    public bool isClosedOrOpen; // Conrolled by 'SecurityGuard'
    void Update()
    {
        if (isClosedOrOpen)
        {
            anim.SetBool("IsClosedOrOpen", isClosedOrOpen);
            myCollider.enabled = true;
        } else if (!isClosedOrOpen)
        {
            anim.SetBool("IsClosedOrOpen", isClosedOrOpen);
            myCollider.enabled = false;
        }
    }
}
