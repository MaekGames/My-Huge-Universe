using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteParticle : MonoBehaviour
{
    private void Awake()
    {
        Invoke("deleteMe", 1f);
    }

    private void deleteMe()
    {
        Destroy(gameObject);
    }
}
