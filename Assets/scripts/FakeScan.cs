// Written By : Thomas Harrison
// Date : 07/06/2021
// Description: Listener for the church so when it is enabled the shader effect begins
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FakeScan : MonoBehaviour
{
    [SerializeField] private GameObject painting;
    [SerializeField] private GameObject paintingToSpawn;
    [SerializeField] private ScannerEffectDemo shaderScript;
    [SerializeField] private GameObject scanCentre;
    [SerializeField] private TextMeshProUGUI debugText;

    bool once = false;

    private void OnEnable()
    {
        churchenabled.ChurchEnabled += StartEffect;
    }

    private void OnDisable()
    {
        churchenabled.ChurchEnabled -= StartEffect;
    }

    void StartEffect(Vector3 _centrePosition) // Takes the origin and beings the effect
    {
        //painting.SetActive(true);
        scanCentre.transform.position = _centrePosition;
        shaderScript.StartShaderWithoutApproval(0);
        StartCoroutine(WaitFor(0.1f));
        //painting.transform.position = Vector3.zero;
        once = true;
        //StartCoroutine(WaitFor(0.08f));
    }

    IEnumerator WaitFor(float _waitTime)
    {
        yield return new WaitForSeconds(_waitTime);
        scanCentre.transform.position = churchenabled.instance.GetOrigin();
        shaderScript.StartShader();
        yield return null;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) // For testing purposes in editor
        {
            if (!once)
            {
                painting = Instantiate(paintingToSpawn);
                painting.transform.Rotate(-90, 0, 0);
                scanCentre.transform.position = paintingToSpawn.GetComponent<churchenabled>().GetOrigin();
                shaderScript.StartShaderWithoutApproval(0);
                shaderScript.StartShader();
                once = true;
            }
        }
    }
}
