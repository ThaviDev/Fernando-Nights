using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private SpriteRenderer myRenderer;
    [SerializeField]
    private Collider2D myCollider;
    public bool isClosed;
    [SerializeField]
    private IntSCOB keys;
    [SerializeField]
    private int remnantRequired;
    private float isOpen;

    [SerializeField]
    private int IDForAreaUnlock;

    private bool doorTriggered;
    void Start()
    {
        // Encontrar el sprite renderer de la puerta
        myRenderer = this.gameObject.transform.Find("Door Visual").gameObject.GetComponent<SpriteRenderer>();
        //myCollider = GetComponent<Collider>();
    }
    void Update()
    {
        if (isClosed)
        {
            myCollider.enabled = true;
        } else {
            myCollider.enabled = false;
        }

        if (isOpen > 0)
        {
            isOpen = isOpen - Time.deltaTime;
            myRenderer.sprite = Resources.Load<Sprite>("DoorOpened");
            doorTriggered = true;
        }
        else if (doorTriggered)
        {
            myRenderer.sprite = Resources.Load<Sprite>("DoorClosed");
            doorTriggered = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "TriggerInput" && isClosed)
        {
            if (keys.Value > 0)
            {
                print("Se abre la puerta");
                keys.Value--;
                isClosed = false;
                FindObjectOfType<UnlockAreas>().UnlockArea(IDForAreaUnlock);
            } else
            {
                // Mandarle informacion a UI que no se puede porque no hay llaves, puede ser por SCOB o por EVENT
                print("No se puede abrir porque no hay llaves");
            }
        }
        if (col.tag == "Player" || col.tag == "Character")
        {
            isOpen = 1f;
        }
    }
}
