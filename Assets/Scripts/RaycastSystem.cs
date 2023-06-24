using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSystem : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    private void Update()
    {
        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log(hit.collider.gameObject.name);

            if (hit.collider.gameObject.name == "Gloves" && Input.GetKeyDown(KeyCode.E))
            {
                ControllerPlayer.glovesOn = true;
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
