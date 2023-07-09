using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    private float axisX;// El valor X del jugador
    private float axisY;// El valor Y del jugador
    //public float axisIntervention = 0.6f; // El valor de desviación para que caminar en diagonal quede bien
    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private IntSCOB remnantAmount;
    [SerializeField]
    private IntSCOB keysAmount;

    [SerializeField]
    private GameObject triggerInput;


    void Start()
    {
        remnantAmount.Value = 0;
        keysAmount.Value = 0;
        anim = this.gameObject.transform.Find("Player visual").gameObject.GetComponent<Animator>();
        sprite = this.gameObject.transform.Find("Player visual").gameObject.GetComponent<SpriteRenderer>();
        triggerInput = this.gameObject.transform.Find("Trigger Input").gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            triggerInput.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Space)){
            triggerInput.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        //Axis de movimiento
        axisX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxisRaw("Vertical");
        Vector3 mov = new Vector3(axisX, axisY, 0f);

        //transform.position = Vector3.MoveTowards(transform.position, transform.position + mov, movementSpeedIG * Time.deltaTime );
        rb.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y) + mov, movementSpeed * Time.deltaTime));

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
}
