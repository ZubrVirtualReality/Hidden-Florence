using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;using System.IO;

public class GetText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!File.Exists(Path.Combine(Application.streamingAssetsPath, "text.txt")))
            {
            File.Create(Path.Combine(Application.streamingAssetsPath, "text.txt"));
        }

       // File.Open(Path.Combine(Application.streamingAssetsPath, "text.txt"));

        TMP_Text[] text = FindObjectsOfType<TMP_Text>();

        foreach(TMP_Text t in text)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
