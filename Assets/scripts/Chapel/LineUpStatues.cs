using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LineUpStatues : MonoBehaviour
{
    [SerializeField] GameObject chapel;
    [SerializeField] ScannerEffectDemo scannerEffect;
    [SerializeField] GameObject statues;
    private void Update()
    {
        if (Input.touchCount > 1||Input.GetKeyDown(KeyCode.O))
        {
            statues.transform.parent = chapel.transform;
            chapel.transform.parent = null;
            chapel.SetActive(true);
            chapel.transform.DOMoveY(chapel.transform.position.y + 4.73f, 10);
            scannerEffect.startPainting();
        }
    }
}
