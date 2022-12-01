using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rig;
    RaycastHit hit;
    public bool freezeRotation = true;
    public int forceConst = 4;
    private bool canJump;
    public Animator playerAnimator;
    public Transform target;
    PlayerInventory playerInventory = null;

    void Start()
    {
        Ini();
        playerInventory = GetComponent<PlayerInventory>();
    }

     void Update()
     {
        // Raycast (doesn't affect gameplay)
        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.cyan);
        }
        // Jump Action
        if (Input.GetKeyUp(KeyCode.Space))
        {          
            canJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<GravityBody>().enabled = false;
            GetComponent<JetPack>().StartJetpack();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            GetComponent<GravityBody>().enabled = true;
        }
     }

        void FixedUpdate()
        {
            Move();
            //Jump();
        }

        /* Some initializations
         */
        private void Ini()
        {
            rig.useGravity = false; // Disables Gravity
            if (freezeRotation)
            {
                rig.constraints = RigidbodyConstraints.FreezeRotation;
            }
            else
            {
                rig.constraints = RigidbodyConstraints.None;
            }
        }

        /* Character jump
         */
        public void Jump()
        {
            canJump = true;
            if (canJump)
            {
                canJump = false;
                playerAnimator.SetBool("Jump", true);
                rig.AddRelativeForce(0, forceConst, 0, ForceMode.Impulse);
            }
        }
    public FixedJoystick fixedJoystick;
    public Rigidbody rb;
    public float speed;
    /* Character movement
     */
    private void Move()
    {
#if UNITY_EDITOR
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            playerAnimator.SetBool("Run", true);
            Vector3 pos = new Vector3(rig.gameObject.transform.position.x, rig.gameObject.transform.position.y, rig.gameObject.transform.position.z);
        }
        else playerAnimator.SetBool("Run", false);
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
#endif
#if ANDROID
        //Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        //rig.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        //Debug.Log(target.position - transform.position);
        //if (fixedJoystick.Horizontal != 0 || fixedJoystick.Vertical != 0)
        //{
            var x_1 = fixedJoystick.Horizontal * Time.deltaTime * 150.0f;
            var z_1 = fixedJoystick.Vertical * Time.deltaTime * 3.0f;
            if (fixedJoystick.Horizontal != 0 || fixedJoystick.Vertical != 0)
            {
                playerAnimator.SetBool("Run", true);
                Vector3 pos = new Vector3(rig.gameObject.transform.position.x, rig.gameObject.transform.position.y, rig.gameObject.transform.position.z);
            }
            else playerAnimator.SetBool("Run", false);
            transform.Rotate(0, x_1, 0);
            transform.Translate(0, 0, z_1);
        //}
#endif
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            Debug.Log("Grounded");
            playerAnimator.SetBool("Jump", false);
        }

        /*if(collision.gameObject.tag.Equals("Absorb"))
        {
            Debug.Log("Absorb");
            playerInventory.AddSphere(1, 1);
        }*/
    }
}