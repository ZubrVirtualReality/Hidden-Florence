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

            //scannerEffect.startPainting();

            scannerEffect.StartShaderWithoutApproval(0);
            scannerEffect.StartShader();

            once = true;
        }
    }

    IEnumerator StatueSequence()
    {
        
        chapel.SetActive(true);
        statues.transform.parent = chapel.transform;
        chapel.transform.parent = null;

        chapel.transform.DOMoveY(chapel.transform.position.y + 4.1f, 10); // (-6.78 = 4.73f)  (- 5.55 = 3.5f)

        chapel.transform.DORotateQuaternion(new Quaternion(0, chapel.transform.rotation.y, 0, chapel.transform.rotation.w), 10);


        yield return new WaitForSeconds(10);

        statueLeft.transform.DOMove(leftEndPos.position, 3);
        statueRight.transform.DOMove(rightEndPos.position, 3);


        //chapel.transform.rotation = new Quaternion(0, chapel.transform.rotation.y, 0, chapel.transform.rotation.w);

        //chapel.transform.DORotate(new Vector3(0, chapel.transform.rotation.y, 0), 10);


        //scannerEffect.StartShader();

        
    }
}
