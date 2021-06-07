// Written By : Thomas Harrison
// Date : 07/06/2021
// Description: Container for flow of scenes in the app. Informs the manager to open this page when clicked
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public string panelName;
    [SerializeField] private Button button;
    public string panelToOpen;
    public CanvasGroup canvasGroup;
    public string sceneToMoveTo;
    public string location;

    void Start()
    {
        if(panelName == null)
        {
            Debug.LogError("Panel Not Named");
        }
        if(canvasGroup == null)
        {
            Debug.LogWarning("Missing canvas group on = " + panelName);
        }
        if(button != null)
        {
            button.onClick.AddListener(OpenPanel);
        }
    }

    public void OpenPanel()
    {
        PanelHandler.instance.SetOpenPanel(this);
    }
}
