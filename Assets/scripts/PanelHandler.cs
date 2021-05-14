using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelHandler : MonoBehaviour
{
    public static PanelHandler instance;
    [SerializeField] private List<Panel> panels = new List<Panel>();
    public Panel currentPanel;
    public string previousPanelName;
    public string futurePanelName;
    public string sceneName;

    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        currentPanel = panels[0];
    }

    public void SetOpenPanel(Panel _panel)
    {
        foreach (Panel panel in panels)
        {
            CanvasGroup tempGroup = panel.canvasGroup;
            if (panel.panelName != _panel.panelToOpen)
            {
                tempGroup.alpha = 0;
                tempGroup.blocksRaycasts = false;
                tempGroup.interactable = false;
            }
            else
            {
                tempGroup.alpha = 1;
                tempGroup.blocksRaycasts = true;
                tempGroup.interactable = true;
                if(_panel.sceneToMoveTo != "")
                {
                    futurePanelName = _panel.sceneToMoveTo;
                    sceneName = _panel.location;

                    AppManager.Instance.ChangeExperience(_panel.location);

                    
                }
                previousPanelName = currentPanel.panelName;
                currentPanel = panel;
            }
        }
    }

    public void ReturnToMain()
    {
        foreach (Panel panel in panels)
        {
            CanvasGroup tempGroup = panel.canvasGroup;
            if (panel.panelName != "MainPanel")
            {
                tempGroup.alpha = 0;
                tempGroup.blocksRaycasts = false;
                tempGroup.interactable = false;
            }
            else
            {
                tempGroup.alpha = 1;
                tempGroup.blocksRaycasts = true;
                tempGroup.interactable = true;
                previousPanelName = currentPanel.panelName;
                currentPanel = panel;
            }
        }
    }
}