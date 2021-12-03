using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;

    float mousezoom = 0;

    public Image floorPlan;

    bool scrolling = false;

    bool zooming = false;


    Touch touchZero;
    Touch touchOne;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !scrolling)
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            scrolling = true;
        }
        if (Input.touchCount == 2 && !zooming)
        {
            touchZero = Input.GetTouch(0);
            touchOne = Input.GetTouch(1);

            zooming = true;
        }
        else if (Input.GetMouseButton(0) && scrolling)
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Camera.main.transform.position += direction;

            floorPlan.transform.position += direction;

            Debug.Log("Start: " + touchStart);
            Debug.Log("Current: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));

        }

        if (Input.touchCount == 2 && zooming)
        {
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        mousezoom += Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            zoom(mousezoom);

        }

        if (Input.GetMouseButtonUp(0))
        {
            scrolling = false;
        }

        if (Input.touchCount == 0)
        {
            zooming = false;
        }
    }

    void zoom(float increment)
    {
        //Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);

        floorPlan.transform.localScale = Vector3.one * Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);// new Vector3(Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax), Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax), Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax));
    }
}