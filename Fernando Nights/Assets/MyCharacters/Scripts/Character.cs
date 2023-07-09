using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private int ID; // Identificacion del personaje
    [SerializeField]
    private int type; // 1 : Door Knocker, 2 : Patience, 3 : Ghost
    [SerializeField]
    private Transform[] positions; // Todas las posiciones que el personaje utiliza para moverse
    private int presentPosition; // El ID de la posicion actual a la que está o va a ir
    private bool isInPos; // Si el personaje esta en ese momento en la posicion deseada de las posiciones del personaje

    private bool isActive;
    private int remnantRequired;
    [SerializeField]
    private IntSCOB remnant;

    void Start()
    {
        
    }

    void Update()
    {
        switch (type)
        {
            case 1: 
                print("Soy 1");
                break;
            case 2:
                print("Soy 2");
                break;
            case 3:
                print("Soy 3");
                break;
            default: 
                Debug.LogError("El tipo de personaje es incorrecto");
                break;
        }
    }
}
