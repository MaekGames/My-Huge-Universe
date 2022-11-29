using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    private AttractorPlanet attractor;
    private Transform m_Transform;
    private Rigidbody m_Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        attractor = AttractorPlanet.Instance.gameObject.GetComponent<AttractorPlanet>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        m_Rigidbody.useGravity = false;
        m_Transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        attractor.Attract(m_Transform);
    }
}
