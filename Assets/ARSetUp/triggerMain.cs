using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class triggerMain : MonoBehaviour {


	//[SerializeField] private ARReferenceImage referenceImage;
	private GameObject imageAnchorGO;
	//[SerializeField] private UnityARCameraManager camScript;
	private bool seen = false;
	public Transform ScannerOrigin;
	public bool active = false;
	public GameObject church;
	[SerializeField] private IMStartMenu menu;
	[SerializeField] ARTrackedImageManager tim;
	[SerializeField] bool isInnocenti = false;
	const string innocenti = "Adoration_of_the_Magi_Spedale_degli_Innocenti";
	const string sanPierMaggiore = "newTrigger";
	[SerializeField] GameObject hotspots;

	// Use this for initialization
	void Start () 
	{
		tim.trackedImagesChanged += Changed;//.ARImageAnchorAddedEvent += AddImageAnchor;
		//tim.ARImageAnchorUpdatedEvent += UpdateImageAnchor;
		//tim.ARImageAnchorRemovedEvent += RemoveImageAnchor;
	}

    private void Changed(ARTrackedImagesChangedEventArgs obj)
    {
       foreach (ARTrackedImage i in obj.added)
		{
			AddImageAnchor(i);
		}
		foreach (ARTrackedImage i in obj.updated)
		{
			UpdateImageAnchor(i);
		}
	
	}

 //   void AddImageAnchor(ARImageAnchor arImageAnchor)
	//{
	//	if (arImageAnchor.referenceImageName == referenceImage.imageName) {
	//		Vector3 position = UnityARMatrixOps.GetPosition (arImageAnchor.transform);
	//		Quaternion rotation = UnityARMatrixOps.GetRotation (arImageAnchor.transform);
	//		church.transform.position = position;
	//		church.transform.rotation = rotation;
	//		ScannerOrigin.position = position;
	//	}
	//}
	void AddImageAnchor(ARTrackedImage arImageAnchor)
	{
		if(arImageAnchor.referenceImage.name != (isInnocenti? innocenti: sanPierMaggiore))
        {
			return;
        }
			Vector3 position = arImageAnchor.transform.position;
			Quaternion rotation =arImageAnchor.transform.rotation;
			church.transform.position = position;
			church.transform.rotation = rotation;
			ScannerOrigin.position = position;
			hotspots.SetActive(true);
	}

	void UpdateImageAnchor(ARTrackedImage arImageAnchor)
	{
		if (arImageAnchor.referenceImage.name != (isInnocenti ? innocenti : sanPierMaggiore))
		{
			return;
		}

		church.transform.position = arImageAnchor.transform.position;
			church.transform.rotation = arImageAnchor.transform.rotation;
			if (!seen)
			{
				menu.callSetText(3);
				seen = true;
			}
		
	}
	//void UpdateImageAnchor(ARImageAnchor arImageAnchor)
	//{
	//	if (arImageAnchor.referenceImageName == referenceImage.imageName) {
	//		church.transform.position = UnityARMatrixOps.GetPosition(arImageAnchor.transform);
	//		church.transform.rotation = UnityARMatrixOps.GetRotation(arImageAnchor.transform);
	//		if(!seen){
	//			menu.callSetText(3);
	//			seen = true;
	//		}
	//	}
	//}

	//void RemoveImageAnchor(ARImageAnchor arImageAnchor)
	//{
	//}

	void OnDestroy()
	{
		tim.trackedImagesChanged -= Changed;
	}
}
