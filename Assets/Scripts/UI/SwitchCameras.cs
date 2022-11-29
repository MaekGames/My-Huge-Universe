using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameras : MonoBehaviour
{
    public GameObject camera_1;
    public GameObject camera_2;
    public void SwitchCamera()
    {
        if (camera_1.activeSelf) camera_1.SetActive(false);
        else camera_1.SetActive(true);
    }
}
