using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineUpStatues : MonoBehaviour
{
    [SerializeField] GameObject chapel;
    [SerializeField] ScannerEffectDemo scannerEffect;

    private void Start()
    {
        chapel.SetActive(true);
        scannerEffect.startPainting();
    }
}
