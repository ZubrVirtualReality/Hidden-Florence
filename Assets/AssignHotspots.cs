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
