// Written By : Thomas Harrison
// Date : 07/06/2021
// Description: Back button to load a previous scene
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    private Button button;
    public string previousSceneName;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Back);
    }

    private void Back() // If the user is at the start of the flow load previous scene otherwise jump back to start
    {
        if (PanelHandler.instance != null)
        {
            if (PanelHandler.instance.currentPanel.panelName != "MainPanel")
            {
                PanelHandler.instance.ReturnToMain();
            }
            else
            {
                Debug.Log("Going back to " + previousSceneName);
                SceneManager.LoadScene(previousSceneName);
            }
        }
        else
        {
            Debug.Log("Going back to " + previousSceneName);
            SceneManager.LoadScene(previousSceneName);
        }
    }


}
