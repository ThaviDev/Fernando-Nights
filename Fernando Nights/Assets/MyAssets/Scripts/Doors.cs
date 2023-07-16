using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Doors : MonoBehaviour
{
    private SpriteRenderer myRenderer;
    [SerializeField]
    private Collider2D myCollider;
    public bool isClosed;
    [SerializeField]
    private int remnantRequired = 80;
    [SerializeField]
    private IntSCOB remnant;
    private float isOpen;

    [SerializeField]
    private int IDForAreaUnlock;

    private bool doorTriggered;

    public TextMeshProUGUI myTextTMP;
    void Start()
    {
        //myTextTMP = transform.Find("CanvasWorld").transform.Find("Text").GetComponent<TextMeshPro>().text;
        // Encontrar el sprite renderer de la puerta
        myRenderer = this.gameObject.transform.Find("Door Visual").gameObject.GetComponent<SpriteRenderer>();
        //myCollider = GetComponent<Collider>();
    }
    void Update()
    {
        if (isClosed)
        {
            myTextTMP.text = ("" + remnantRequired);
            //myTextTMP.UpdateText("" + remnantRequired);
            myCollider.enabled = true;
        } else {
            myTextTMP.text = ("");
            //myTextTMP.UpdateText("");
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
            if (remnant.Value >= remnantRequired)
            {
                print("Se abre la puerta");
                remnant.Value -= remnantRequired;
                isClosed = false;
                FindObjectOfType<UnlockAreas>().UnlockArea(IDForAreaUnlock);
                CanvasManager canvasMan = FindObjectOfType<CanvasManager>();
                canvasMan.UpdateText();
            } else
            {
                print("No hay suficiente remnant");
            }
        }
        if (col.tag == "Player" || col.tag == "Character")
        {
            isOpen = 1f;
        }
    }
}
