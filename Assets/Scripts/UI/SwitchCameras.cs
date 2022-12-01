using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCameras : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera camera_1;
    public void SwitchCamera()
    {
        if (camera_1.m_Priority >= 9) camera_1.m_Priority = 8;
        else camera_1.m_Priority = 10;
    }
}
