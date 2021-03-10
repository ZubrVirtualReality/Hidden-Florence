using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startmenu : MonoBehaviour {

	public void onSite(){
		// Debug.Log("tapped nonAR");
		SceneManager.LoadScene("imageAnchor");
	}

	public void atHome(){
		// Debug.Log("tapped AR");
		SceneManager.LoadScene("tapToPlace");
	}

}
