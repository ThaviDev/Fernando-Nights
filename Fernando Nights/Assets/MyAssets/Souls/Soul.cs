using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField]
    private IntSCOB remnant;
    private Animator anim;
    public float timeAlive;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        timeAlive = timeAlive - Time.deltaTime;
        if (timeAlive <= 0)
        {
            DieFunction();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            remnant.Value += 20;
            DieFunction();
            gameObject.GetComponent<Collider2D>().enabled = false;
            CanvasManager canvasMan;
            canvasMan = FindObjectOfType<CanvasManager>();
            canvasMan.UpdateText();
        }
    }

    void DieFunction()
    {
        anim.SetTrigger("Die");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
