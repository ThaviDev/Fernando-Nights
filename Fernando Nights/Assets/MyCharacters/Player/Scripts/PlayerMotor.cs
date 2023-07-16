using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private float movementSpeedOG;
    private float presentSpeed;
    private float axisX;// El valor X del jugador
    private float axisY;// El valor Y del jugador
    //public float axisIntervention = 0.6f; // El valor de desviación para que caminar en diagonal quede bien
    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField]
    private Rigidbody2D rb;
    public bool canActivateFoxy;
    public bool canActivatePuppet;

    public float foxyDecreaseRate;
    public float puppetDecreaseRate;

    [SerializeField]
    private IntSCOB remnantAmount;
    [SerializeField]
    FloatSCOB foxyPatience;
    [SerializeField]
    FloatSCOB musicBox;

    [SerializeField]
    private GameObject triggerInput;

    [SerializeField]
    IntSCOB charsInRight;
    [SerializeField]
    IntSCOB charsInLeft;

    Vector3 myInitialPos;

    void Start()
    {
        myInitialPos = this.gameObject.transform.position;
        print(myInitialPos);
        presentSpeed = movementSpeedOG;
        remnantAmount.Value = 0;
        anim = this.gameObject.transform.Find("Player visual").gameObject.GetComponent<Animator>();
        sprite = this.gameObject.transform.Find("Player visual").gameObject.GetComponent<SpriteRenderer>();
        triggerInput = this.gameObject.transform.Find("Trigger Input").gameObject;

        foxyPatience.Value = 100f;
        musicBox.Value = 100f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            triggerInput.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Space)){
            triggerInput.SetActive(false);
        }

        if (canActivateFoxy)
        {
            if (foxyPatience.Value > 0)
                foxyPatience.Value = foxyPatience.Value - Time.deltaTime * foxyDecreaseRate;
        }
        if (canActivatePuppet)
        {
            if (musicBox.Value > 0)
                musicBox.Value = musicBox.Value - Time.deltaTime * puppetDecreaseRate;
        }
        if (musicBox.Value > 100)
        {
            musicBox.Value = 100;
        }
        if (foxyPatience.Value > 100)
        {
            foxyPatience.Value = 100;
        }
    }

    void FixedUpdate()
    {
        //Axis de movimiento
        axisX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxisRaw("Vertical");
        Vector3 mov = new Vector3(axisX, axisY, 0f);

        //transform.position = Vector3.MoveTowards(transform.position, transform.position + mov, movementSpeedIG * Time.deltaTime );
        rb.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y) + mov, presentSpeed * Time.deltaTime));

        if (Mathf.Abs(axisX) > 0.1 || Mathf.Abs(axisY) > 0.1)
        {
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsMoving", false);
        }

        if (axisX < 0)
        {
            sprite.flipX = true;
        }
        else if (axisX > 0)
        {
            sprite.flipX = false;
        }
    }
    public void TriggerSpeed(int speedDivider)
    {
        presentSpeed = movementSpeedOG / speedDivider;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "OfficeDoor")
        {
            transform.position = myInitialPos;
            TriggerSpeed(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Guard")
        {
            CanvasManager canvas = FindObjectOfType<CanvasManager>();
            canvas.WinGame();
        }
    }
}
