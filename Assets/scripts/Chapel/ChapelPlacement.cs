using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ChapelPlacement : MonoBehaviour
{
    Sequence s;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 midPos;
    [SerializeField] Vector3 endPos;

    private void Awake()
    {
        s = DOTween.Sequence();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePos(startPos);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangePos(midPos);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangePos(endPos);
        }

        switch (Input.touchCount)
        {
            case 1:
                ChangePos(startPos);
                break;
            case 2:
                ChangePos(midPos);
                break;
            case 3:
                ChangePos(endPos);
                break;
        }
    }
    public void ChangePos(Vector3 _pos)
    {
        s?.Kill(true);
        s.Append(transform.DOLocalMove(_pos, 0.3f));
    }
}
