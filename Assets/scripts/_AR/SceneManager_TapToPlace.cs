using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.XR.ARFoundation;
using DG.Tweening;

public class SceneManager_TapToPlace : MonoBehaviour
{
    [System.Serializable]public enum TapToPlace_StateInno { SCANNING, PLACING, GETTING_READY, EXPERIENCING };
    public TapToPlace_StateInno state = TapToPlace_StateInno.SCANNING;
    private ExperienceType selectedExperience;

    [Header("Church")]
    public GameObject churchContainer;
    public Animator churchAnimator;

    [Header("Placeholder Altar")]
    public Transform altarPiece;
    public float altarPieceOffsetHeight;
    public GameObject altarBase_Florence;
    public GameObject altarBase_Elsewhere;

    [Header("ScannerEffect")]
    public Transform scannerEffectOrigin;
    [SerializeField] private ScannerEffectDemo scannerEffectScrip;
    [SerializeField] private Material paintingMat;
    [SerializeField] private float paintingNum = 1.2f;

    [Header("UI Objects")]
    public Text alertText;
    public Text instructionsText;
    public CanvasGroup alertCanvas;
    public CanvasGroup instructionsCanvas;
    public CanvasGroup scanGifCanvas;
    public CanvasGroup helpCanvas;

    [Header("UI Strings - Florence")]
    public string scanningAlert_Florence;
    public string scanningInstruction_Florence;
    public string placingAlert_Florence;
    public string placingInstruction_Florence;
    public string gettingReadyAlert_Florence;
    public string gettingReadyInstruction_Florence;

    [Header("UI Strings - Elsewhere")]
    public string scanningAlert_Elsewhere;
    public string scanningInstruction_Elsewhere;
    public string placingAlert_Elsewhere;
    public string placingInstruction_Elsewhere;
    public string gettingReadyAlert_Elsewhere;
    public string gettingReadyInstruction_Elsewhere;


    [Header("AR Object")]
    public GameObject focusSquare;
    public GameObject focusSquareFocused;
 //   [SerializeField] public UnityARGeneratePlane generatePlaneScrip;
    [SerializeField] ARPlaneManager planeManager;

    [SerializeField] GameObject hotspots;
    [SerializeField] private ScannerEffectDemo shader;


    private void Start()
    {
        alertCanvas.alpha = instructionsCanvas.alpha = scanGifCanvas.alpha = helpCanvas.alpha = 0;
        selectedExperience = AppManager.Instance.GetExperienceType();
        churchAnimator.SetTrigger("go");
        setExperienceState(TapToPlace_StateInno.SCANNING);
    }

    private void Update()
    {
        // Update scanner effect material variable
        paintingMat.SetFloat("_Level", paintingNum);

        if (Input.GetMouseButtonDown(0) && !isPointerOverUIObject())
        {
            if (state == TapToPlace_StateInno.PLACING)
            {
                setExperienceState(TapToPlace_StateInno.GETTING_READY);

                //setExperienceState(TapToPlace_StateInno.EXPERIENCING); // Jake

            } else if (state == TapToPlace_StateInno.GETTING_READY)
            {
                setExperienceState(TapToPlace_StateInno.EXPERIENCING);
            }
        } else if (state == TapToPlace_StateInno.SCANNING && planeManager.trackables.count>0)// generatePlaneScrip.hasGeneratedPlanes())
        {
            setExperienceState(TapToPlace_StateInno.PLACING);
        }

        // Update scanner effect origin position
        if (selectedExperience == ExperienceType.ELSEWHERE && scannerEffectOrigin.position != altarPiece.position)
        {
            scannerEffectOrigin.position = altarPiece.position;
        }
    }

    public void setExperienceState(TapToPlace_StateInno newState)
    {
        Text alert = alertText.GetComponent<UnityEngine.UI.Text>();
        Text instructions = instructionsText.GetComponent<UnityEngine.UI.Text>();
        bool isFlorence = selectedExperience == ExperienceType.FLORENCE;
        switch (newState)
        {
            case TapToPlace_StateInno.SCANNING:
				this.state = newState;
                alert.text = isFlorence ? scanningAlert_Florence : scanningAlert_Elsewhere;
                instructions.text = isFlorence ? scanningInstruction_Florence : scanningInstruction_Elsewhere;
				StartCoroutine(fadeIn(alertCanvas, 1f));
                //StartCoroutine(fadeIn(scanGifCanvas, 2f));
				StartCoroutine(fadeIn(instructionsCanvas, 2f));
                StartCoroutine(fadeOut(alertCanvas, 5f));
                altarBase_Florence.SetActive(false);
                altarBase_Elsewhere.SetActive(false);
                focusSquare.SetActive(true);
                break;

            case TapToPlace_StateInno.PLACING:
				this.state = newState;
                alert.text = isFlorence ? placingAlert_Florence : placingAlert_Elsewhere;
                instructions.text = isFlorence ? placingInstruction_Florence : placingInstruction_Elsewhere;
                //scanGifCanvas.gameObject.SetActive(false);
                //StartCoroutine(fadeOut(scanGifCanvas, 0f));
                StartCoroutine(fadeIn(alertCanvas, 0f));
                StartCoroutine(fadeOut(alertCanvas, 6f));
                break;

            case TapToPlace_StateInno.GETTING_READY:
                if (!placeAltarPiece()) break;
                this.state = newState;
                alert.text = isFlorence ? gettingReadyAlert_Florence : gettingReadyAlert_Elsewhere;
                instructions.text = isFlorence ? gettingReadyInstruction_Florence : gettingReadyInstruction_Elsewhere; ;
                //StartCoroutine(fadeOut(scanGifCanvas, 0f));
                StartCoroutine(fadeIn(alertCanvas, 0f));
                StartCoroutine(fadeOut(alertCanvas, 6f));

                // Jake added
                //StartCoroutine(fadeOut(instructionsCanvas, 0f));

                

                if (selectedExperience == ExperienceType.FLORENCE)
                {
                    altarBase_Florence.SetActive(true);
                } else
                {
                    altarBase_Elsewhere.SetActive(true);
                }
                planeManager.subsystem.Stop();
                focusSquare.SetActive(false);

                //setExperienceState(TapToPlace_StateInno.EXPERIENCING); // Jake

                break;

            case TapToPlace_StateInno.EXPERIENCING:
				StartCoroutine(fadeOut(instructionsCanvas, 0f));
                hotspots.SetActive(true);
				this.state = newState;
				startExperience();
                break;
        }
    }

    public void StartAltarAnim()
    {
        if (selectedExperience == ExperienceType.FLORENCE)
        {
            altarBase_Florence.SetActive(true);
        }
        else
        {
            altarBase_Elsewhere.SetActive(true);
        }
        churchAnimator.SetTrigger("go");
    }

    private bool placeAltarPiece()
    {
        if (!focusSquareFocused.activeSelf)
		{
			return false;
		}

        // Move church to focusSquare position
        churchContainer.transform.position = new Vector3(
            focusSquareFocused.transform.position.x,
            focusSquareFocused.transform.position.y + altarPieceOffsetHeight,
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

        StartCoroutine(showPainting());

		return true;
    }

    private void startExperience()
    {
        StartCoroutine(startScannerEffect());
    }

    public void handleHelpButtonPress()
    {
        StartCoroutine(fadeIn(helpCanvas, 0));
    }
    public void handleCloseHelpButtonPress()
    {
        StartCoroutine(fadeOut(helpCanvas, 0));
    }

    IEnumerator showPainting()
    {
        Debug.Log("showingPainting");
        while (paintingNum > -0.2f)
        {
            paintingNum -= Time.deltaTime * 1.5f;
            yield return null;
        }
        
    }
    
    IEnumerator startScannerEffect()
    {
        Debug.Log("debugging --- go");
        scannerEffectScrip.startPainting();
        churchAnimator.SetTrigger("go");
        yield return new WaitForSeconds(0.1f);
        shader.StartShader();
        scannerEffectScrip.startPainting();
        Debug.Log("debugging --- go end");
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
        //c.gameObject.SetActive(false);
    }
}
