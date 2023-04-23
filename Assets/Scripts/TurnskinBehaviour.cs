using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnskinBehaviour : MonoBehaviour
{
    public GameObject SelectedShape { get; private set; }

    void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            var hittedObject = hit.transform.gameObject;
            var hittedObjectOutlineComponent = hittedObject.GetComponent<Outline>();
            var isHittedObjectForTurnskining = hittedObjectOutlineComponent != null;

            if (hittedObject == SelectedShape)
            {
                return;
            }

            DisableSelectedShapeOutlining();
            SelectedShape = null;

            if (!isHittedObjectForTurnskining)
            {
                return;
            }

            SelectedShape = hittedObject;
            hittedObjectOutlineComponent.enabled = true;

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log($"Did Hit the {hit.transform.gameObject.name}");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            DisableSelectedShapeOutlining();
            SelectedShape = null;
            Debug.Log($"Did Hit nothing");
        }
    }

    private void DisableSelectedShapeOutlining()
    {
        if (SelectedShape != null)
        {
            SelectedShape.GetComponent<Outline>().enabled = false;
        }
    }
}
