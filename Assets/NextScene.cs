using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string nextScene;

    void Start()
    {
        if(button == null)
        {
            button = GetComponent<Button>();
        }
        button.onClick.AddListener(MoveScene);
    }

    public void MoveScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
