
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using DG.Tweening;
using System.Collections;
//using Lean.Touch;
//using Cinemachine;

public class MyPlayerController : MonoBehaviour
{
    public AudioClip nitroSpeed;
    public AudioClip hurt;
    public AudioClip jumpClip;
    public AudioClip bowPickUp;
    public AudioClip quiverPickUp;
    public AudioClip bossCharge;
    public AudioSource backgroundMusic;
    public GameObject BodyMesh;
    public SkinnedMeshRenderer head;
    public GameObject aimSprite;

    private AudioSource playerAudioSource;
    private Rigidbody playerRigidbody;
    public Animator playerAnimator;
    //private LeanDragTranslateRigidbody leanDrag;

    public float moveSpeed = 5f;
    public float jumpForce = 2f;
    public float speedBoost = 2f;

    [HideInInspector]
    public bool startMove = true;
    [HideInInspector]
    public bool isDead = false;

    public TextMeshProUGUI playerLevelText;
    public Image playerLevelGB;
    public float endValue;
    //public Ease easeType;
    public float duration = .2f;
    public float speedBoostTime = 2f;
    public float deathWait = 2f;
    [HideInInspector]
    public int playerLevel;

    private GameObject TPCamera;
    private GameObject FPCamera;
    //private ShootArrow shootArrow;

    //[SerializeField] private CinemachineBrain mainCamera;
    //[SerializeField] private CinemachineVirtualCamera frame0_cam;
    //[SerializeField] private CinemachineVirtualCamera frame1_cam;
    [SerializeField] private GameObject[] frames;

    //Swipe Control
    float damping = 30;
    Vector2 fp;
    Vector2 lp;
    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
        isDead = false;
        startMove = false;
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponentInChildren<Animator>();
        //leanDrag = GetComponent<LeanDragTranslateRigidbody>();
        //playerLevel = GameManager.instance.playerLevel;
        TPCamera = GameObject.FindGameObjectWithTag("TPCamera");
        FPCamera = GameObject.FindGameObjectWithTag("FPCamera");
        //shootArrow = GetComponent<ShootArrow>();

    }

    private void Awake()
    {
        playerAudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bow"))
        {
            playerAudioSource.PlayOneShot(bowPickUp);
        }
        if(other.gameObject.CompareTag("Arrow"))
        {
            playerAudioSource.PlayOneShot(quiverPickUp);
        }
    }
    public void StartGame()
    {
        if (!startMove && !reachEnd)
        {
            startMove = true;
            GameObject.Find("Player/Stickman_heads_sphere").transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            //UIManager.instance.DisableStartPanel();
            //UIManager.instance.ActivateGamePlayPanel();
        }

        //playerLevelText.text = "Lv." + GameManager.instance.playerLevel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead)
            return;

        if (!startMove)
            return;

        playerAnimator.SetBool("Run", true);
        playerRigidbody.velocity = Vector3.Lerp(playerRigidbody.velocity, new Vector3(velocity.x, playerRigidbody.velocity.y, moveSpeed), Time.fixedDeltaTime * 5);
        transform.rotation = Quaternion.LookRotation(new Vector3(playerRigidbody.velocity.x, 0 ,playerRigidbody.velocity.z));

    }

    public float moveToCenterSpeed = 5f;

    private void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
                // Calculate required velocity to arrive in one FixedUpdate
                velocity = (lp - fp) * (Time.deltaTime * damping);
                //Debug.Log("Velocity " + velocity);
                //cachedRigidbody.velocity = Vector2.Lerp(cachedRigidbody.velocity, new Vector3(velocity.x, cachedRigidbody.velocity.y, cachedRigidbody.velocity.z), Time.deltaTime * (damping * 2));

                fp = lp;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                //text1.text = maxVelocity.x.ToString();
                velocity = Vector3.zero;
            }
            else if(touch.phase == TouchPhase.Stationary)
            {
                velocity = Vector3.zero;
            }
        }

        if (!reachEnd)
            return;
        float step = moveToCenterSpeed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, transform.position.y, transform.position.z), step);
   
    }

    private bool reachEnd = false;
    public void StopMoving()
    {
        startMove = false;
        playerAnimator.SetBool("Run", false);
        reachEnd = true;
        playerAudioSource.PlayOneShot(bossCharge);
        //StartCoroutine(StartFade(backgroundMusic, 1f, 0f));
        StartCoroutine(shootBossWait());
    }
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    IEnumerator shootBossWait()
    {
        Debug.Log("Shoot Boss");
        yield return new WaitForSeconds(1f);
        frames[0].SetActive(false);
        frames[1].SetActive(true);
        //frame0_cam.gameObject.SetActive(false);
        //frame1_cam.gameObject.SetActive(true);
        //frame1_cam.transform.parent = transform;
        yield return new WaitForSeconds(0.5f);
        BodyMesh.SetActive(false);
        head.enabled = false;
        aimSprite.SetActive(true);
        //shootArrow.ShootTheBoss();
    }

    public void UpdatePlayerLevelText(int levelNo)
    {
        playerLevel = levelNo;
        playerLevelText.text = "Lv." + levelNo;
        LevelBGAnimate();
    }

    public void ChangePlayerSpeed()
    {
        moveSpeed += speedBoost;
        playerAnimator.speed = 2f;
        playerAudioSource.PlayOneShot(nitroSpeed);
        StartCoroutine(WaitSpeedBoost());
    }

    public void PlayerJump()
    {
        //Debug.Log("Jump");
        playerAudioSource.PlayOneShot(jumpClip);
        playerAnimator.SetTrigger("Jump");
        playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        //leanDrag.enabled = false;
        playerAnimator.applyRootMotion = true;
        playerAnimator.SetTrigger("Death");
        playerAudioSource.PlayOneShot(hurt);
        //UIManager.instance.DisableGameplayPanel();
        //playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        //playerRigidbody.AddForce(-transform.forward * jumpForce, ForceMode.Impulse);
        StartCoroutine(DeathWait());
    }

    IEnumerator WaitSpeedBoost()
    {
        yield return new WaitForSeconds(speedBoostTime);
        moveSpeed -= speedBoost;
        playerAnimator.speed = 1f;
    }

    IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(deathWait);
        //UIManager.instance.ShowLevelFailedUI();
    }

    void LevelBGAnimate()
    {
        //playerLevelGB.transform.DOScale(endValue, duration).SetEase(easeType).OnComplete(ScaleAniationComplete);
    }

    void ScaleAniationComplete()
    {
        //playerLevelGB.transform.DOScale(.6f, duration).SetEase(easeType);
    }

    public bool GetReachEndStatus()
    {
        return reachEnd;
    }   

}
