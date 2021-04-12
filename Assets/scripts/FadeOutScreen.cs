using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeOutScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    public float fadeSpeed = 1;

    private void Start()
    {
        if(canvasGroup == null)
        {
            canvasGroup = this.GetComponent<CanvasGroup>();
        }
        canvasGroup.DOFade(0, fadeSpeed);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }


}
