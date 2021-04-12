using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelHandler : MonoBehaviour
{
    public static PanelHandler instance;
    [SerializeField] private List<Panel> panels = new List<Panel>();
    public Panel currentPanel;

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
                currentPanel = panel;
            }
        }
    }
}