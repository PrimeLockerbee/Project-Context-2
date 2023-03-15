using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject endcam;
    public GameObject minimap;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (menu.activeSelf.Equals(false))
            {
                menu.SetActive(true);
                minimap.SetActive(false);
            }
            else
            {
                menu.SetActive(false);
                minimap.SetActive(true);
                endcam.SetActive(false);
            }
            
        }
    }
    /*
    public void SceneLoader(int num)
    {
        SceneManager.LoadScene(num);
    }
    */
    public void Quit()
    {
        Application.Quit();
    }
}
