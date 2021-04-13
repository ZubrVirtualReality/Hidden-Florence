using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotspot : MonoBehaviour
{
    [SerializeField] Transform anchor;
    [SerializeField] RectTransform rect;
    [SerializeField] Button click;
    [SerializeField] string title;
    [SerializeField] string info;
    [SerializeField] Image image;

    private void OnEnable()
    {
        click.onClick.AddListener(ShowInfo);
    }

    private void Awake()
    {
        image = click.image;
    }
    private void ShowInfo()
    {
        HotspotManager.instance.ShowInfo(title, info);
    }

    bool InViewOfCamera(Vector3 _screen)
    {
       return  _screen.z>0&&_screen.x > 0 && _screen.x < 1 && _screen.y > 0 && _screen.y < 1;
    }

    private void Update()
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(anchor.position);
        Vector3 screen = Camera.main.WorldToViewportPoint(anchor.position);
        image.enabled = InViewOfCamera(screen);
        if (!InViewOfCamera(screen))
        {            
            return; 
        }
     
        rect.position = pos;
    }

    private void OnDisable()
    {
        click.onClick.RemoveListener(ShowInfo);
    }
}
