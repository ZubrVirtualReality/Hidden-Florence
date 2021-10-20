using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndScale : MonoBehaviour
{

    float dist = 0;
    // Update is called once per frame

    
    void Update()
    {
        if(Input.touchCount>0)
        {
            if(Input.touchCount>1)
            {

                    float pinchAmount = 0;
                    Quaternion desiredRotation = transform.rotation;

                    DetectTouchMovement.Calculate();

                    if (Mathf.Abs(DetectTouchMovement.pinchDistanceDelta) > 0)
                    { // zoom
                        pinchAmount = DetectTouchMovement.pinchDistanceDelta;
                    }

                    if (Mathf.Abs(DetectTouchMovement.turnAngleDelta) > 0)
                    { // rotate
                        Vector3 euler = new Vector3(0, -DetectTouchMovement.turnAngleDelta, 0);
                        desiredRotation *= Quaternion.Euler(euler);
                    }

                   transform.rotation = desiredRotation;
                //  Camera.main.transform.localScale += Vector3.one * pinchAmount;
               // Vector3 scale = ARManager.instance.gameObject.transform.localScale;

               //scale += Vector3.one * (pinchAmount);
               // float x = Mathf.Clamp(scale.x, 0.1f, 2);
               // ARManager.instance.gameObject.transform.localScale = Vector3.one * x;


            }
            else
            {

            }
        }
    }
}
