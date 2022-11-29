using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    private GameObject player;
    private GameObject planet;
    public float speed = 1.0f;
    public Animator enemyAnimator;

    private Vector3 v_diff;
    private float atan2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//PlayerController.Instance.gameObject;
        planet = GameObject.FindGameObjectWithTag("Earth");
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemyAnimator.SetBool("Run", true);
        var step = speed * Time.deltaTime;
        GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(player.transform.position);
        //transform.up = transform.position - player.transform.position;
        //transform.LookAt(player.transform.position, Vector3.up);
        //Vector3 relativePos = player.transform.position - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        //transform.rotation = rotation;
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        //Rottate();
    }

    public void Rottate()
    {
        v_diff = (player.transform.position - transform.position);
        atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg - 90f);
    }

    void LateUpdate()
    {
        //transform.up = transform.position; // this assumes your sphere center is in 0, 0, 0

        // or
        //Transform sphereTransform; // assuming you have set this variable somewhere outside, and its a sphere center
        //transform.up = transform.position - planet.transform.position;
    }
}
