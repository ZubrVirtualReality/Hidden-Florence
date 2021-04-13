using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenAR : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(MoveScene);
    }

    public void MoveScene()
    {
        SceneManager.LoadScene(PanelHandler.instance.futurePanelName);
    }


}
