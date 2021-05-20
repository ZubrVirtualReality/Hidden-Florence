using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class churchenabled : MonoBehaviour
{
    public delegate void SpawnAction(Vector3 _centre);
    public static event SpawnAction ChurchEnabled;
    public static churchenabled instance;
    [SerializeField] private GameObject origin;

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public Vector3 GetOrigin()
    {
        return origin.transform.position;
    }

    private void Start()
    {
        ChurchEnabled.Invoke(origin.transform.position);
    }

}
