using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondCamera;

    public void ShowMainView()
    {
        mainCamera.enabled = true;
        secondCamera.enabled = false;
    }

    public void ShowSecondView()
    {
        mainCamera.enabled = false;
        secondCamera.enabled = true;
    }
}

