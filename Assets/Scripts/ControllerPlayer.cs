using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
    public static bool glovesOn;
    private CameraWalk cameraWalk;
    private void Start()
    {
        cameraWalk = GetComponent<CameraWalk>();
        cameraWalk.enabled = false;
    }

    private void Update()
    {
        if (glovesOn)
        {
            cameraWalk.enabled = true;
        }
    }
}
