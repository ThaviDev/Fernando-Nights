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
    [SerializeField]
    private float waitTimeOG; // Tiempo que esta establecido para empezar a esperar para ir a otra posicion

    [SerializeField]
    private float waitTimePos; // Tiempo que falta para que decida ir a otra posición
    [SerializeField]
    private float movementSpeedOG;
    private float presentSpeed;

    private bool isActive;
    [SerializeField]
    private int remnantRequired;
    [SerializeField]
    private bool canAnimate;
    [SerializeField]
    private IntSCOB remnant;
    [SerializeField]
    private AIPath myAIPath;
    [SerializeField]
    private AIDestinationSetter myAIDestinationSetter;
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Animator anim;



    void Start()
    {
        // Tiempo de espera de posicion se establece al inicio
        waitTimePos = waitTimeOG;
        // Al inicio no esta activado
        isActive = false;
        // La velocidad se establece como normal al inicio
        presentSpeed = movementSpeedOG;
        // La velocidad se conecta con la velocidad del pathfinder
        myAIPath.maxSpeed = presentSpeed;
    }

    void Update()
    {
        if (isActive)
        {
            ActiveAnimatronic();
        }
    }

    void ActiveAnimatronic()
    {
        // Logica que controla el movimiento o traslacion de posicion a posicion
        PositionTraversals();

        switch (type)
        {
            case 1:
                AnimatronicTypeOne();
                break;
            case 2:
                AnimatronicTypeTwo();
                break;
            case 3:
                AnimatronicTypeThree();
                break;
            default:
                Debug.LogError("El tipo de personaje es incorrecto");
                break;
        }
    }

    // Animatronico tipo 1 es ir a la puerta e intentar entrar, ir de posicion a posicion, aturdir al guardia si se llega
    void AnimatronicTypeOne() {
        GeneralMovement();
    }
    // Animatronico tipo 2 es tener una barra de paciencia y cuando se desgaste viajar a la oficina
    void AnimatronicTypeTwo() {
        GeneralMovement();
    }
    // Animatronico tipo 3 es poder teletransportarse a la oficina y robar energia hasta que el guardia te saque
    void AnimatronicTypeThree() {
        
    }

    // Logica que se encarga del movimiento de Pathfinder y animacion.
    void GeneralMovement() {
        if (myAIPath.desiredVelocity.x >= 0.01f)
        {
            sprite.flipX = false;
        }
        else if (myAIPath.desiredVelocity.x <= -0.01f)
        {
            sprite.flipX = true;
        }

        if (myAIPath.reachedDestination && canAnimate)
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsMoving", false);
        }
        else if (!myAIPath.reachedDestination && canAnimate)
        {
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsMoving", true);
        }
    }

    // Determines if the destiny was reached, if it was, start waiting to move
    bool destinyReached = false;
    // (Only for type 2) it increases the rate at witch the bar empties 
    float timeDecreaseRate = 1f;
    // Failsafe para que no piense que su posicion llegada fue la anterior
    float failsafeTime;
    // Logica que se encarga de el ir de una posicion del escenario a otra
    void PositionTraversals() {
        myAIDestinationSetter.target = positions[presentPosition];
        if (myAIPath.reachedDestination && !destinyReached && failsafeTime <= 0)
        {
            destinyReached = true;
        }
        if (destinyReached == true)
        {
            waitTimePos = waitTimePos - Time.deltaTime * timeDecreaseRate;
        }

        if (waitTimePos <= 0)
        {
            waitTimePos = waitTimeOG + Random.Range(0.1f,1f);
            presentPosition++;
            failsafeTime = 0.5f;
            destinyReached = false;
        }

        if (failsafeTime > 0)
        {
            failsafeTime = failsafeTime - Time.deltaTime;
        }
    }
    public void OfficeDoorHit()
    {

    }
    public void TriggerSpeed(int speedDivider)
    {
        presentSpeed = movementSpeedOG / speedDivider;
        myAIPath.maxSpeed = presentSpeed;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "TriggerInput" && !isActive)
        {
            if (remnantRequired <= remnant.Value)
            {
                isActive = true;
                remnant.Value -= remnantRequired;
            } else if (remnantRequired > remnant.Value)
            {
                print("No tienes suficiente Remnant");
            }
        } else if (col.tag == "TriggerInput" && isActive)
        {
            // Logica de subir de nivel
        }
    }
}
