using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerComp : MonoBehaviour
{
    public int valueDivider;
    public int officeDet; // 0 : its not, 1 : Left, 2 : Right
    public IntSCOB charAmount;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PlayerMotor playermot = col.gameObject.GetComponent<PlayerMotor>();
            playermot.TriggerSpeed(valueDivider);
            if (officeDet > 0)
                charAmount.Value++;
        } else if (col.tag == "Character")
        {
            Character character = col.gameObject.GetComponent<Character>();
            // Mientras el personaje no sea de paciencia reducir su velocidad
            if (character.type != 2)
            character.TriggerSpeed(valueDivider);
            if (officeDet > 0)
                charAmount.Value++;
        }

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (officeDet > 0)
                charAmount.Value--;
        }
        else if (col.tag == "Character")
        {
            if (officeDet > 0)
                charAmount.Value--;
        }
    }
}
