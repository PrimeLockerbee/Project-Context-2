using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void SceneLoader(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
