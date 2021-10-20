using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class OutlinePlacement : MonoBehaviour
{
    bool placed = false;
    [SerializeField] GameObject outline;
    [SerializeField] GameObject arObject;
    [SerializeField] GameObject arMarkers;
    [SerializeField] Animator ani;

    private void OnEnable()
    {
        ARPlacementLock.HoldFinished += PlaceDoor;
    }

    private void OnDisable()
    {
        ARPlacementLock.HoldFinished -= PlaceDoor;
    }

    private void PlaceDoor()
    {
        if (!placed)
        {
            outline.SetActive(false);
            arObject.transform.parent = null;

            arObject.AddComponent<ARAnchor>();

            arObject.SetActive(true);
            arMarkers.SetActive(true);
            ani.SetTrigger("Open");
            

            placed = true;
        }
    }   
}

