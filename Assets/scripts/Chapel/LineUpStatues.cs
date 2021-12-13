using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LineUpStatues : MonoBehaviour
{
    [SerializeField] GameObject chapel;
    [SerializeField] ScannerEffectDemo scannerEffect;
    [SerializeField] GameObject statues;

    [SerializeField] GameObject statueLeft, statueRight;

    [SerializeField] Transform leftEndPos, rightEndPos;

    bool once = false;
    private void Update()
    {
        if (once)
            return;

        if (Input.touchCount > 1||Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(StatueSequence());

            once = true;
        }
    }

    IEnumerator StatueSequence()
    {
        scannerEffect.startPainting();
        chapel.SetActive(true);
        statues.transform.parent = chapel.transform;
        chapel.transform.parent = null;


        statueLeft.transform.DOMove(leftEndPos.position, 3);
        statueRight.transform.DOMove(rightEndPos.position, 3);

        yield return new WaitForSeconds(3);

        
        
        chapel.transform.DOMoveY(chapel.transform.position.y + 4.73f, 10);

        //chapel.transform.rotation = new Quaternion(0, chapel.transform.rotation.y, 0, chapel.transform.rotation.w);

        //chapel.transform.DORotate(new Vector3(0, chapel.transform.rotation.y, 0), 10);

        chapel.transform.DORotateQuaternion(new Quaternion(0, chapel.transform.rotation.y, 0, chapel.transform.rotation.w), 10);

        //scannerEffect.StartShader();

        
    }
}
