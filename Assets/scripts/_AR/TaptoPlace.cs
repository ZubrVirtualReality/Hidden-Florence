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
    bool once = false;
    bool done = false;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Test for Baking Issues
    [SerializeField] private GameObject objectToEnable;
    [SerializeField] private SceneManager_TapToPlace churchManager;

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

    public void ManualStart()
    {

        //if (!objectToEnable.activeInHierarchy)
        //{
        //    objectToEnable.SetActive(true);
        //    objectToEnable.transform.position = Vector3.zero;
        //    foreach (var trackable in planeManager.trackables)
        //    {
        //        trackable.gameObject.SetActive(false);
        //    }
        //    planeManager.enabled = false;
        //    shaderScript.StartShader();
        //}

        if (!once)
        {
            objectToEnable.SetActive(true);
            shaderScript.StartShaderWithoutApproval(0);
            objectToEnable.transform.position = Vector3.zero;
            once = true;
        }
        else
        {
            shaderScript.StartShader();
            churchManager.StartAltarAnim();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            ManualStart();
        }


        if (!GetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;

        if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon) && objectSpawned == false && !done)
        {
            var hitPose = hits[0].pose;

            //if (once)
            //{
            //    objectToEnable.SetActive(true);
            //    shaderScript.StartShader();
            //    objectToEnable.transform.position = hitPose.position;
            //    foreach (var trackable in planeManager.trackables)
            //    {
            //        trackable.gameObject.SetActive(false);
            //    }
            //    planeManager.enabled = false;
            //}
            //
            //if (churchManager.state == SceneManager_TapToPlace.TapToPlace_State.GETTING_READY && once)
            //{
            //    churchManager.setExperienceState(SceneManager_TapToPlace.TapToPlace_State.EXPERIENCING);
            //    done = true;
            //    //shaderScript.StartShader();
            //    return;
            //}
            //churchManager.setExperienceState(SceneManager_TapToPlace.TapToPlace_State.GETTING_READY);
            //once = true;

            if(!once)
            {
                objectToEnable.SetActive(true);
                shaderScript.StartShaderWithoutApproval(0);
                objectToEnable.transform.position = hitPose.position;
                once = true;
            }
        }
        else if(Input.touchCount > 0 && once)
        {

            shaderScript.StartShader();
            churchManager.StartAltarAnim();     
        }

    }
}