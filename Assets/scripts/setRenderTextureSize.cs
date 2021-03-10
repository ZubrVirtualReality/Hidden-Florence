using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class setRenderTextureSize : MonoBehaviour {

	public Camera cam;
	public Material mat;
	public string texture;
	[SerializeField] ARCameraBackground main;

	// Use this for initialization
	void Start () 
	{

		Graphics.Blit(null, cam.targetTexture, main.material);
		if ( cam.targetTexture != null ) {
         	cam.targetTexture.Release( );
     	}
     	cam.targetTexture = new RenderTexture( Screen.width, Screen.height, 24 );
		mat.SetTexture(texture, cam.targetTexture);
	}
	
	void Update () {
		Graphics.Blit(null, cam.targetTexture, main.material);
	}
}
