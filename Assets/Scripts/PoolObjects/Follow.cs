using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    //public float minModiffier;
    //public float maxModiffier;
    Vector3 velocity = Vector3.zero;
    //bool isfolowing = false;
    public float chaseSpeed = 5f;
    public float _frequency;
    public float _amplitude;
    public int movespeed;
    [SerializeField] AnimationCurve ColourCurve;

    private float cycle; // This variable increases with time and allows the sine to produce numbers between -1 and 1.
    private Vector3 basePosition; // This variable maintains the location of the object without applying sine changes

    public Transform target_2;
    public Vector2 minmaxWaveSpeed;
    [Range(0f, 5f)] public float waveSpeed = 1f; // Higher make the wave faster
    [Range(0f, 5f)] public float bonusHeight = 1f; // Set higher if you want more wave intensity
    [Range(0f, 5f)] public float speed = 1f; // more value going faster to target
    public float xAngle, yAngle, zAngle;
    public float scaleValue = 0.1f;
    //public void Start() => basePosition = transform.position;
    public List<Vector3> vectorList;
    Vector3 moveVector;
    void SinMoveWTowards()
    {
        cycle += Time.deltaTime * waveSpeed;
        //transform.LookAt(target_2);
        //Vector3 newPosition = transform.localToWorldMatrix * (new Vector3(0, 0, 90));
        //transform.transform.InverseTransformPoint(transform.position);
        //transform.Rotate(new Vector3(0, transform.position.y, 0), Space.World);
        //new Vector(Random.Range(vectorList[Random.Range(0, vectorList.Count)].x,
        //transform.Tra
        //transform.TransformPoint(transform.position);
        //float dir = target_2.position.z - transform.position.z;
        //if (dir > -1 && dir < 1) moveVector = new Vector3(0, 1f, Random.Range(-1.2f, 1.2f)); else moveVector = new Vector3(Random.Range(-1.2f, 1.2f), 1f, 0);
        transform.position =  basePosition + (moveVector * bonusHeight * Mathf.Sin(cycle));
        //Debug.Log(target_2.position - transform.position);
        //transform.TransformPoint(transform.position);
        //transform.position = transform.TransformDirection(transform.position);
        //Debug.Log("InverseVecctor " + transform.position);
        //Debug.Log("InverseDir " + transform.InverseTransformDirection(transform.position));
        //transform.Translate(basePosition + (moveVector * bonusHeight * Mathf.Sin(cycle)), Space.World);
        //Debug.Log("pos " + transform.position);
        //Debug.Log("Inverse " + transform.transform.InverseTransformPoint(transform.position));
        //transform.position = basePosition + (moveVector * bonusHeight * Mathf.Asin(cycle));
        if (target_2) basePosition = Vector3.MoveTowards(basePosition, target_2.position, Time.deltaTime * speed);
    }
    //ublic Transform transform;
    Vector3 lastPos;
    Vector3 newPos;
    float t;
    void ScaleObj()
    {
        transform.localScale -= new Vector3(scaleValue, scaleValue, scaleValue);
        if(transform.localScale.x < 0.5f) transform.localScale = new Vector3(1, 1, 1);
    }
    void Rotation()
    {
        gameObject.transform.eulerAngles = Vector3.Lerp(lastPos, newPos, t);
        t += 0.1f;
        if (t > 1)
        {
            lastPos = newPos;
            newPos = new Vector3(
                     Random.Range(-10f, 10f),
                     Random.Range(0f, 360f),
                     Random.Range(-10f, 10f));
            t = 0;
        }
        //transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
    }

    void Rotation_2()
    {
        Vector3 newDir = Vector3.RotateTowards(transform.forward, target_2.position, 2 * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
    public float radius = 5.0F;
    public float power = 10.0F;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.eulerAngles;

        bonusHeight = Random.Range(1f, bonusHeight);
        waveSpeed = Random.Range(0.5f, waveSpeed);
        //moveVector = vectorList[Random.Range(0, vectorList.Count)];
        //moveVector = new Vector3(Random.Range(-1.2f,1.2f), 1f, 0);
        speed = Random.Range(1f, speed);
        float dir = target_2.position.z - transform.position.z;
        //Debug.Log(dir);
        //if (dir < -1 || dir > 1) moveVector = new Vector3(0, 1f, Random.Range(-1.2f, 1.2f)); else moveVector = new Vector3(Random.Range(-1.2f, 1.2f), 1f, Random.Range(-1.2f, 1.2f));
        Debug.Log(moveVector);
        moveVector = new Vector3(Random.Range(-1.2f, 1.2f), 1f, Random.Range(-1.2f, 1.2f));
        //isfolowing = true;
        basePosition = transform.position;
        //gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * Random.Range(0, 20));
        //gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * Random.Range(0, 20));
        gameObject.GetComponent<Rigidbody>().AddExplosionForce(power, basePosition, radius, 3.0F);
    }

    private void OnEnable()
    {
        basePosition = transform.position;
        cycle = 0;
    }
    public void StartFollowing()
    {
        //isfolowing = true;
    }
    // Update is called once per frame
    void Update()
    {
        Rotation();
        ScaleObj();
        //Rotation_2();
        //MoveSin();
        SinMoveWTowards();
        //if (isfolowing)
        //transform.position = Vector3.SmoothDamp(transform.position, target.position,ref velocity, Time.deltaTime * Random.Range(minModiffier, maxModiffier));
        //transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
        //transform.position += transform.right * Mathf.Sin(Time.time * 3f + target.position.x) * 1f;
        //if (transform.position.y < target.position.y && transform.position.x < target.position.x)
        // {
        //     Finish();
        // }

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigered "+ other.gameObject.tag);
        if (other.gameObject.tag.Equals("Rocket"))
        {
            Finish();
        }
    }
    public void Finish()
    {
        PoolManager.ReleaseObject(this.gameObject);

        //Note: 
        // This takes the gameObject instance, and NOT the prefab instance.
        // Without this call the object will never be available for re-use!
        // gameObject.SetActive(false) is automatically called
    }
    Vector3 pos;
    void MoveSin()
    {
        float x = Mathf.Cos(Time.time * _frequency) * _amplitude;
        float y = Mathf.Sin(Time.time * _frequency) * _amplitude;
        float z = transform.position.z;
        pos += transform.forward * Time.deltaTime * movespeed;

        //transform.position = new Vector3(x, y, z);
        pos += transform.forward * Time.deltaTime * movespeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * _frequency) * _amplitude; ;
    }
}
