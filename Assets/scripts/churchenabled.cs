using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class churchenabled : MonoBehaviour
{
    public delegate void SpawnAction();
    public static event SpawnAction ChurchEnabled;
    public static churchenabled instance;
    [SerializeField] private GameObject origin;


    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
        ChurchEnabled.Invoke();
    }

    public Vector3 GetOrigin()
    {
        return origin.transform.position;
    }

}
