using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovingLogic : MonoBehaviour
{
    private float cycle; // This variable increases with time and allows the sine to produce numbers between -1 and 1.
    private Vector3 basePosition; // This variable maintains the location of the object without applying sine changes
    [SerializeField] Transform target_2;
    [SerializeField] Vector2 MinMaxWaveSpeed;
    [SerializeField][Range(0.5f, 5f)] private float waveSpeed = 1f; // Higher make the wave faster
    [SerializeField] Vector2 MinMaxBonusHeight;
    [SerializeField] [Range(1f, 5f)] public float bonusHeight = 2f; // Set higher if you want more wave intensity
    [SerializeField] Vector2 MinMaxSpeed;
    [SerializeField] [Range(0.5f, 5f)] public float speed = 1f; // more value going faster to target
    [SerializeField] Vector2 MinMaxVectorRange;
    private float xAngle, yAngle, zAngle;
    [SerializeField] float scaleValue = 0.1f;
     List<Vector3> vectorList;
    Vector3 moveVector;
    bool scaleUp = false;
    /// <summary>
    /// Rotation variables
    /// </summary>

    Vector3 lastPos;
    Vector3 newPos;
    float t;

    void Start()
    {
        Init();
    }
    private void Init()
    {
        lastPos = transform.eulerAngles;
        waveSpeed = Random.Range(MinMaxWaveSpeed.x, MinMaxWaveSpeed.y);
        bonusHeight = Random.Range(MinMaxBonusHeight.x, MinMaxBonusHeight.y);
        moveVector = new Vector3(Random.Range(MinMaxVectorRange.x, MinMaxVectorRange.y), 1, Random.Range(MinMaxVectorRange.x, MinMaxVectorRange.y));
        speed = Random.Range(MinMaxSpeed.x, MinMaxSpeed.y);
        basePosition = transform.position;
        transform.localScale = new Vector3(1f, 1f, 1f);
        cycle = 0;
    }
    private void UpdateUi()
    {
        MinMaxWaveSpeed.y = waveSpeed;
        MinMaxBonusHeight.y = bonusHeight;
        MinMaxSpeed.y = speed;
    }
    void Update()
    {
        ScaleObj();
        Rotation();
        SinMoveWTowards();
        UpdateUi();
    }
    /// <summary>
    /// Move objects in Sin wave
    /// </summary>
    void SinMoveWTowards()
    {
        cycle += Time.deltaTime * waveSpeed;
        transform.position = basePosition + (moveVector * bonusHeight * Mathf.Sin(cycle));
        if (target_2) basePosition = Vector3.MoveTowards(basePosition, target_2.position, Time.deltaTime * speed);
    }
    /// <summary>
    /// Make objects rotate, change values for better flow 
    /// </summary>
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
    }
    void ScaleObj()
    {
        transform.localScale -= new Vector3(scaleValue, scaleValue, scaleValue);
        if (scaleUp) transform.localScale += new Vector3(scaleValue, scaleValue, scaleValue); else transform.localScale -= new Vector3(scaleValue, scaleValue, scaleValue);
        if (transform.localScale.x < 0.5f) { scaleUp = true; transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); }
        if (transform.localScale.x > 1f) {scaleUp = false; transform.localScale = new Vector3(1f, 1f, 1f);}

    }
    private void OnEnable()
    {
        Init();
    }
    private void OnTriggerEnter(Collider other)
    {
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
}
