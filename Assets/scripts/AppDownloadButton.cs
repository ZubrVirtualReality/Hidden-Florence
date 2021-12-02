using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppDownloadButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenIOSPage()
    {
        Application.OpenURL("https://apps.apple.com/us/app/hidden-florence/id896723912");
    }

    public void OpenAndroidPage()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=uk.ac.exeter.hiddenflorence&hl=en&gl=US");
    }


    public void DownloadForCurrentPlatform()
    {

#if UNITY_IOS

        Application.OpenURL("https://apps.apple.com/us/app/hidden-florence/id896723912");

#elif UNITY_ANDROID

        Application.OpenURL("https://play.google.com/store/apps/details?id=uk.ac.exeter.hiddenflorence&hl=en&gl=US");

#endif



    }
}
