using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{   
    public AudioClip levelUp;
    public bool isPlayer;
    private Color enemyColor;
    public float speed = 5f;
    public float collideForce = 5f;
    private float destroyWait = 1f;
    private BoxCollider _collider;
    private MeshRenderer _renderer;

    public GameObject particlePrefab;
    public Transform particleSpawn;
    private Transform particleSpawnRotation;

    public Transform target;
    Vector3 targetPos;

    //int counter = 0;
    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _renderer = GetComponentInChildren<MeshRenderer>();
        Invoke("DestroyArrow", 2f);
        particleSpawn = GameObject.Find("Player/Particle Spawn").transform;
        if(isPlayer)
        targetPos = new Vector3(target.position.x, target.position.y + 0.5f, target.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            //transform.LookAt(target.position);
            //transform.position = Vector3.MoveTowards(transform.position, targetPos,  Time.deltaTime * speed);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyController enemyController = other.GetComponent<EnemyController>();
                
                //enemyController.EnemyDead(collideForce);                                                                                                            //The If statement above ^ checks if the player is the same
                //int enemyLevel = enemyController.enemyLevel;                                                                                                        //color as the enemy he just shot, if he is he kills it
                //GameManager.instance.AddLevel(enemyLevel);
                enemyColor = other.gameObject.transform.Find("Stickman_heads_sphere/Stickman_heads_sphere").GetComponent<SkinnedMeshRenderer>().material.color;                                                                   //Get color property of chosen enemy
                GameObject.Find("Player/Stickman_heads_sphere/Stickman_heads_sphere").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", enemyColor);  //Find color property of player
                Instantiate(particlePrefab, particleSpawn.position, particleSpawn.rotation);
                GameObject.Find("Player").GetComponent<AudioSource>().PlayOneShot(levelUp);
                //Debug.Log("Enemy");
                Destroy(_collider);
                _renderer.enabled = false;
                Invoke("DestroyArrow", destroyWait);
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<MyPlayerController>().PlayerDead();
                //Debug.Log("Player");
                Destroy(_collider);
                _renderer.enabled = false;
                Invoke("DestroyArrow", destroyWait);
            }
        }
    }
    void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
