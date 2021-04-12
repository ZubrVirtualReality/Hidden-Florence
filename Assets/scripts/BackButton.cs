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

    private void Back() // Previous Scene must be above in the settings
    {
        if(PanelHandler.instance.currentPanel.panelName != "MainPanel")
        {
            PanelHandler.instance.ReturnToMain();
        }
        else
        {
            SceneManager.LoadScene(previousSceneName);
        }
    }


}
