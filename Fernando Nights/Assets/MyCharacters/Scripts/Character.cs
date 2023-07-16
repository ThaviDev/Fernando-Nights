using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField]
    private int ID; // Identificacion del personaje
    [SerializeField] private string myName; // Nombre del personaje
    public int type; // 1 : Door Knocker, 2 : Patience, 3 : Ghost
    [SerializeField]
    private Transform[] positions; // Todas las posiciones que el personaje utiliza para moverse
    public int presentPosition; // El ID de la posicion actual a la que está o va a ir
    private bool isInPos; // Si el personaje esta en ese momento en la posicion deseada de las posiciones del personaje (For type 3)
    [SerializeField]
    private float waitTimeOG; // Tiempo que esta establecido para empezar a esperar para ir a otra posicion
    [SerializeField]
    private float waitTimePos; // Tiempo que falta para que decida ir a otra posición
    [SerializeField]
    private float movementSpeedOG;
    private float presentSpeed;

    [SerializeField]
    private SecurityGuard secGuard;
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
    [SerializeField]
    private Collider2D myCol;
    [SerializeField]
    private Rigidbody2D myRB;

    Vector3 myInitialPos;

    [SerializeField]
    IntSCOB charsInRight;
    [SerializeField]
    IntSCOB charsInLeft;

    [SerializeField]
    FloatSCOB foxyPatience;
    [SerializeField]
    FloatSCOB musicBox;

    public TextMeshProUGUI myTextTMP;
    [SerializeField] private int[] remnantsToLevelUp = new int[4] {40, 80, 160, 240};
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private int maxLevel = 4;

    void Start()
    {
        // Mi posicion inicial es en la que spawneo en el juego
        myInitialPos = transform.position;
        // Tiempo de espera de posicion se establece al inicio
        waitTimePos = waitTimeOG;
        // Al inicio no esta activado
        isActive = false;
        // La velocidad se establece como normal al inicio
        presentSpeed = movementSpeedOG;
        // La velocidad se conecta con la velocidad del pathfinder
    }

    void Update()
    {
        myAIPath.maxSpeed = presentSpeed;
        if (type == 2 && presentPosition > 1)
            presentPosition = 1;
        if (isActive)
        {
            if (!(currentLevel >= maxLevel)) {
                myTextTMP.text = ("" + remnantsToLevelUp[currentLevel]);
            }
            else {
                myTextTMP.text = ("");
            }
            ActiveAnimatronic();
        } else
        {
            myTextTMP.text = ("" + remnantRequired);
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

    public void LevelUp()
    {
        switch (currentLevel)
        {
            case 1:
                presentSpeed = movementSpeedOG += 0.2f;
                timeDecreaseRate = 1.1f;
                if (type == 1)
                {
                    myAIPath.pickNextWaypointDist = 0.175f;
                    myAIPath.slowdownDistance = 0.15f;
                    myAIPath.endReachedDistance = 0.25f;
                }
                if (type == 2)
                {
                    if (ID == 2)
                    {
                        PlayerMotor player = FindObjectOfType<PlayerMotor>();
                        player.foxyDecreaseRate += 0.1f;
                    }
                    if (ID == 7)
                    {
                        PlayerMotor player = FindObjectOfType<PlayerMotor>();
                        player.puppetDecreaseRate += 0.1f;
                    }
                }
                break;
            case 2:
                presentSpeed = movementSpeedOG += 0.4f;
                timeDecreaseRate = 1.2f;
                if (type == 1)
                {
                    myAIPath.pickNextWaypointDist = 0.35f;
                    myAIPath.slowdownDistance = 0.2f;
                    myAIPath.endReachedDistance = 0.50f;
                }
                if (type == 2)
                {
                    if (ID == 2)
                    {
                        PlayerMotor player = FindObjectOfType<PlayerMotor>();
                        player.foxyDecreaseRate += 0.1f;
                    }
                    if (ID == 7)
                    {
                        PlayerMotor player = FindObjectOfType<PlayerMotor>();
                        player.puppetDecreaseRate += 0.1f;
                    }
                }
                break;
            case 3:
                presentSpeed = movementSpeedOG += 0.6f;
                timeDecreaseRate = 1.4f;
                if (type == 1)
                {
                    myAIPath.pickNextWaypointDist = 0.525f;
                    myAIPath.slowdownDistance = 0.25f;
                    myAIPath.endReachedDistance = 0.75f;
                }
                if (type == 2)
                {
                    if (ID == 2)
                    {
                        PlayerMotor player = FindObjectOfType<PlayerMotor>();
                        player.foxyDecreaseRate += 0.1f;
                    }
                    if (ID == 7)
                    {
                        PlayerMotor player = FindObjectOfType<PlayerMotor>();
                        player.puppetDecreaseRate += 0.1f;
                    }
                }
                break;
            case 4:
                presentSpeed = movementSpeedOG += 1.2f;
                timeDecreaseRate = 1.8f;
                if (type == 1)
                {
                    myAIPath.pickNextWaypointDist = 0.7f;
                    myAIPath.slowdownDistance = 0.3f;
                    myAIPath.endReachedDistance = 1f;

                }
                                if (type == 2)
                {
                    if (ID == 2)
                    {
                        PlayerMotor player = FindObjectOfType<PlayerMotor>();
                        player.foxyDecreaseRate += 0.1f;
                    }
                    if (ID == 7)
                    {
                        PlayerMotor player = FindObjectOfType<PlayerMotor>();
                        player.puppetDecreaseRate += 0.1f;
                    }
                }
                break;
        }
    }

    // Animatronico tipo 1 es ir a la puerta e intentar entrar, ir de posicion a posicion, aturdir al guardia si se llega
    void AnimatronicTypeOne() {
        GeneralMovement();
    }
    // Animatronico tipo 2 es tener una barra de paciencia y cuando se desgaste viajar a la oficina
    void AnimatronicTypeTwo() {
        if (ID == 2)
        {
            waitTimePos = foxyPatience.Value;
        }
        if (ID == 7)
        {
            waitTimePos = musicBox.Value;
        }
    }
    // it increases the rate at witch the bar empties 
    float timeDecreaseRate = 1f;
    // Animatronico tipo 3 es poder teletransportarse a la oficina y robar energia hasta que el guardia te saque
    void AnimatronicTypeThree() {
        float distance = Vector3.Distance(transform.position, positions[presentPosition].position);
        myAIPath.canMove = false;
        transform.position = positions[presentPosition].position;
        if (distance <= 1 && presentPosition == 0)
            isInPos = false;
        if (distance <= 1 && presentPosition == 1)
        {
            print("Estoy en la oficina yay!!");
            secGuard.batery.Value = secGuard.batery.Value - (Time.deltaTime * timeDecreaseRate) / 10;
            if (!isInPos)
            {
                if (ID == 5)
                {
                    secGuard.phantomIsInOffice[0] = true;
                }
                else if (ID == 6)
                {
                    secGuard.phantomIsInOffice[1] = true;
                }
                else if (ID == 3)
                {
                    secGuard.phantomIsInOffice[2] = true;
                }
                isInPos = true;
            }

        }
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
    // Failsafe para que no piense que su posicion llegada fue la anterior
    float failsafeTime;
    // Logica que se encarga de el ir de una posicion del escenario a otra
    void PositionTraversals() {
        myAIDestinationSetter.target = positions[presentPosition];
        if (myAIPath.reachedDestination && !destinyReached && failsafeTime <= 0)
        {
            destinyReached = true;
        }
        if (destinyReached == true && type != 2)
        {
            waitTimePos = waitTimePos - Time.deltaTime * timeDecreaseRate;
        }

        if (waitTimePos <= 0)
        {
            waitTimePos = waitTimeOG + Random.Range(0.1f,1f);
            presentPosition++;
            if (type == 2)
                presentPosition = 1;
            failsafeTime = 0.5f;
            destinyReached = false;
        }

        if (failsafeTime > 0)
        {
            failsafeTime = failsafeTime - Time.deltaTime;
        }
    }
    public void TriggerSpeed(int speedDivider)
    {
        if (currentLevel == 0) {
            presentSpeed = movementSpeedOG / speedDivider;
        } else if (currentLevel == 1)
        {
            presentSpeed = (movementSpeedOG + 0.2f) / speedDivider;
        } else if (currentLevel == 2)
        {
            presentSpeed = (movementSpeedOG + 0.4f) / speedDivider;
        } else if (currentLevel == 3)
        {
            presentSpeed = (movementSpeedOG + 0.6f) / speedDivider;
        } else if (currentLevel == 4)
        {
            presentSpeed = (movementSpeedOG + 1.2f) / speedDivider;
        }
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
                CanvasManager canvasMan = FindObjectOfType<CanvasManager>();
                PlayerMotor player = FindObjectOfType<PlayerMotor>();
                canvasMan.UpdateText();
                if (ID == 2)
                {
                    canvasMan.ActivateUIFoxy();
                    player.canActivateFoxy = true;
                }
                if (ID == 7)
                {
                    canvasMan.ActivateUIPuppet();
                    player.canActivatePuppet = true;
                }
            } else if (remnantRequired > remnant.Value)
            {
                print("No tienes suficiente Remnant");
            }
        } else if (col.tag == "TriggerInput" && isActive)
        {
            // Logica de subir de nivel
            if (!(currentLevel >= maxLevel) && remnant.Value >= remnantsToLevelUp[currentLevel])
            {
                print("Voy a subir de nivel porque interactuaron conmigo");
                remnant.Value -= remnantsToLevelUp[currentLevel];
                currentLevel++;
                LevelUp();
            }
        }
        if (col.tag == "Guard")
        {
            transform.position = myInitialPos;
            TriggerSpeed(1);
            presentPosition = 0;
            // Si es tipo 1 Stunear al guardia y volver a la posicion original
            if (type == 1)
            {
                //col.gameObject.GetComponent<SecurityGuard>().StunnedInput();
                secGuard.StunnedInput();
                secGuard.UpdateTextAttacked("has been stunned by " + myName);
            }
            // Si es tipo 2 entonces forzar ambas puertas
            if (type == 2)
            {
                // Forzar ambas puertas para que se abran
                //col.gameObject.GetComponent<SecurityGuard>().ForceBothDoors();
                secGuard.ForceBothDoors();
                secGuard.UpdateTextAttacked("is vulnerable " + myName + " has forced both doors");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "OfficeDoor")
        {
            transform.position = myInitialPos;
            TriggerSpeed(1);
            presentPosition = 0;
            if (type == 2)
            {
                // Forzar la puerta para que se abra
                OfficeDoor ofDoor = col.gameObject.GetComponent<OfficeDoor>();
                ofDoor.isForced = true;
                ofDoor.forcedTime = 6;
                if (ID == 2)
                secGuard.UpdateTextAttacked("is vulnerable " + myName + " has forced the left door");
                if (ID == 7)
                secGuard.UpdateTextAttacked("is vulnerable " + myName + " has forced the right door");
            }
            /*
            bool dir = col.gameObject.GetComponent<OfficeDoor>().isRightOrLeft;
            if (dir)
            {
                charsInRight.Value--;
            } else if (!dir)
            {
                charsInLeft.Value--;
            }
            */
        }
    }
}
