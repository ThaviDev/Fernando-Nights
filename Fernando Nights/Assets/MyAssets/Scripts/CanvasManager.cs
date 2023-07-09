using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public TextTMP remnantText;
    public TextTMP bateryText;
    public TextTMP keysText;

    [SerializeField]
    IntSCOB remnantAmount;
    [SerializeField]
    IntSCOB bateryAmount;
    [SerializeField]
    IntSCOB keysAmount;
    void Start()
    {
        
    }

    void Update()
    {
        // TEMPORALMENTE EN EL UPDATE, UNA VEZ QUE SE ESTABLEZCAN LOS EVENTOS SERA PARTE DE UN EVENTO O VARIOS
        remnantText.UpdateText("Remnant: " + remnantAmount.Value);
        bateryText.UpdateText("Batery: " + bateryAmount.Value);
        keysText.UpdateText("Keys: " + keysAmount.Value);
    }
}
