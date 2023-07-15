using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject tutorialObj;
    public void Button(int ID)
    {
        if (ID == 1)
        {
            SceneManager.LoadScene("GameScene");
        }
        if (ID == 2)
        {
            Application.Quit();
        }
        if (ID == 3)
        {
            tutorialObj.SetActive(true);
        }
        if (ID == 4)
        {
            tutorialObj.SetActive(false);
        }
    }
}
