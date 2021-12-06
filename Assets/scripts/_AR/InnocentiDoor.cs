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
    [SerializeField] GameObject arrow;

    private void OnEnable()
    {
        ScannerEffectDemo.ScanFinished += EnableHotspots;
        
    }

    private void EnableHotspots()
    {
        arrow.SetActive(false);
        //markers.SetActive(true);
        ani.SetTrigger("Open");
        StartCoroutine(OpenDoor());
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

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(10f);
        markers.SetActive(true);

    }
}
