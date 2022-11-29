using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : Fighter
{
    protected Vector3 moveDelta;
    protected Animator anim;
    protected RaycastHit2D hit;
    protected bool isRunning;
    public int[] healthLength = { 5, 10, 15, 25, 30, 50, 70 };
    public float ySpeed = 0.7f;
    public float xSpeed = 0.9f;
    protected float localScaleSize;
    [HideInInspector]
    public bool hasEnemyTarget;
    protected AudioSource audioS;
    protected bool flipx = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        localScaleSize = transform.localScale.x;
        audioS = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        //Debug.Log(moveDelta);
        //moveDelta = new Vector3( xSpeed,  ySpeed,0);
        isRunning = input.x != 0 || input.y != 0 ? true : false;
        if (moveDelta.x != 0)
            Animate(moveDelta.x); 
        if (hit.collider == null)
        {
            //moving the player
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
        else if (hit.collider != null && pushDirection != Vector3.zero)
        {
            pushDirection = Vector3.zero;
        }
    }

    protected virtual void Animate(float x)
    {
        if (gameObject.tag == "Player")
            return;
        moveDelta += pushDirection;
        //reduce pushForce everyframe, based on recoveryspeed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, PushRecoverySpeed);
        if (anim != null)
            anim.SetBool("Run", isRunning);
    }
}
