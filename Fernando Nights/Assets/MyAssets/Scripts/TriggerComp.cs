using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerComp : MonoBehaviour
{
    public int valueDivider;
    public int officeDet; // 0 : its not, 1 : Left, 2 : Right
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PlayerMotor playermot = col.gameObject.GetComponent<PlayerMotor>();
            playermot.TriggerSpeed(valueDivider);
        } else if (col.tag == "Character")
        {
            Character character = col.gameObject.GetComponent<Character>();
            character.TriggerSpeed(valueDivider);
        }
    }
}
