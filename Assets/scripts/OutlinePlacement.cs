using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class OutlinePlacement : MonoBehaviour
{
    bool placed = false;
    [SerializeField] GameObject outline;
    [SerializeField] GameObject arObject;
    [SerializeField] Animator ani;
  
    private void Update()
    {
        if (Input.touchCount > 0||Input.GetMouseButtonUp(0))
        {
            if (!placed)
            {
                outline.SetActive(false);
                arObject.transform.parent = null;
                
                arObject.AddComponent<ARAnchor>();
                
                arObject.SetActive(true);
                ani.SetTrigger("Open");
                
                placed = true;
            }
        }
    }
}

