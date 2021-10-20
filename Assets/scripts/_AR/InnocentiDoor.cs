using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnocentiDoor : MonoBehaviour
{
    [SerializeField] GameObject markers;
    [SerializeField] GameObject door;
    [SerializeField] Animator ani;
    [SerializeField] RotateAndScale r;
    [SerializeField] GameObject chapel;

    private void OnEnable()
    {
        ScannerEffectDemo.ScanFinished += EnableHotspots;
    }

    private void EnableHotspots()
    {

        markers.SetActive(true);
        ani.SetTrigger("Open");
    }

    public void HideDoor()
    {
        chapel.SetActive(true);
        r.enabled = false;
      //  door.SetActive(false);
    }

    private void OnDisable()
    {
        ScannerEffectDemo.ScanFinished -= EnableHotspots;
    }
}
