using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoorSceneUserMove : MonoBehaviour
{
    bool readyToMove;

    [SerializeField] GameObject chapel;


    Vector3 chapelpos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToMove)
        {
            if (Input.touchCount > 0)
            {
                

                float angle = Camera.main.transform.rotation.y - chapel.transform.rotation.y;

                if (angle > -45f && angle < 45f)
                {

                    Touch t = Input.GetTouch(0);

                    if (t.phase == TouchPhase.Moved)
                    {
                        chapel.transform.localPosition = new Vector3(chapel.transform.localPosition.x, chapel.transform.localPosition.y, Mathf.Clamp(chapel.transform.localPosition.z + Input.GetTouch(0).deltaPosition.y / 20, chapelpos.z - 20, chapelpos.z));
                    }
                }
                
            }
        }
    }

    public void SetReadyToMove(bool value)
    {
        readyToMove = value;
    }

    public void SetChapelPos(Vector3 pos)
    {
        chapelpos = pos;
    }
}
