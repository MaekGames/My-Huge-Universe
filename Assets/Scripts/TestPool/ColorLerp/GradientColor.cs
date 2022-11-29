using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientColor : MonoBehaviour
{
    [SerializeField] Gradient albedoTint;
    [SerializeField] AnimationCurve colourCurve;
    [SerializeField] float duration = 5f;

    MaterialPropertyBlock propertyBlock;
    MeshRenderer linkedMR;
    public float _currentTime = 0f;

    void Awake()
    {
        linkedMR = GetComponent<MeshRenderer>();
        propertyBlock = new MaterialPropertyBlock();
    }
    public void SetTime(float currentTime)
    {
        _currentTime = currentTime;
    }
    public void UpdateGradient()
    {
        // update the time
        _currentTime = Mathf.Repeat(_currentTime + Time.deltaTime, duration);
        // retrieve and apply the colour
        var colourProgress = colourCurve.Evaluate(_currentTime / duration);
        var newColour = albedoTint.Evaluate(colourProgress);
        propertyBlock.SetColor("_Color", newColour);
        linkedMR.SetPropertyBlock(propertyBlock);
        //currentTime = Mathf.Repeat(CurrentTime + Time.deltaTime, Duration);
    }
}
