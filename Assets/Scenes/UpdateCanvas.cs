using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Canvas.ForceUpdateCanvases();
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
