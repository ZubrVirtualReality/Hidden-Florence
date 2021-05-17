using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class churchenabled : MonoBehaviour
{
    public delegate void SpawnAction();
    public static event SpawnAction ChurchEnabled;
    public static churchenabled instance;

    private void OnEnable()
    {
        ChurchEnabled.Invoke();
    }

}
