using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{

    //This loads scenes
    public void SceneChanger(string newLevel)
    {
        SceneManager.LoadScene(newLevel);
    }

    //Exit game
    public void doExitGame() {
        Application.Quit();
    }

}
