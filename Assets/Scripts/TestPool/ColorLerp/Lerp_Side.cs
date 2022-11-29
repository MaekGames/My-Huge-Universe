using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp_Side : MonoBehaviour
{
    MeshRenderer[] meshRenderers;
    Attractor[] atractors;

    [SerializeField] [Range(0f, 1f)] float lerptime;
    [SerializeField] Color[] myColors;
    int colorindex = 0;
    float t = 0f;
    int len;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        atractors = GetComponentsInChildren<Attractor>();
        DisableAtractors();
        len = myColors.Length;
    }
    void DisableAtractors()
    {
        foreach (Attractor atractor in atractors)
        {
            atractor.enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, myColors[colorindex], lerptime * Time.deltaTime);
            t = Mathf.Lerp(t, 1F, lerptime * Time.deltaTime);
            if (t > .9f)
            {
                t = 0f;
                colorindex++;
                colorindex = (colorindex >= len) ? 0 : colorindex;
            }
        }
    }
}
