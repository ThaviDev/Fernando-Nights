using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private SpriteRenderer myRenderer;
    [SerializeField]
    private IntSCOB remnant;
    [SerializeField]
    private IntSCOB keys;
    [SerializeField]
    private int remnantAmount = 20;
    private bool isOpen;

    void Start()
    {
        myRenderer = this.gameObject.transform.Find("Chest Visual").gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && isOpen == false)
        {
            myRenderer.sprite = Resources.Load<Sprite>("ChestOpen");
            remnant.Value += remnantAmount;
            keys.Value += 1;
            isOpen = true;
        }
    }
}
