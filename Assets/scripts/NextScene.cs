// Written By : Thomas Harrison
// Date : 07/06/2021
// Description: Simple button thatll take you to the specified scipt on click
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
