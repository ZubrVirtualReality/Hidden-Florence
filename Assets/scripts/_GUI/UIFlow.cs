using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIFlow : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI alertText, instructionsText;
	[SerializeField] string[] texts;
	[SerializeField] private CanvasGroup instructionsBox, alertBox;
	[SerializeField] private float fadeSpeed;
	public CanvasGroup helpCanvas;


	// Use this for initialization
	void Start()
	{
		alertBox.alpha = 0;
		instructionsBox.alpha = 0;
		StartCoroutine(fadeIn(alertBox));
		StartCoroutine(beginning());
	}

	IEnumerator beginning()
	{ //Setting first text when you start
		yield return new WaitForSeconds(3f);
		StartCoroutine(fadeOut(alertBox));
		StartCoroutine(fadeIn(instructionsBox));
		yield return new WaitForSeconds(5f);
		StartCoroutine(fadeOut(instructionsBox));
	}

	public void handleHelpButtonPress()
	{
		helpCanvas.blocksRaycasts = true;
		helpCanvas.interactable = true;
		StartCoroutine(fadeIn(helpCanvas, 1f));
	}

	public void handleCloseHelpPanelPress()
	{
		helpCanvas.blocksRaycasts = false;
		helpCanvas.interactable = false;
		StartCoroutine(fadeOut(helpCanvas, 1f));
	}

	IEnumerator fadeIn(CanvasGroup c, float maxAlpha = 0.9f)
	{
		float temp = c.alpha = 0;
		yield return new WaitForSeconds(0.5f);
		while (temp < maxAlpha)
		{
			temp += Time.deltaTime * fadeSpeed;
			c.alpha = temp;
			yield return null;
		}
	}
	IEnumerator fadeOut(CanvasGroup c, float maxAlpha = 0.7f)
	{
		float temp = c.alpha = maxAlpha;
		yield return new WaitForSeconds(0.5f);
		while (temp > 0)
		{
			temp -= Time.deltaTime * fadeSpeed;
			c.alpha = temp;
			yield return null;
		}
	}
	public void callBackButton()
	{
		StartCoroutine(back());
	}
	IEnumerator back()
	{
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("MainMenu");
	}
}

