using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientObject : MonoBehaviour
{
    [SerializeField] float _currentTime = 0f;
    public float CurrentTime { set => _currentTime = value ; get => _currentTime; }
    public float currentTime = 0f;

    public void SetTime(float CurrentTime)
    {
        currentTime = CurrentTime;
    }
}
