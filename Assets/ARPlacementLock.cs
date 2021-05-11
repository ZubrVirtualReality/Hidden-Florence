using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ARPlacementLock : MonoBehaviour
{
    public delegate void LockAction();
    public static event LockAction HoldFinished;

    [SerializeField] private Image fillImage;

    public float holdTime;
    private float currentTimeHeld;
    public bool holdComplete;

    private void OnDisable()
    {
        currentTimeHeld = 0;
        holdComplete = false;
        fillImage.fillAmount = 0;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && !holdComplete)
        {
            if (!EventSystem.current.IsPointerOverGameObject(0))
            {
                currentTimeHeld += Time.deltaTime;
            }
        }
        else if (currentTimeHeld < holdTime && currentTimeHeld > 0)
        {
            currentTimeHeld -= Time.deltaTime;
        }

        UpdateFill(currentTimeHeld);

        if (currentTimeHeld >= holdTime)
        {
            holdComplete = true;
        }
        if (holdComplete && !Input.GetMouseButton(0))
        {
            if (HoldFinished != null)
            {
                HoldFinished();
            }
            gameObject.SetActive(false);
        }

#endif
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Input.touchCount > 0 && !holdComplete)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                currentTimeHeld += Time.deltaTime;
            }
        }
        else if (currentTimeHeld < holdTime && currentTimeHeld > 0)
        {
            currentTimeHeld -= Time.deltaTime;
        }

        UpdateFill(currentTimeHeld);        

        if (currentTimeHeld >= holdTime)
        {
            holdComplete = true;
        }

        if (holdComplete && Input.touchCount == 0)
        {
            if (HoldFinished != null)
            {
                HoldFinished();
            }
            gameObject.SetActive(false);
        }
#endif

    }

    void UpdateFill(float _currentHoldTime)
    {
        float currentPercentage = _currentHoldTime / holdTime;
        fillImage.fillAmount = currentPercentage;
    }
}
