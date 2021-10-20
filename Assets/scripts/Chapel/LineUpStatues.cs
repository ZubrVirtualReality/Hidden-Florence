using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LineUpStatues : MonoBehaviour
{
    [SerializeField] GameObject chapel;
    [SerializeField] ScannerEffectDemo scannerEffect;
    [SerializeField] GameObject statues;
    bool once = false;
    private void Update()
    {
        if (once)
            return;

        if (Input.touchCount > 1||Input.GetKeyDown(KeyCode.O))
        {
            statues.transform.parent = chapel.transform;
            chapel.transform.parent = null;
            chapel.SetActive(true);
            chapel.transform.DOMoveY(chapel.transform.position.y + 4.73f, 10);
            scannerEffect.startPainting();
            //scannerEffect.StartShader();
                
            once = true;
        }
    }
}
