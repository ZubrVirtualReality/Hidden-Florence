﻿// Written By : Thomas Harrison
// Date : 07/06/2021
// Description: Tweaked and expanded SceneManager_TapToPlace for the innocenti scene
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.XR.ARFoundation;
using DG.Tweening;

public class SceneManager_TapToPlace_Innocenti : MonoBehaviour
{
    public static SceneManager_TapToPlace_Innocenti instance;
    [System.Serializable]public enum TapToPlace_State { SCANNING, PLACING, GETTING_READY, EXPERIENCING };
    public TapToPlace_State state = TapToPlace_State.SCANNING;
    private ExperienceType selectedExperience;

    [Header("Church")]
    public GameObject churchContainer;
    public Animator churchAnimator;

    [Header("UI Objects")]
    public Text alertText;
    public Text instructionsText;
    public CanvasGroup alertCanvas;
    public CanvasGroup instructionsCanvas;
    public CanvasGroup scanGifCanvas;
    public CanvasGroup helpCanvas;

    [Header("UI Strings - Elsewhere")]
    public string scanningAlert_Elsewhere;
    public string scanningInstruction_Elsewhere;
    public string placingAlert_Elsewhere;
    public string placingInstruction_Elsewhere;
    public string gettingReadyAlert_Elsewhere;
    public string gettingReadyInstruction_Elsewhere;

    [Header("ScannerEffect")]
    public Transform scannerEffectOrigin;
    [SerializeField] private ScannerEffectDemo scannerEffectScrip;
    [SerializeField] private Material paintingMat;
    [SerializeField] private float paintingNum = 1.2f;

    [Header("AR Object")]
    public GameObject focusSquare;
    public GameObject focusSquareFocused;
    [SerializeField] ARPlaneManager planeManager;

    //[SerializeField] GameObject hotspots;
    [SerializeField] private ScannerEffectDemo shader;
    private bool transitioned = false;


    private void Start()
    {
        instance = this;
        alertCanvas.alpha = instructionsCanvas.alpha = scanGifCanvas.alpha = helpCanvas.alpha = 0;
        selectedExperience = AppManager.Instance.GetExperienceType();
        setExperienceState(TapToPlace_State.SCANNING);
    }

    private void Update() // When the ground is scanned move to tapping functionality
    {
        paintingMat.SetFloat("_Level", paintingNum);

        if (state == TapToPlace_State.SCANNING && planeManager.trackables.count>0 && !transitioned)
        {
            transitioned = true;
            setExperienceState(TapToPlace_State.PLACING);
        }      
    }

    public void setExperienceState(TapToPlace_State newState) // Called to change the current state, Fades in and out relevant instructions
    {
        Text alert = alertText.GetComponent<UnityEngine.UI.Text>();
        Text instructions = instructionsText.GetComponent<UnityEngine.UI.Text>();
        bool isFlorence = selectedExperience == ExperienceType.FLORENCE;
        switch (newState)
        {
            case TapToPlace_State.SCANNING:
				this.state = newState;
                alert.text = scanningAlert_Elsewhere;
                instructions.text = scanningInstruction_Elsewhere;
				StartCoroutine(fadeIn(alertCanvas, 1f));
                StartCoroutine(fadeIn(scanGifCanvas, 2f));
				StartCoroutine(fadeIn(instructionsCanvas, 2f));
                StartCoroutine(fadeOut(alertCanvas, 5f));
                focusSquare.SetActive(true);
                break;

            case TapToPlace_State.PLACING:
				this.state = newState;
                alert.text = placingAlert_Elsewhere;
                instructions.text = placingInstruction_Elsewhere;
                StartCoroutine(fadeOut(scanGifCanvas, 0f));
                StartCoroutine(fadeIn(alertCanvas, 0f));
                StartCoroutine(fadeOut(alertCanvas, 6f));
                break;

            case TapToPlace_State.GETTING_READY:
                if (!placeAltarPiece()) break;
                this.state = newState;
                StartCoroutine(fadeOut(scanGifCanvas, 0f));

                planeManager.subsystem.Stop();
                focusSquare.SetActive(false);
                StartCoroutine(fadeOut(instructionsCanvas, 0f));
                //setExperienceState(TapToPlace_State.EXPERIENCING);
                break;

            case TapToPlace_State.EXPERIENCING:

                StartCoroutine(fadeOut(instructionsCanvas, 0f));
                //hotspots.SetActive(true);
				this.state = newState;
                startExperience();
                break;
        }
    }

    public void handleHelpButtonPress()
    {
        StartCoroutine(fadeIn(helpCanvas, 0));
    }
    public void handleCloseHelpButtonPress()
    {
        StartCoroutine(fadeOut(helpCanvas, 0));
    }
    private void startExperience()
    {
        StartCoroutine(startScannerEffect());
    }

    private bool placeAltarPiece() // Places the small altar on the ground before the reveal
    {
        if (!focusSquareFocused.activeSelf)
        {
            return false;
        }

        // Move church to focusSquare position
        churchContainer.transform.position = new Vector3(
            focusSquareFocused.transform.position.x,
            focusSquareFocused.transform.position.y + 0.15f,
            focusSquareFocused.transform.position.z);
        // Rotate church to face Camera
        churchContainer.transform.eulerAngles = new Vector3(
            churchContainer.transform.eulerAngles.x,
            Camera.main.transform.eulerAngles.y,
            churchContainer.transform.eulerAngles.z);

        // IDK ??
        if (paintingNum <= -0.2)
        {
            Debug.Log("debugging -- error starting showPainting because paintingNum <= -0.2");
        }

        return true;
    }

    // UI
    private bool isPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    IEnumerator startScannerEffect()
    {
        Debug.Log("debugging --- go");
        scannerEffectScrip.startPainting();
        yield return new WaitForSeconds(0.1f);
        shader.StartShader();
        scannerEffectScrip.startPainting();
        Debug.Log("debugging --- go end");
    }

    IEnumerator fadeIn(CanvasGroup c, float delay = 0.5f)
    {
        yield return new WaitForSeconds(delay);
        float temp = c.alpha;
        c.gameObject.SetActive(true);
        while (temp < 1)
        {
            temp += Time.deltaTime * 2;
            c.alpha = temp;
            yield return null;
        }
    }
    IEnumerator fadeOut(CanvasGroup c, float delay = 0.5f)
    {
        yield return new WaitForSeconds(delay);
        float temp = c.alpha;
        while (temp > 0)
        {
            temp -= Time.deltaTime * 2;
            c.alpha = temp;
            yield return null;
        }
        c.gameObject.SetActive(false);
    }
}
