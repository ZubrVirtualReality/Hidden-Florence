using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeScan : MonoBehaviour
{
    [SerializeField] private GameObject painting;
    [SerializeField] private GameObject paintingToSpawn;
    [SerializeField] private ScannerEffectDemo shaderScript;
    [SerializeField] private GameObject scanCentre;

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
        //painting.SetActive(true);
        scanCentre.transform.position = churchenabled.instance.GetOrigin();
        shaderScript.StartShaderWithoutApproval(0);
        shaderScript.StartShader();
        //painting.transform.position = Vector3.zero;
        once = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!once)
            {
                //painting.SetActive(true);
                painting = Instantiate(paintingToSpawn);
                painting.transform.Rotate(-90, 0, 0);
                scanCentre.transform.position = paintingToSpawn.GetComponent<churchenabled>().GetOrigin();
                shaderScript.StartShaderWithoutApproval(0);
                shaderScript.StartShader();
                //painting.transform.position = Vector3.zero;
                once = true;
            }
           //else
           //{
           //    shaderScript.StartShader();
           //}
        }
    }
}
