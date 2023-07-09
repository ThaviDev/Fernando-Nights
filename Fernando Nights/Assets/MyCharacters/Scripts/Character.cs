using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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
    private bool canAnimate;
    [SerializeField]
    private IntSCOB remnant;
    [SerializeField]
    private AIPath myAIPath;
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float movementSpeedOG;
    private float presentSpeed;


    void Start()
    {
        isActive = false;
        presentSpeed = movementSpeedOG;
        myAIPath.maxSpeed = presentSpeed;
    }

    void Update()
    {
        if (myAIPath.desiredVelocity.x >= 0.01f)
        {
            sprite.flipX = false;
        } else if (myAIPath.desiredVelocity.x <= -0.01f)
        {
            sprite.flipX = true;
        }

        if (myAIPath.reachedDestination && canAnimate)
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsMoving", false);
        } else if (!myAIPath.reachedDestination && canAnimate)
        {
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsMoving", true);
        }


        switch (type)
        {
            case 1: 
                print("Soy 1 voy a puertas y les pego");
                break;
            case 2:
                print("Soy 2 espero a que mi paciencia se baje");
                break;
            case 3:
                print("Soy 3 me teletransporto a su oficina");
                break;
            default: 
                Debug.LogError("El tipo de personaje es incorrecto");
                break;
        }
    }

    void AnimatronicTypeOne() { }
    void AnimatronicTypeTwo() { }
    void AnimatronicTypeThree() { }

    public void TriggerSpeed(int speedDivider)
    {
        presentSpeed = movementSpeedOG / speedDivider;
        myAIPath.maxSpeed = presentSpeed;
    }
}
