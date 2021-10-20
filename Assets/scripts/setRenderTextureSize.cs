using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class setRenderTextureSize : MonoBehaviour {

	public Camera cam;
	public Material mat;
	public string texture;
	[SerializeField] public ARCameraBackground main;
	public TextMeshProUGUI debugText;
	// Use this for initialization
	void Start () 
	{

		Graphics.Blit(null, cam.targetTexture, main.material);
		if ( cam.targetTexture != null ) {
         	cam.targetTexture.Release( );
     	}
     	cam.targetTexture = new RenderTexture( Screen.width, Screen.height, 0 );
		mat.SetTexture(texture, cam.targetTexture);
	}
	
	void Update () 
	{
		//debugText.SetText(main.material.name);
		Graphics.Blit(null, cam.targetTexture, main.material);
	}
}
