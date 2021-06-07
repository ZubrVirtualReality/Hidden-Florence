// Written By : Thomas Harrison
// Date : 07/06/2021
// Description: Informs the hotspot manager that the hotspots are ready to be assigned and placed in the scene
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignHotspots : MonoBehaviour
{
    [SerializeField] private List<Transform> hotspots = new List<Transform>();


    void Start()
    {
        HotspotManager.instance.SetUp(hotspots);
    }


    void Update()
    {
        
    }
}
