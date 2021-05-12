using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TaptoPlace : MonoBehaviour
{
    public static TaptoPlace instance;
    private GameObject spawnedObject;
    public bool objectSpawned;
    private ARRaycastManager raycastManager;
    private Vector2 touchPosition;
    private MeshRenderer planeVisual;
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private ScannerEffectDemo shaderScript;
    int fingerID = -1;
    private bool touchAllowed = true;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Test for Baking Issues
    [SerializeField] private GameObject objectToEnable;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        raycastManager = GetComponent<ARRaycastManager>();
#if !UNITY_EDITOR
        fingerID = 0;
#endif
    }

    private bool GetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    public bool CheckTrackables()
    {
        if (planeManager.trackables.count > 0)
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        if (!GetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;

        if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon) && objectSpawned == false)
        {
            var hitPose = hits[0].pose;

            if (!objectToEnable.activeInHierarchy)
            {
                objectToEnable.SetActive(true);
                objectToEnable.transform.position = hitPose.position;
                foreach (var trackable in planeManager.trackables)
                {
                    trackable.gameObject.SetActive(false);
                }
                planeManager.enabled = false;
                shaderScript.StartShader();
            }
        }

    }
}