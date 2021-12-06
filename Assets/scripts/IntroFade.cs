using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IntroFade : MonoBehaviour
{
    public Image logo;

    bool introRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        /*if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            StartCoroutine(IntroSequence());
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
        }
        else
        {
            gameObject.SetActive(false);
        }*/

        StartCoroutine(IntroSequence());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator IntroSequence()
    {
        //yield return new WaitForSeconds(.1f);
        if (logo != null)
        {
            logo.transform.DOScale(.8f, 5);
        }

        yield return new WaitForSeconds(1f);

        FadeOutAndDisable(GetComponent<CanvasGroup>());

        //yield return new WaitForSeconds(2);
    }

    /// <summary>
    /// Will fade out the given CanvasGroup and disable interactivity.
    /// </summary>
    /// <param name="element"></param>
    private void FadeOutAndDisable(CanvasGroup element)
    {
        element.DOFade(0f, 1f);
        element.blocksRaycasts = false;
    }
}
