using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeScan : MonoBehaviour
{
    [SerializeField] private GameObject painting;
    [SerializeField] private ScannerEffectDemo shaderScript;

    bool once = false;

    private void OnEnable()
    {
        churchenabled.ChurchEnabled += StartEffect;
    }

    private void OnDisable()
    {
        churchenabled.ChurchEnabled -= StartEffect;
    }

    void StartEffect()
    {
        Debug.Log("Church Enabled");
        //painting.SetActive(true);
        shaderScript.StartShaderWithoutApproval();
        shaderScript.StartShader();
        painting.transform.position = Vector3.zero;
        once = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!once)
            {
                painting.SetActive(true);
                shaderScript.StartShaderWithoutApproval();
                shaderScript.StartShader();
                painting.transform.position = Vector3.zero;
                once = true;
            }
           //else
           //{
           //    shaderScript.StartShader();
           //}
        }
    }
}
