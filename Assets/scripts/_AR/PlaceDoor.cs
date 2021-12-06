using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PlaceDoor : MonoBehaviour
{
    [SerializeField] ARPlaneManager planes;
    [SerializeField] ARRaycastManager rays;
    bool placed = false;
    bool locked = false;
    [SerializeField] InnocentiDoor chapel;
    InnocentiDoor placedObject;
    [SerializeField] Button b;
    [SerializeField] ScannerEffectDemo effect;

    private void OnEnable()
    {
        ScannerEffectDemo.ScanFinished += HideButton;

    }
    private void Start()
    {
        b.onClick.AddListener(Lock);
      
        HideButton();
        placed = false;
        locked = false;
    }

    private void HideButton()
    {
        b.gameObject.SetActive(false);
    }

    private void Lock()
    {
        locked = true;
        b.gameObject.SetActive(locked);
        effect.ScannerOrigin = placedObject.transform;
        effect.StartShaderWithoutApproval(0);
        effect.StartShader();
        placedObject.HideDoor();
        planes.subsystem.Stop();
        foreach (ARPlane ap in planes.trackables)
        {
            ap.gameObject.SetActive(false);

        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.touches[0];
            if (t.phase == TouchPhase.Began)
            {
                if (!placed)
                {
                    PlaceObject(t);
                }
            }
            if (placed&&!locked)
            {
                if (Input.touchCount == 1)
                {
                    if (t.phase == TouchPhase.Moved)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(t.position);
                        List<ARRaycastHit> hits = new List<ARRaycastHit>();

                        if (rays.Raycast(ray, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                        {
                            placedObject.transform.position = hits[0].pose.position;
                        }
                    }
                }               
            }
        }
        
    }



    void PlaceObject(Touch _t)
    {
        Ray ray = Camera.main.ScreenPointToRay(_t.position);
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        if (rays.Raycast(ray, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            if (chapel)
            {
                if (placedObject)
                {
                    Destroy(placedObject);
                }

                Quaternion rot = Quaternion.identity;
                chapel.transform.position = hits[0].pose.position; 
                chapel.transform.rotation = rot;
                chapel.gameObject.SetActive(true);
                // rot.eulerAngles = Camera.main.transform.eulerAngles + new Vector3(0, 180, 0);
                placedObject = chapel;
                placed = true;
                b.gameObject.SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        ScannerEffectDemo.ScanFinished -= HideButton;
    }
}
