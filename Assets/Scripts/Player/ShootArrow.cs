using System.Collections;
using UnityEngine;
using Cinemachine;
public class ShootArrow : MonoBehaviour
{
    public Transform startrayPoint;
    public Transform shootArrowPoint;
    public LayerMask detectLayer;

    public GameObject arrow;
    public GameObject bossArrow;
    public float rayLength = 10f;
    public float nextShootWait = 2f;
    public float startShootWait = .1f;
    private Animator animator;
    private PlayerController player;
    private GameObject enemy;
    private WaitForSeconds shootWaitInSec;
    private bool isShoot = true;
    private WaitForSeconds shootArrowWaitInSce;
    //[SerializeField] private CinemachineBrain mainCamera;
    //[SerializeField] private CinemachineVirtualCamera frame0_cam;
    //[SerializeField] private CinemachineVirtualCamera frame1_cam;
    //[SerializeField] private GameObject[] frames;
    public AudioClip chargeBow;
    public AudioClip bossArrowClip;
    //this is the ui element
    //public RectTransform UI_Element;
    //public RectTransform canvas;
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<PlayerController>();
        shootWaitInSec = new WaitForSeconds(nextShootWait);
        shootArrowWaitInSce = new WaitForSeconds(startShootWait);
    }

    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            if(enemy != null && arrow != null) StartCoroutine(StartShoot(enemy.transform));
        }*/
    }
    private void FixedUpdate()
    {
        //if (player.isDead)
        //    return;
        //if (player.GetReachEndStatus())
        //    rayLength = 0;

        RaycastHit hit;
        if (Physics.Raycast(startrayPoint.position, transform.TransformDirection(Vector3.forward), out hit, rayLength, detectLayer))
        {
            Debug.Log("StartShoot1... " + hit.transform.gameObject.name);
            Debug.DrawRay(startrayPoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            if (isShoot)
            {
                //if(!(hit.transform.GetComponent<EnemyController>().enemyLevel <= player.playerLevel) || hit.transform.GetComponent<EnemyController>().isFinalPointEnemy)
               //     return;
                isShoot = false;

                Debug.Log("StartShoot2... " + hit.transform.gameObject.name);
                StartCoroutine(StartShoot(hit.transform));
                GetComponent<AudioSource>().PlayOneShot(chargeBow);
            }
        }
        else
        {
            Debug.DrawRay(startrayPoint.position, transform.TransformDirection(Vector3.forward) * rayLength, Color.blue);
        }


    }

    IEnumerator StartShoot(Transform hitObj)
    {
        yield return shootArrowWaitInSce;
        ShootTheArrow(hitObj);
    }

    private void ShootTheArrow(Transform hitObj)
    {   
        //if (player.isDead)
        //    return;

        animator.SetTrigger("Shoot");
        GameObject arr = Instantiate(arrow, shootArrowPoint.position, arrow.transform.localRotation);
        arr.GetComponent<Arrow>().target = hitObj;
        StartCoroutine(NextShootArrowWait());
    }

    IEnumerator NextShootArrowWait()
    {
        yield return shootWaitInSec;
        isShoot = true;
    }

    public void ShootTheBoss()
    {
        Instantiate(bossArrow, shootArrowPoint.position, arrow.transform.localRotation);
        slowMotion.Instance.SlowMotion();
        GameObject.Find("Player").GetComponent<AudioSource>().PlayOneShot(bossArrowClip);
        //frames[0].SetActive(false);
        //frames[1].SetActive(true);
        //frame0_cam.gameObject.SetActive(false);
        //frame1_cam.gameObject.SetActive(true);
        StartCoroutine(NextShootArrowWait());
    }
    
}
