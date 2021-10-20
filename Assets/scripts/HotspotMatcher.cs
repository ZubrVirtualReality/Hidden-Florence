using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotMatcher : MonoBehaviour
{
    private void Start()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            HotspotManager.instance.hotspots[i].anchor = transform.GetChild(i);
            
        }
        HotspotManager.instance.TurnOn();       
    }
}
