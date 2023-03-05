using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SceneLoader(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
